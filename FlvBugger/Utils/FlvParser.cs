using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tsanie.FlvBugger.Utils {

    public class FlvParser {
        public long Length { get; private set; }
        public uint Duration { get; private set; }
        public double Rate { get; private set; }
        public ScriptTag MetaTag { get; private set; }
        public List<FlvTag> Tags { get; private set; }
        public bool IsFlv { get; private set; }

        public FlvParser(Stream stream, Predicate<FlvTag> exec) {
            FLVHeader header = FLVHeader.ReadHeader(stream);
            if (!header.IsFlv) {
                this.IsFlv = false;
                return;
            }
            this.IsFlv = true;
            stream.Seek(header.Length, SeekOrigin.Begin);
            Tags = new List<FlvTag>();
            FlvTag tag;
            while ((tag = FlvTag.ReadTag(stream)) != null) {
                if (tag is ScriptTag) {
                    this.MetaTag = tag as ScriptTag;
                }
                Tags.Add(tag);
                if (Duration < tag.TimeStamp)
                    Duration = tag.TimeStamp;
                if (exec != null) {
                    if (!exec(tag)) {
                        break;
                    }
                }
            }
            if (Tags.Count > 1) {
                this.Length = stream.Length - Tags[1].Offset + 11; //+ FlvMain.c_HeaderSize;
                this.Rate = (this.Duration == 0 ? 0 : this.Length * 8 / this.Duration);
            }
        }

        public void Remove(int index) {
            if (index < 0 || index >= Tags.Count)
                return;
            FlvTag tag = Tags[index];
            //if (Duration <= tag.TimeStamp) {
            //    RefreshDuration();
            //}
            this.Length -= tag.DataSize + 11;
            Tags.RemoveAt(index);
        }
        public void RefreshDuration() {
            // 重新获取Duration
            uint max = 0;
            foreach (FlvTag t in Tags) {
                if (max < t.TimeStamp)
                    max = t.TimeStamp;
            }
            this.Duration = max;
            this.Rate = (this.Duration == 0 ? 0 : this.Length * 8 / this.Duration);
        }

        public enum TagType {
            None = 0,
            Audio = 8,
            Video = 9,
            Script = 0x12
        }
        public class FlvTag {
            private uint presize;
            private int tagtype;
            private uint datasize;
            private uint timestamp; // 单位ms
            private int timestamp_ex;
            private uint streamid;
            protected byte taginfo;
            protected byte avcpaktype;
            private long offset;

            protected FlvTag() { }
            internal static FlvTag ReadTag(Stream stream) {
                try {
                    FlvTag tag;
                    byte[] buffer = new byte[4];
                    int rtn;
                    rtn = stream.Read(buffer, 0, 4);
                    if (rtn <= 0) {
                        return null;
                    }
                    int type = stream.ReadByte();
                    if (type == 8)
                        tag = new AudioTag();
                    else if (type == 9)
                        tag = new VideoTag();
                    else if (type == 0x12)
                        tag = new ScriptTag();
                    else
                        tag = new FlvTag();
                    tag.presize = ByteUtil.ByteToUInt(buffer, 4);
                    tag.tagtype = type;
                    tag.datasize = ByteUtil.ReadUI24(stream);
                    tag.timestamp = ByteUtil.ReadUI24(stream);
                    tag.timestamp_ex = stream.ReadByte();
                    tag.streamid = ByteUtil.ReadUI24(stream);
                    tag.offset = stream.Position;
                    if (tag is ScriptTag) {
                        (tag as ScriptTag).ReadScript(stream);
                        stream.Seek(tag.offset + tag.DataSize, SeekOrigin.Begin);
                    } else if (tag is AudioTag) {
                        rtn = stream.Read(buffer, 0, 1);
                        if (rtn <= 0)
                            return null;
                        tag.taginfo = buffer[0];
                        stream.Seek(tag.DataSize - 1, SeekOrigin.Current);
                    } else if (tag is VideoTag) {
                        rtn = stream.Read(buffer, 0, 2);
                        if (rtn <= 0)
                            return null;
                        tag.taginfo = buffer[0];
                        tag.avcpaktype = buffer[1];
                        stream.Seek(tag.DataSize - 2, SeekOrigin.Current);
                    }
                    return tag;
                } catch {
                    return null;
                }
            }
            public override string ToString() {
                return "#0: frame info"
                    + "\r\n#1: {"
                    + "\r\n  TagType: " + this.Type
                    + "\r\n  DataSize: " + this.DataSize
                    + "\r\n  TimeStamp: " + ByteUtil.GetTime(this.TimeStamp) + " (" + this.TimeStamp + ")"
                    + "\r\n  StreamsID: " + this.StreamID
                    + "\r\n  Offset: " + this.Offset
                    + "\r\n}"
                    ;
            }

            public TagType Type {
                get { return (TagType)tagtype; }
            }
            public int DataSize {
                get { return (int)datasize; }
            }
            public uint TimeStamp {
                get { return ((uint)timestamp_ex << 24) | timestamp; }
            }
            public uint StreamID {
                get { return streamid; }
            }
            public long Offset {
                get { return offset; }
            }
            public virtual string Info1 { get { return "-"; } }
            public virtual string Info2 { get { return "-"; } }
        }
        public class AudioTag : FlvTag {
            public double CodecId {
                get {
                    return (taginfo >> 4) & 0xF;
                }
            }
            public string Codec {
                get {
                    int codec = (taginfo >> 4) & 0xF;
                    switch (codec) {
                        case 0:
                            return "Linear PCM, platform endian";
                        case 1:
                            return "ADPCM";
                        case 2:
                            return "MP3";
                        case 3:
                            return "Linear PCM, little endian";
                        case 4:
                            return "Nellymoser 16-kHz momo";
                        case 5:
                            return "Nellymoser 8-kHz momo";
                        case 6:
                            return "Nellymoser";
                        case 7:
                            return "G.711 A-law logarithmic PCM";
                        case 8:
                            return "G.711 mu-law logarithmic PCM";
                        case 9:
                            return "(reserved)";
                        case 10:
                            return "AAC";
                        case 11:
                            return "Speex";
                        case 14:
                            return "MP3 8-kHz";
                        case 15:
                            return "Device-specific sound";
                        default:
                            return "(unrecognized #" + codec + ")";
                    }
                }
            }
            public int Sample {
                get {
                    int sample = (taginfo >> 2) & 0x3;
                    switch (sample) {
                        case 0:
                            return 5500;
                        case 1:
                            return 11000;
                        case 2:
                            return 22000;
                        case 3:
                            return 44000;
                        default:
                            return sample;
                    }
                }
            }
            public int Bit {
                get {
                    int bit = (taginfo >> 1) & 0x1;
                    if (bit == 0)
                        return 8;
                    else if (bit == 1)
                        return 16;
                    return bit;
                }
            }
            public int Channel {
                get {
                    return taginfo & 0x1;
                }
            }
            public override string Info1 { get { return Codec; } }
            public override string ToString() {
                return "#0: audio frame info"
                    + "\r\n#1: {"
                    + "\r\n  Codec: " + this.Codec
                    + "\r\n  TimeStamp: " + ByteUtil.GetTime(this.TimeStamp) + " (" + this.TimeStamp + ")"
                    + "\r\n  Sample: " + (this.Sample) / 1000 + "kHz"
                    + "\r\n  Bit: " + this.Bit + "bit"
                    + "\r\n  Channel: " + (this.Channel == 0 ? "Mono" : "Stereo")
                    + "\r\n  DataSize: " + this.DataSize + " bytes"
                    + "\r\n  Offset: " + this.Offset + " (0x" + this.Offset.ToString("X2") + ")"
                    + "\r\n}"
                    ;
            }
        }
        public class VideoTag : FlvTag {
            public string FrameType {
                get {
                    int type = (taginfo >> 4) & 0xF;
                    switch (type) {
                        case 1:
                            return "keyframe";
                        case 2:
                            return "inter frame";
                        case 3:
                            return "disposable inter frame";
                        case 4:
                            return "generated keyframe";
                        case 5:
                            return "video info/command frame";
                        default:
                            return "(unrecognized #" + type + ")";
                    }
                }
            }
            public string Codec {
                get {
                    int codec = taginfo & 0xF;
                    switch (codec) {
                        case 1:
                            return "JPEG (currently unused)";
                        case 2:
                            return "H.263";
                        case 3:
                            return "Screen video";
                        case 4:
                            return "On2 VP6";
                        case 5:
                            return "On2 VP6 with alpha channel";
                        case 6:
                            return "Screen video version 2";
                        case 7:
                            return "H.264";
                        default:
                            return "(unrecognized #" + codec + ")";
                    }
                }
            }
            public double CodecId {
                get {
                    return taginfo & 0xF;
                }
            }
            public int AVCPacketType {
                get { return avcpaktype; }
            }
            public override string Info1 { get { return Codec; } }
            public override string Info2 { get { return FrameType; } }
            public override string ToString() {
                return "#0: video " + this.FrameType + " info"
                    + "\r\n#1: {"
                    + "\r\n  Codec: " + this.Codec
                    + "\r\n  TimeStamp: " + ByteUtil.GetTime(this.TimeStamp) + " (" + this.TimeStamp + ")"
                    + "\r\n  DataSize: " + this.DataSize + " bytes"
                    + "\r\n  Offset: " + this.Offset + " (0x" + this.Offset.ToString("X2") + ")"
                    + "\r\n}"
                    ;
            }
        }
        public class ScriptTag : FlvTag {
            public List<KeyValuePair<string, object>> Values { get; private set; }
            private int offset = 0;

            public ScriptTag() {
                Values = new List<KeyValuePair<string, object>>();
            }
            public override string ToString() {
                string str = "";
                foreach (KeyValuePair<string, object> kv in this.Values) {
                    str += kv.Key + ": " + kv.Value + "\r\n";
                }
                return str;
            }
            public override string Info2 { get { return Values.Count + " 元素"; } }

            public bool TryGet(string key, out object o) {
                o = null;
                foreach (KeyValuePair<string, object> kv in Values) {
                    if (kv.Value is ScriptObject) {
                        o = (kv.Value as ScriptObject)[key];
                    }
                }
                return o != null;
            }

            private object ReadElement(Stream src) {
                int type = src.ReadByte();
                offset++;
                switch (type) {
                    case 0: // Number - 8
                        return ReadDouble(src);
                    case 1: // Boolean - 1
                        return ReadByte(src);
                    case 2: // String - 2+n
                        return ReadString(src);
                    case 3: // Object
                        return ReadObject(src);
                    case 4: // MovieClip
                        return ReadString(src);
                    case 5: // Null
                        break;
                    case 6: // Undefined
                        break;
                    case 7: // Reference - 2
                        return ReadUShort(src);
                    case 8: // ECMA array
                        return ReadArray(src);
                    case 10: // Strict array
                        return ReadStrictArray(src);
                    case 11: // Date - 8+2
                        return ReadDate(src);
                    case 12: // Long string - 4+n
                        return ReadLongString(src);
                }
                return null;
            }
            private object ReadObject(Stream src) {
                byte[] bs = new byte[3];
                ScriptObject obj = new ScriptObject();
                while (offset < this.DataSize) {
                    src.Read(bs, 0, 3);
                    if (bs[0] == 0 && bs[1] == 0 && bs[2] == 9) {
                        offset += 3;
                        break;
                    }
                    src.Seek(-3, SeekOrigin.Current);
                    string key = ReadString(src);
                    if (key[0] == 0)
                        break;
                    obj[key] = ReadElement(src);
                }
                return obj;
            }
            private double ReadDate(Stream src) {
                double d = ReadDouble(src);
                src.Seek(2, SeekOrigin.Current);
                offset += 2;
                return d;
            }
            private ScriptObject ReadArray(Stream src) {
                byte[] buffer = new byte[4];
                src.Read(buffer, 0, 4);
                offset += 4;
                uint count = ByteUtil.ByteToUInt(buffer, 4);
                ScriptObject array = new ScriptObject();
                for (uint i = 0; i < count; i++) {
                    string key = ReadString(src);
                    array[key] = ReadElement(src);
                }
                src.Seek(3, SeekOrigin.Current); // 00 00 09
                offset += 3;
                return array;
            }
            private ScriptArray ReadStrictArray(Stream src) {
                byte[] bs = new byte[4];
                src.Read(bs, 0, 4);
                offset += 4;
                ScriptArray array = new ScriptArray();
                uint count = ByteUtil.ByteToUInt(bs, 4);
                for (uint i = 0; i < count; i++) {
                    array.Add(ReadElement(src));
                }
                return array;
            }
            private double ReadDouble(Stream src) {
                byte[] buffer = new byte[8];
                src.Read(buffer, 0, 8);
                offset += 8;
                return ByteUtil.ByteToDouble(buffer);
            }
            private byte ReadByte(Stream src) {
                offset++;
                return (byte)src.ReadByte();
            }
            private string ReadString(Stream src) {
                byte[] bs = new byte[2];
                src.Read(bs, 0, 2);
                offset += 2;
                int n = (int)ByteUtil.ByteToUInt(bs, 2);
                bs = new byte[n];
                src.Read(bs, 0, n);
                offset += n;
                return Encoding.ASCII.GetString(bs);
            }
            private string ReadLongString(Stream src) {
                byte[] bs = new byte[4];
                src.Read(bs, 0, 4);
                offset += 4;
                int n = (int)ByteUtil.ByteToUInt(bs, 4);
                bs = new byte[n];
                src.Read(bs, 0, n);
                offset += n;
                return Encoding.ASCII.GetString(bs);
            }
            private ushort ReadUShort(Stream src) {
                byte[] buffer = new byte[2];
                src.Read(buffer, 0, 2);
                offset += 2;
                return (ushort)ByteUtil.ByteToUInt(buffer, 2);
            }
            internal void ReadScript(Stream stream) {
                offset = 0;
                Values.Clear();
                byte[] bs = new byte[3];
                while (offset < this.DataSize) {
                    stream.Read(bs, 0, 3);
                    if (bs[0] == 0 && bs[1] == 0 && bs[2] == 9) {
                        offset += 3;
                        break;
                    }
                    stream.Seek(-3, SeekOrigin.Current);
                    AddElement("#" + offset, ReadElement(stream));
                }
            }
            private void AddElement(string key, object o) {
                Values.Add(new KeyValuePair<string, object>(key, o));
            }
            public class ScriptObject {
                public static int indent = 0;
                private Dictionary<string, object> values = new Dictionary<string, object>();
                public object this[string key] {
                    get {
                        object o;
                        values.TryGetValue(key, out o);
                        return o;
                    }
                    set {
                        if (!values.ContainsKey(key)) {
                            values.Add(key, value);
                        }
                    }
                }
                public override string ToString() {
                    string str = "{\r\n";
                    ScriptObject.indent += 2;
                    foreach (KeyValuePair<string, object> kv in values) {
                        str += new string(' ', ScriptObject.indent) + kv.Key + ": " + kv.Value + "\r\n";
                    }
                    ScriptObject.indent -= 2;
                    //if (str.Length > 1)
                    //    str = str.Substring(0, str.Length - 1);
                    str += "}";
                    return str;
                }
            }
            public class ScriptArray {
                private List<object> values = new List<object>();
                public object this[int index] {
                    get {
                        if (index >= 0 && index < values.Count)
                            return values[index];
                        return null;
                    }
                }
                public void Add(object o) {
                    values.Add(o);
                }
                public override string ToString() {
                    string str = "[";
                    int n = 0;
                    foreach (object o in values) {
                        if (n % 10 == 0)
                            str += "\r\n";
                        n++;
                        str += o + ",";
                    }
                    if (str.Length > 1)
                        str = str.Substring(0, str.Length - 1);
                    str += "\r\n]";
                    return str;
                }
            }
        }
        private class FLVHeader {
            private byte[] signature;
            private byte version;
            private byte typeflag;
            private int dataoffset;

            private FLVHeader() {
                signature = new byte[3];
                version = 0;
                typeflag = 0;
                dataoffset = 0;
            }
            internal static FLVHeader ReadHeader(Stream stream) {
                FLVHeader header = new FLVHeader();
                byte[] buffer = new byte[4];
                stream.Read(header.signature, 0, 3);
                stream.Read(buffer, 0, 1);
                header.version = buffer[0];
                stream.Read(buffer, 0, 1);
                header.typeflag = buffer[0];
                try {
                    header.dataoffset = (int)ByteUtil.ReadUI32(stream);
                } catch { }
                return header;
            }

            public bool IsFlv {
                get {
                    if (signature == null || signature.Length != 3)
                        return false;
                    return (signature[0] == 0x46) &&
                        (signature[1] == 0x4C) &&
                        (signature[2] == 0x56);
                }
            }
            public int Version {
                get { return version; }
            }
            public bool HasVideo {
                get { return (typeflag & 0x1) == 0x1; }
            }
            public bool HasAudio {
                get { return (typeflag & 0x4) == 0x4; }
            }
            public int Length {
                get {
                    return dataoffset;
                }
            }
        }
    }

    public class ByteUtil {
        public static uint ByteToUInt(byte[] bs, int length) {
            if (bs == null || bs.Length < length)
                return 0;
            uint rtn = 0;
            for (int i = 0; i < length; i++) {
                rtn <<= 8;
                rtn |= bs[i];
            }
            return rtn;
        }
        public static double ByteToDouble(byte[] bs) {
            if (bs == null || bs.Length < 8)
                return 0;
            byte[] b2 = new byte[8];
            for (int i = 0; i < 8; i++) {
                b2[i] = bs[7 - i];
            }
            return BitConverter.ToDouble(b2, 0);
        }
        public static short ReadUI16(Stream src) {
            byte[] bs = new byte[2];
            if (src.Read(bs, 0, 2) <= 0)
                return 0;
            return (short)((bs[0] << 8) | bs[1]);
        }
        public static uint ReadUI24(Stream src) {
            byte[] bs = new byte[3];
            if (src.Read(bs, 0, 3) <= 0)
                throw new IOException("Stream end.");
            return ByteToUInt(bs, 3);
        }
        public static uint ReadUI32(Stream src) {
            byte[] bs = new byte[4];
            if (src.Read(bs, 0, 4) <= 0)
                throw new IOException("Stream end.");
            return ByteToUInt(bs, 4);
        }
        public static string GetTime(uint time) {
            return (time / 60000).ToString() + ":"
                + (time / 1000 % 60).ToString("D2") + "."
                + (time % 1000).ToString("D3");
        }
    }

}
