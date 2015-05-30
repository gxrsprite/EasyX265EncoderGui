using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsanie.FlvBugger.Utils;

namespace Tsanie.FlvBugger
{
    public class FlvMain
    {
        private FlvParser _parser = null;
        private string _filename = "";

        public void addFile(string filename)
        {
            _filename = filename;
            int n = 1;
            //List<ListViewItem> items = new List<ListViewItem>();
            FileStream stream = new FileStream(_filename, FileMode.Open, FileAccess.Read);

            _parser = new FlvParser(stream, (tag) =>
            {
                string[] s = new string[]{
                            n++.ToString(),
                            tag.Type.ToString(),
                            ByteUtil.GetTime(tag.TimeStamp),
                            tag.Info1,
                            tag.Info2,
                            "0x" + tag.Offset.ToString("X8"),
                            tag.DataSize.ToString()
                        };
                FlvParser.TagType type = tag.Type;
                if (type == FlvParser.TagType.Audio)
                {
                    FlvParser.AudioTag atag = tag as FlvParser.AudioTag;
                }
                else if (type == FlvParser.TagType.Video)
                {
                    FlvParser.VideoTag vtag = tag as FlvParser.VideoTag;
                }
                else if (type == FlvParser.TagType.Script)
                {
                }
                return true;
            });
            stream.Close();
            stream.Dispose();

            if (!_parser.IsFlv)
            {
                //无法识别
            }
            else
            {

            }

        }

        public void ExecuteBlack(double rate, double time, string outputfilename)
        {
            if (_parser == null)
                return;

            double o_rate = _parser.Rate;
            if (o_rate < rate)
            {
                // 不需要转换
                //此视频不需要转换 后黑

                return;
            }
            long filesize = _parser.Length + 16;
            double duration;
            if (time < 0)
            {
                // 计算傲娇时间
                duration = filesize / 125.0 / rate; // * 8 / 1000 / rate
            }
            else
            {
                duration = _parser.Duration / 1000.0 + time;
            }
            string offset = ((duration * 1000 - _parser.Duration) / 1000).ToString("0.000");

            //开始处理后黑

            Stream src = new FileStream(_filename, FileMode.Open);
            string path = outputfilename;

            Stream dest = new FileStream(path, FileMode.Create);
            WriteHead(dest, filesize, duration, -1, -1, -1, 1.0, 0,
                0, _parser.Tags.Count - 1, false);
            for (int i = 1; i < _parser.Tags.Count; i++)
            {
                src.Seek(_parser.Tags[i].Offset - 11, SeekOrigin.Begin);
                FlvParser.FlvTag tag = _parser.Tags[i];
                byte[] bs = new byte[tag.DataSize + 11];
                // 数据
                src.Read(bs, 0, bs.Length);
                dest.Write(bs, 0, bs.Length);
                // prev tag size
                src.Read(bs, 0, 4);
                dest.Write(bs, 0, 4);

            }
            src.Close();
            src.Dispose();
            byte[] buffer = new byte[]{
                    0x09, 0, 0, 0x01, // 视频帧 1 字节
                    0, 0, 0, 0,       // 04h, timestamp & ex
                    0, 0, 0,          // stream id
                    0x17,             // InnerFrame, H.264
                    0, 0, 0, 0x0c     // 此帧长度 12 字节
                };
            uint dur = (uint)(duration * 1000);
            PutTime(buffer, 0x04, dur);
            dest.Write(buffer, 0, buffer.Length);

            dest.Flush();
            dest.Close();
            dest.Dispose();


        }
        public void ExecuteTime(double rate, double time, string outputfilename)
        {
            if (_parser == null)
                return;
            double o_rate = _parser.Rate;
            if (o_rate < rate)
            {
                // 不需要转换
                return;
            }

            long filesize = _parser.Length;
            if (rate < 0)
                filesize += 27;
            uint offset;
            double duration;
            if (time < 0)
            {
                // 计算傲娇时间
                duration = filesize / 125.0 / rate; // * 8 / 1000 / rate
                offset = (uint)(duration * 1000 - _parser.Duration);
                //o_rate = rate;
            }
            else
            {
                duration = _parser.Duration / 1000.0 + time;
                offset = (uint)(time * 1000);
                //o_rate = filesize / 125.0 / duration;
            }
            string offstr = (offset / 1000.0).ToString("0.000");
            //开始处理傲娇

            Stream src = new FileStream(_filename, FileMode.Open);

            string path = outputfilename;

            Stream dest = new FileStream(path, FileMode.Create);
            WriteHead(dest, filesize, duration, 2.0, 2.0, -1, 1.0, 0 - offset,
                0, _parser.Tags.Count - 1, (rate < 0));
            // 傲娇的话才插入新帧
            if (rate < 0)
            {
                ushort width = 512, height = 384;
                //try
                //{
                //    string[] ss = toolComboFrame.Text.Split('x');
                //    if (ss.Length == 2)
                //    {
                //        width = ushort.Parse(ss[0]);
                //        height = ushort.Parse(ss[1]);
                //    }
                //}
                //catch { }
                byte[] buffer = GetH263Frame(0, width, height);
                dest.Write(buffer, 0, buffer.Length);
            }

            //WriteDataStream(src, parser.Tags[1].Offset - 11, dest);
            bool flag = true;
            for (int i = 1; i < _parser.Tags.Count; i++)
            {
                src.Seek(_parser.Tags[i].Offset - 11, SeekOrigin.Begin);
                FlvParser.FlvTag tag = _parser.Tags[i];
                byte[] bs = new byte[tag.DataSize < 4 ? 4 : tag.DataSize];
                src.Read(bs, 0, 4);
                dest.Write(bs, 0, 4);
                // 时间戳
                uint t = tag.TimeStamp;
                if (flag)
                {
                    FlvParser.VideoTag vtag = tag as FlvParser.VideoTag;
                    if (time < 0)
                    {
                        // 无傲娇进度条
                        if ((vtag != null) && (vtag.AVCPacketType == 1))
                        {
                            t += offset;
                            flag = false;
                        }
                        else
                        {
                            t = 0;
                        }
                    }
                    else
                    {
                        // 傲娇
                        if (vtag != null)
                        {
                            if (vtag.FrameType == "keyframe")
                            {
                                flag = false;
                            }
                            t = 0;
                        }
                        else
                        {
                            t += offset;
                        }
                    }
                }
                else
                {
                    t += offset;
                }
                PutTime(bs, 0, t);
                dest.Write(bs, 0, 4); // timestamp
                // 继续的数据
                src.Seek(4, SeekOrigin.Current);
                src.Read(bs, 0, 3); // streamid
                dest.Write(bs, 0, 3);
                src.Read(bs, 0, tag.DataSize);
                dest.Write(bs, 0, tag.DataSize);
                // prev tag size
                src.Read(bs, 0, 4);
                dest.Write(bs, 0, 4);
            }
            src.Close();
            src.Dispose();
            dest.Flush();
            dest.Close();
            dest.Dispose();


        }

        #region - Write 函数 -
        private int PutInt(byte[] dest, int pos, int val, int length)
        {
            if (length <= 0)
                return pos;
            for (int i = length - 1; i >= 0; i--)
            {
                dest[pos + i] = (byte)(val & 0xFF);
                val >>= 8;
            }
            return pos + length;
        }
        private int WriteString(byte[] dest, int pos, string str, bool type)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            if (type)
                dest[pos++] = 0x2;
            byte[] bs = Encoding.ASCII.GetBytes(str);
            pos = PutInt(dest, pos, bs.Length, 2);
            bs.CopyTo(dest, pos);
            pos += bs.Length;
            return pos;
        }
        private int WriteString(byte[] dest, int pos, string str)
        {
            return WriteString(dest, pos, str, false);
        }
        private int WriteDouble(byte[] dest, int pos, double val)
        {
            dest[pos++] = 0;
            byte[] bd = BitConverter.GetBytes(val);
            for (int i = 0; i < 8; i++)
            {
                dest[pos++] = bd[7 - i];
            }
            return pos;
        }
        private int WriteByte(byte[] dest, int pos, byte b)
        {
            dest[pos++] = 0x1;
            dest[pos++] = b;
            return pos;
        }

        private void WriteHead(Stream dest, long datasize, double duration, double vcodec, double acodec,
            double framerate, double x, uint offset_b, int f1, int f2, bool reserve)
        {
            int framecount = f2 - f1 + 1;
            if (framecount <= 0)
                throw new Exception("帧不能为空！");

            double audiosize = 0;
            double videosize = 0;
            double audiocodec = acodec;
            double videocodec = vcodec;
            double lasttimestamp = 0;
            double lastkeyframetimestamp = 0;
            double lastkeyframelocation = 0;
            List<double> filepositions = new List<double>();
            List<double> times = new List<double>();

            long first_offset = 0;
            bool res = reserve;
            for (int i = f1; i <= f2; i++)
            {
                if ((first_offset == 0) && !(_parser.Tags[i] is FlvParser.ScriptTag))
                {
                    first_offset = _parser.Tags[i].Offset;
                }
                FlvParser.AudioTag atag = _parser.Tags[i] as FlvParser.AudioTag;
                if (atag != null)
                {
                    if (audiocodec < 0)
                        audiocodec = atag.CodecId;
                    audiosize += atag.DataSize + 11;
                    continue;
                }
                FlvParser.VideoTag vtag = _parser.Tags[i] as FlvParser.VideoTag;
                if (vtag != null)
                {
                    if (videocodec < 0)
                        videocodec = vtag.CodecId;
                    videosize += vtag.DataSize + 11;
                    lasttimestamp = Math.Round((vtag.TimeStamp - offset_b) * x) / 1000.0;
                    if (vtag.FrameType == "keyframe")
                    {
                        if (res)
                        {
                            lasttimestamp = vtag.TimeStamp / 1000.0;
                            res = false;
                        }
                        lastkeyframetimestamp = lasttimestamp;
                        lastkeyframelocation = vtag.Offset - first_offset;
                        filepositions.Add(lastkeyframelocation);
                        times.Add(lastkeyframetimestamp);
                    }
                    continue;
                }
            }
            FlvParser.ScriptTag meta = _parser.MetaTag;

            byte[] bhead = new byte[] {
                0x46, 0x4c, 0x56, // FLV
                0x01,             // Version 1
                0x05,             // 0000 0101, 有音频有视频
                0, 0, 0, 0x09,    // Header size, 9
                0, 0, 0, 0,       // Previous Tag Size #0
            };
            int pos = 0;
            byte[] buffer = new byte[63356];
            buffer[pos++] = 0x12; // script
            #region - 开始写 -
            for (int i = 0; i < 10; i++)
            {
                buffer[pos++] = 0;
            }
            pos = WriteString(buffer, pos, "onMetaData", true);
            buffer[pos++] = 0x08;
            pos = PutInt(buffer, pos, 26, 4);

            object o;
            double d;

            pos = WriteString(buffer, pos, "creator");
            pos = WriteString(buffer, pos, "tsorgy.cnblogs.com", true);

            pos = WriteString(buffer, pos, "metadatacreator");
            pos = WriteString(buffer, pos, "Metadata creator - by Tsanie", true);

            pos = WriteString(buffer, pos, "hasKeyframes");
            pos = WriteByte(buffer, pos, 1);
            pos = WriteString(buffer, pos, "hasVideo");
            pos = WriteByte(buffer, pos, 1);
            pos = WriteString(buffer, pos, "hasAudio");
            pos = WriteByte(buffer, pos, 1);
            pos = WriteString(buffer, pos, "hasMetadata");
            pos = WriteByte(buffer, pos, 1);
            pos = WriteString(buffer, pos, "canSeekToEnd");
            pos = WriteByte(buffer, pos, 0);

            pos = WriteString(buffer, pos, "duration");
            pos = WriteDouble(buffer, pos, duration);
            pos = WriteString(buffer, pos, "datasize");
            pos = WriteDouble(buffer, pos, datasize);
            pos = WriteString(buffer, pos, "videosize");
            pos = WriteDouble(buffer, pos, videosize);
            pos = WriteString(buffer, pos, "videocodecid");
            pos = WriteDouble(buffer, pos, videocodec);

            pos = WriteString(buffer, pos, "width");
            d = 512.0;
            if (meta.TryGet("width", out o))
                d = (double)o;
            pos = WriteDouble(buffer, pos, d);

            pos = WriteString(buffer, pos, "height");
            d = 384.0;
            if (meta.TryGet("height", out o))
                d = (double)o;
            pos = WriteDouble(buffer, pos, d);

            pos = WriteString(buffer, pos, "framerate");
            d = framerate > 0 ? framerate : (framecount / duration);
            pos = WriteDouble(buffer, pos, d);

            pos = WriteString(buffer, pos, "videodatarate");
            pos = WriteDouble(buffer, pos, videosize / 125.0 / duration);

            pos = WriteString(buffer, pos, "audiosize");
            pos = WriteDouble(buffer, pos, audiosize);
            pos = WriteString(buffer, pos, "audiocodecid");
            pos = WriteDouble(buffer, pos, audiocodec);
            pos = WriteString(buffer, pos, "audiosamplerate");
            d = 44100;
            if (meta.TryGet("audiosamplerate", out o))
                d = (double)o;
            pos = WriteDouble(buffer, pos, d);
            pos = WriteString(buffer, pos, "audiosamplesize");
            d = 16;
            if (meta.TryGet("audiosamplesize", out o))
                d = (double)o;
            pos = WriteDouble(buffer, pos, d);
            pos = WriteString(buffer, pos, "stereo");
            byte stereo = 1;
            if (meta.TryGet("stereo", out o))
                stereo = (byte)o;
            pos = WriteByte(buffer, pos, stereo);
            pos = WriteString(buffer, pos, "audiodatarate");
            pos = WriteDouble(buffer, pos, audiosize / 125.0 / duration);

            pos = WriteString(buffer, pos, "filesize");
            int filesize_pos = pos;
            pos += 9;

            pos = WriteString(buffer, pos, "lasttimestamp");
            pos = WriteDouble(buffer, pos, lasttimestamp);
            pos = WriteString(buffer, pos, "lastkeyframetimestamp");
            pos = WriteDouble(buffer, pos, lastkeyframetimestamp);
            pos = WriteString(buffer, pos, "lastkeyframelocation");
            pos = WriteDouble(buffer, pos, lastkeyframelocation);
            #endregion
            pos = WriteString(buffer, pos, "keyframes");
            buffer[pos++] = 3; // object
            pos = WriteString(buffer, pos, "filepositions");
            int file_positions = pos;
            pos = WriteArray(buffer, pos, filepositions);
            pos = WriteString(buffer, pos, "times");
            pos = WriteArray(buffer, pos, times);
            buffer[pos++] = 0;
            buffer[pos++] = 0;
            buffer[pos++] = 9; // 结束符

            // script tag 长度
            PutInt(buffer, 1, pos - 11, 3); // script 帧的 datasize
            pos = PutInt(buffer, pos, pos, 4);
            WriteDouble(buffer, filesize_pos, datasize + pos + bhead.Length); // filesize
            WriteArray(buffer, file_positions, filepositions, pos + bhead.Length + (reserve ? 27 : 0));

            dest.Write(bhead, 0, bhead.Length);
            dest.Write(buffer, 0, pos);
        }
        private int WriteArray(byte[] dest, int pos, List<double> ds)
        {
            return WriteArray(dest, pos, ds, 0.0);
        }
        private int WriteArray(byte[] dest, int pos, List<double> ds, double offset)
        {
            dest[pos++] = 0xa;
            pos = PutInt(dest, pos, ds.Count, 4);
            for (int i = 0; i < ds.Count; i++)
            {
                pos = WriteDouble(dest, pos, ds[i] + offset);
            }
            return pos;
        }

        private void PutTime(byte[] bs, int pos, uint value)
        {
            for (int i = 2; i >= 0; i--)
            {
                bs[pos + i] = (byte)(value & 0xff);
                value >>= 8;
            }
            bs[pos + 3] = (byte)(value & 0xff);
        }
        private byte[] GetH263Frame(uint timestamp, ushort width, ushort height)
        {
            long b = (1 << 16) | width;
            b = (b << 16) | height;
            b <<= 7;
            byte[] buffer = new byte[]{
                    0x09, 0, 0, 0x0c, // 视频帧 12 字节
                    0, 0, 0, 0,       // timestamp & ex
                    0, 0, 0,          // stream id
                    0x22, 0, 0, 0x84, 0, // InnerFrame, H.263
                    0, 0, 0, 0, 0, 0x12, 0x26, // 16~20:width x height
                    0, 0, 0, 0x17     // 此帧长度 23 字节
                };
            PutTime(buffer, 4, timestamp);
            for (int i = 0; i < 5; i++)
            {
                buffer[20 - i] = (byte)(b & 0xFF);
                b >>= 8;
            }
            return buffer;
        }
        #endregion
    }//end class
}
