using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;
using ID3.ID3v2Frames.TextFrames;
using ID3.ID3v2Frames.BinaryFrames;

/*
 * This namespace contain frames that have array of information
 */
namespace ID3.ID3v2Frames.ArrayFrames
{
    /// <summary>
    /// A Class for frames that includes TextEncoding, Language, TimeStampFormat, ContentType and ContentDescriptor
    /// </summary>
    public class SynchronisedText : TermOfUseFrame
    {
        protected FramesCollection<Syllable> _Syllables;
        protected ContentTypes _ContentType;
        protected TimeStamps _TimeStamp;

        public enum ContentTypes
        {
            Other = 0,
            Lyric,
            TextTranscription,
            MovementOrPartName,
            Event,
            Chord,
            Trivia_PopupInfo
        }

        /// <summary>
        /// New SynchronisedText
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Flags">Frame Flag</param>
        /// <param name="Data">FileStream contain current frame data</param>
        internal SynchronisedText(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _Syllables = new FramesCollection<Syllable>();

            TextEncoding = (TextEncodings)Data.ReadByte();
            if (!IsValidEnumValue(TextEncoding, ValidatingErrorTypes.ID3Error))
                return;

            Length--;

            _Language = new Language(Data);
            Length -= 3;

            _TimeStamp = (TimeStamps)Data.ReadByte();
            if (!IsValidEnumValue(_TimeStamp, ValidatingErrorTypes.ID3Error))
                return;

            Length--;

            _ContentType = (ContentTypes)Data.ReadByte();
            if (!IsValidEnumValue(_ContentType, ValidatingErrorTypes.Nothing))
                _ContentType = ContentTypes.Other;
            Length--;

            // use Text variable for descriptor property
            ContentDescriptor = Data.ReadText(Length, TextEncoding, ref Length, true);

            string tempText;
            uint tempTime;
            while (Length > 5)
            {
                tempText = Data.ReadText(Length, TextEncoding, ref Length, true);
                tempTime = Data.ReadUInt(4);

                _Syllables.Add(new Syllable(tempTime, tempText));

                Length -= 4;
            }
        }

        /// <summary>
        /// New Synchronised Text
        /// </summary>
        /// <param name="Flags">Frame Flags</param>
        /// <param name="TextEncoding">TextEncoding use for texts</param>
        /// <param name="Lang">Language of texts</param>
        /// <param name="TimeStamp">TimeStamps that use for times</param>
        /// <param name="ContentType">ContentType</param>
        /// <param name="ContentDescriptor">Descriptor of Contents</param>
        public SynchronisedText(FrameFlags Flags,
            TextEncodings TextEncoding, string Lang, TimeStamps TimeStamp,
            ContentTypes ContentType, string ContentDescriptor)
            : base("SYLT", Flags)
        {
            _Syllables = new FramesCollection<Syllable>();

            this.ContentType = ContentType;
            this.TimeStamp = TimeStamp;
            this.TextEncoding = TextEncoding;
            Language = new Language(Lang);
            this.ContentDescriptor = ContentDescriptor;
        }

        /// <summary>
        /// Gets or Sets TimeStamp of current frame
        /// </summary>
        public TimeStamps TimeStamp
        {
            get { return _TimeStamp; }
            set
            {
                if (IsValidEnumValue(value, ValidatingErrorTypes.Exception))
                    _TimeStamp = value;
            }
        }

        /// <summary>
        /// Gets or sets ContentType of current frame
        /// </summary>
        public ContentTypes ContentType
        {
            get { return _ContentType; }
            set
            {
                if (IsValidEnumValue(value, ValidatingErrorTypes.Exception))
                    _ContentType = value;
            }
        }

        #region -> Override method and properties <-

        /// <summary>
        /// Gets Length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                // 3: Language
                // 1: Encoding
                // 2: TimeStamp And ContentType
                // Length of text (+ text seprator)
                // Foreach Syllable 4 byte Time
                // For each Syllable 1/2 byte seprator
                return 6 + GetTextLength(ContentDescriptor, TextEncoding, true)
                    + _Syllables.Length;
            }
        }

        /// <summary>
        /// Gets MemoryStream to save current frame
        /// </summary>
        /// <returns>Bytes for saving this frame</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            if (ID3v2.AutoTextEncoding)
                SetEncoding();

            ms.WriteByte((byte)TextEncoding); // Write Text Encoding

            _Language.Write(ms); // Write Language

            ms.WriteByte((byte)_TimeStamp);
            ms.WriteByte((byte)_ContentType);

            WriteText(ms, ContentDescriptor, TextEncoding, true);

            _Syllables.Sort(); // Sort Syllables

            byte[] Buf;
            foreach (Syllable sb in _Syllables)
            {
                WriteText(ms, sb.Text, TextEncoding, true);

                Buf = BitConverter.GetBytes(sb.Time);
                Array.Reverse(Buf);
                ms.Write(Buf, 0, 4);
            }

            return ms;
        }

        /// <summary>
        /// Indicate if current frame is available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (ContentDescriptor == null || _Syllables == null || _Syllables.Count == 0)
                    return false;

                return true;
            }
        }

        private void SetEncoding()
        {
            if (IsAscii(Text))
                foreach (Syllable Sb in Syllables)
                {
                    if (!IsAscii(Sb.Text))
                    {
                        TextEncoding = ID3v2.DefaultUnicodeEncoding;
                        break;
                    }
                }
            else
                TextEncoding = ID3v2.DefaultUnicodeEncoding;
        }

        #endregion

        /// <summary>
        /// Gets Syllables of current frame
        /// </summary>
        public FramesCollection<Syllable> Syllables
        {
            get
            { return _Syllables; }
        }

        /// <summary>
        /// This property is not available for SynchronisedTextFrame
        /// </summary>
        public new string Text
        {
            get
            { throw (new InvalidOperationException("Text is not available for Synchronised text")); }
        }

        /// <summary>
        /// Gets or Sets ContentDescriptor of current Frame
        /// </summary>
        public string ContentDescriptor
        {
            get
            { return base.Text; }
            set
            { base.Text = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            if (((SynchronisedText)obj)._Language == this._Language &&
                ((SynchronisedText)obj).ContentDescriptor == this.ContentDescriptor)
                return true;
            else
                return false;
        }

        public override int GetHashCode() { return base.GetHashCode(); }

        public override string ToString()
        {
            return ContentDescriptor + " [" + _Language + "]";
        }
    }

    /// <summary>
    /// Provide a class with Text and Time this class used SynchronisedText class and don't have any other usage
    /// </summary>
    public class Syllable : IComparable, ILengthable
    {
        protected string _Text;
        protected uint _Time;

        /// <summary>
        /// Create ne Syllable class
        /// </summary>
        /// <param name="Time">Absoulute Time for Syllable</param>
        /// <param name="Text">Text of Syllable</param>
        public Syllable(uint Time, string Text)
        {
            _Text = Text;
            _Time = Time;
        }

        /// <summary>
        /// Get/Set Text of current frame
        /// </summary>
        public string Text
        {
            get
            { return _Text; }
            set
            { _Text = value; }
        }

        /// <summary>
        /// Get/Set absolute time of current Syllable
        /// </summary>
        public uint Time
        {
            get
            { return _Time; }
            set
            { _Time = value; }
        }

        public int CompareTo(object obj)
        {
            if (this._Time > ((Syllable)obj)._Time)
                return 1;
            else if (this._Time < ((Syllable)obj)._Time)
                return -1;
            else
                return 0;
        }

        /// <summary>
        /// Gets length of current Syllable
        /// </summary>
        public int Length
        {
            get { return _Text.Length + 4; }
        }

        public override string ToString()
        {
            return _Time.ToString() + ":" + _Text;
        }
    }

    /// <summary>
    /// A class for frame that include TempCodes, TimeStampFormat
    /// </summary>
    public class SynchronisedTempoFrame : Frame
    {
        private FramesCollection<TempoCode> _TempoCodes;
        private TimeStamps _TimeStamp;

        /// <summary>
        /// Create new STempoCodes
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flags">2 Bytes flags identifier</param>
        /// <param name="Data">Contain Data for this frame</param>
        /// <param name="Length"></param>
        internal SynchronisedTempoFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _TempoCodes = new FramesCollection<TempoCode>();

            _TimeStamp = (TimeStamps)Data.ReadByte();
            if (IsValidEnumValue(_TimeStamp, ValidatingErrorTypes.ID3Error))
            {
                _MustDrop = true;
                return;
            }
            int Tempo;
            uint Time;

            while (Length > 4)
            {
                Tempo = Data.ReadByte();
                Length--;

                if (Tempo == 0xFF)
                {
                    Tempo += Data.ReadByte();
                    Length--;
                }

                Time = Data.ReadUInt(4);
                Length -= 4;

                _TempoCodes.Add(new TempoCode(Tempo, Time));
            }
        }

        public SynchronisedTempoFrame(FrameFlags Flags, TimeStamps TimeStamp)
            : base("SYTC", Flags)
        {
            _TempoCodes = new FramesCollection<TempoCode>();

            this.TimeStampFormat = TimeStamp;
        }

        /// <summary>
        /// Get/Set TimeStamp for current frame
        /// </summary>
        public TimeStamps TimeStampFormat
        {
            get
            { return _TimeStamp; }
            set
            {
                if (IsValidEnumValue(value, ValidatingErrorTypes.Exception))
                    _TimeStamp = value;
            }
        }

        /// <summary>
        /// Gets Collection of TempoCode for current frame
        /// </summary>
        public FramesCollection<TempoCode> TempoCodes
        {
            get { return _TempoCodes; }
        }

        #region -> Override Method and properties <-

        /// <summary>
        /// Gets Length of current frame
        /// </summary>
        public override int Length
        {
            get
            { return _TempoCodes.Length + 1; }
        }

        /// <summary>
        /// Indicate if this frame is available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (_TempoCodes.Count == 0)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Gets MemoryStream to save current frame
        /// </summary>
        /// <returns>MemoryStream contain frame information</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            byte[] Buf;
            MemoryStream ms = FrameHeader(MinorVersion);

            ms.WriteByte((byte)_TimeStamp);

            _TempoCodes.Sort();

            foreach (TempoCode TC in _TempoCodes.Items)
            {
                Buf = TC.Data();
                ms.Write(Buf, 0, Buf.Length);
            }

            return ms;
        }

        #endregion
    }

    /// <summary>
    /// Provide Tempo for STempoCodes
    /// </summary>
    public class TempoCode : IComparable, ILengthable
    {
        protected int _Tempo;
        protected uint _Time;

        /// <summary>
        /// Create new TempoCode
        /// </summary>
        /// <param name="Tempo">Tempo for current frame</param>
        /// <param name="TimeStamp">Time for current frame</param>
        public TempoCode(int Tempo, uint Time)
        {
            _Tempo = Tempo;
            _Time = Time;
        }

        /// <summary>
        /// Get/Set current Tempo
        /// </summary>
        public int Tempo
        {
            get
            { return _Tempo; }
            set
            {
                if (value > 510 || value < 2)
                    throw (new ArgumentException("Tempo must be between 2-510"));

                _Tempo = value;
            }
        }

        /// <summary>
        /// Get/Set Current frame time
        /// </summary>
        public uint Time
        {
            get
            { return _Time; }
            set
            { _Time = value; }
        }

        /// <summary>
        /// Get byte of information for current TempoCode
        /// </summary>
        /// <returns>byte array contain TempoCode data</returns>
        internal byte[] Data()
        {
            byte[] Buf = new byte[Length];
            int c = 0;
            if (_Tempo > 0xFF)
            {
                Buf[c++] = 0xFF;
                Buf[c++] = Convert.ToByte(_Tempo - 0xFF);
            }
            else
                Buf[c++] = Convert.ToByte(_Tempo);

            byte[] B = BitConverter.GetBytes(_Time);
            Array.Reverse(B);
            Array.Copy(B, 0, Buf, c, 4);

            return Buf;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(TempoCode))
                return false;

            if (((TempoCode)obj)._Time == this._Time)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (Equals(obj))
                return 0;
            else if (this._Time > ((TempoCode)obj)._Time)
                return 1;
            else
                return -1;
        }

        /// <summary>
        /// Gets length of current frame
        /// </summary>
        public int Length
        {
            get
            {
                if (Tempo > 0xFF)
                    return 6;
                else
                    return 5;
            }
        }

        public override string ToString()
        {
            return _Tempo.ToString() + ":" + _Time.ToString();
        }
    }

    /// <summary>
    /// Provide a class for Equalisation frame
    /// </summary>
    public class Equalisation : Frame
    {
        protected byte _AdjustmentBits;
        protected FramesCollection<FrequencyAdjustmentFrame> _Frequensies;

        /// <summary>
        /// Create new Equalisation frame
        /// </summary>
        /// <param name="FrameID"></param>
        /// <param name="Flags"></param>
        /// <param name="Data"></param>
        /// <param name="Length"></param>
        internal Equalisation(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _Frequensies = new FramesCollection<FrequencyAdjustmentFrame>();

            _AdjustmentBits = Data.ReadByte();
            Length--;

            if (_AdjustmentBits == 0)
            {
                ErrorOccur("Adjustment bit of Equalisation is zero. this frame is invalid", true);
                return;
            }

            if (_AdjustmentBits % 8 != 0 || _AdjustmentBits > 32)
            {
                ErrorOccur("AdjustmentBit of Equalisation Frame is out of supported range of this program", true);
                return;
            }

            int AdLen = _AdjustmentBits / 8;

            int FreqBuf;
            uint AdjBuf;
            while (Length > 3)
            {
                FreqBuf = Convert.ToInt32(Data.ReadUInt(2));

                AdjBuf = Data.ReadUInt(AdLen);
                _Frequensies.Add(new FrequencyAdjustmentFrame(FreqBuf, AdjBuf));

                Length -= 2 + AdLen;
            }
        }

        public Equalisation(FrameFlags Flags, byte AdjustmentBits)
            : base("EQUA", Flags)
        {
            this.AdjustmentLength = AdjustmentBits;

            _Frequensies = new FramesCollection<FrequencyAdjustmentFrame>();
        }

        /// <summary>
        /// Get/Set Adjustment length in bit
        /// </summary>
        public byte AdjustmentLength
        {
            get
            {
                return _AdjustmentBits;
            }
            set
            {
                if (value == 0 || value % 8 != 0 || value > 32)
                    throw (new ArgumentOutOfRangeException("Adjustment bits must be in range of 8 - 32 and be multiple of 8"));

                _AdjustmentBits = value;
            }
        }

        /// <summary>
        /// Get All frequencis
        /// </summary>
        /// <returns>frequencis array</returns>
        public FramesCollection<FrequencyAdjustmentFrame> Frequencies
        {
            get
            { return _Frequensies; }
        }

        #region -> Overide Methods <-

        /// <summary>
        /// Gets length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                int RLen = 0;
                RLen = _Frequensies.Count * (1 + (_AdjustmentBits / 8));
                return RLen + 1;
            }
        }

        /// <summary>
        /// Gets MemoryStream to save current frame
        /// </summary>
        /// <returns>MemoryStream contain current frame data</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);
            byte[] Buf;

            ms.WriteByte(_AdjustmentBits);

            foreach (FrequencyAdjustmentFrame FA in _Frequensies.Items)
            {
                Buf = FA.GetBytes(_AdjustmentBits);
                ms.Write(Buf, 0, Buf.Length);
            }

            return ms;
        }

        /// <summary>
        /// Indicate if this frame is available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (_Frequensies.Count == 0)
                    return false;
                else
                    return true;
            }
        }

        #endregion
    }

    /// <summary>
    /// Provide a class for frequency frames. containing Inc/Dec, Frequency, Adjustment
    /// </summary>
    public class FrequencyAdjustmentFrame : ILengthable, IComparable
    {
        protected IncrementDecrement _IncDec;
        protected int _Frequency;
        protected uint _Adjustment;

        /// <summary>
        /// Create new FrequencyAdjustment Frame
        /// </summary>
        /// <param name="Frequency">Frequency with inc/dec bit</param>
        /// <param name="Adjustment">Adjustment</param>
        internal FrequencyAdjustmentFrame(int Frequency, uint Adjustment)
        {
            _IncDec = (IncrementDecrement)Convert.ToByte(Frequency & 0x8000);
            Frequency &= 0x7FFF;

            _Adjustment = Adjustment;
            _Frequency = Frequency;
        }

        /// <summary>
        /// Create new FrequencyAdjustment frame
        /// </summary>
        /// <param name="IncDec">Increment/Decrement</param>
        /// <param name="Frequency">Frequency</param>
        /// <param name="Adjustment">Adjustment</param>
        public FrequencyAdjustmentFrame(IncrementDecrement IncDec
            , int Frequency, uint Adjustment)
        {
            _IncDec = IncDec;
            this.Frequency = Frequency;
            _Adjustment = Adjustment;
        }

        /// <summary>
        /// Get/Set Frequency of current FrequencyAdjustmentFrame
        /// </summary>
        public int Frequency
        {
            get
            { return _Frequency; }
            set
            {
                if (value > 0x7FFF || value < 0)
                    throw (new ArgumentException("Frequency value must be between 0 - 32767 Hz"));

                _Frequency = value;
            }
        }

        /// <summary>
        /// Get/Set Adjustment for current Frequency frame
        /// </summary>
        public uint Adjustment
        {
            get { return _Adjustment; }
            set { _Adjustment = value; }
        }

        /// <summary>
        /// Convert current Frequency adjustment to byte array
        /// </summary>
        /// <returns></returns>
        internal byte[] GetBytes(int AdjustmentBits)
        {
            int AdByte = AdjustmentBits / 8;
            byte[] Buf = new byte[AdByte + 2];
            byte[] Temp;
            int AddFreq = _Frequency;

            if (_IncDec == IncrementDecrement.Increment)
                AddFreq |= 0xFFFF;

            Temp = BitConverter.GetBytes(AddFreq);
            Array.Reverse(Temp);

            Array.Copy(Temp, 0, Buf, 0, 2);

            Temp = BitConverter.GetBytes(_Adjustment);
            Array.Reverse(Temp);
            Array.Copy(Temp, 0, Buf, 0, AdByte);

            return Buf;
        }

        // Length of Frequencies must calculate in FrequencyFrame
        public int Length
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public int CompareTo(object obj)
        {
            return this._Frequency - ((FrequencyAdjustmentFrame)obj)._Frequency;
        }
    }

    /// <summary>
    /// A class for EventTimingCode frame
    /// </summary>
    public class EventTimingCodeFrame : Frame
    {
        private TimeStamps _TimeStamp;
        private FramesCollection<EventCode> _Events;

        internal EventTimingCodeFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _Events = new FramesCollection<EventCode>();

            _TimeStamp = (TimeStamps)Data.ReadByte();
            if (!IsValidEnumValue(_TimeStamp, ValidatingErrorTypes.ID3Error))
            {
                _MustDrop = true;
                return;
            }
            Length--;

            while (Length >= 5)
            {
                _Events.Add(new EventCode(Data.ReadByte(), Data.ReadUInt(4)));

                Length -= 5;
            }
        }

        public TimeStamps TimeStamp
        {
            get
            { return _TimeStamp; }
            set
            {
                if (IsValidEnumValue(value, ValidatingErrorTypes.Exception))
                    _TimeStamp = value;
            }
        }

        /// <summary>
        /// Create new EventTimingCode frame
        /// </summary>
        /// <param name="Flags">Flags of frame</param>
        /// <param name="TimeStamp">TimeStamp use for times</param>
        public EventTimingCodeFrame(FrameFlags Flags, TimeStamps TimeStamp)
            : base("ETCO", Flags)
        {
            this.TimeStamp = TimeStamp;
        }

        /// <summary>
        /// Gets all Events for current frame
        /// </summary>
        public FramesCollection<EventCode> Events
        {
            get { return _Events; }
        }

        #region -> Override Methods <-

        /// <summary>
        /// Indicate if current frame is available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (_Events.Count == 0)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// Gets MemoryStream to save current frame
        /// </summary>
        /// <returns>MemoryStream contain current frame data</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            _Events.Sort();
            byte[] Buf;
            MemoryStream ms = FrameHeader(MinorVersion);

            ms.WriteByte((byte)_TimeStamp);

            foreach (EventCode EC in _Events.Items)
            {
                ms.WriteByte((byte)EC.EventType);
                Buf = BitConverter.GetBytes(EC.Time);
                Array.Reverse(Buf);
                ms.Write(Buf, 0, Buf.Length);
            }

            return ms;
        }

        /// <summary>
        /// Gets length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                return _Events.Count * 5 + 1;
                // 1: TimeStamp
            }
        }

        #endregion
    }

    /// <summary>
    /// A class for Event Codes
    /// </summary>
    public class EventCode : ILengthable
    {
        private byte _EventType;
        private uint _Time;

        /// <summary>
        /// Create new EventCode
        /// </summary>
        /// <param name="EventType">Event Type</param>
        /// <param name="Time">Time of Event</param>
        public EventCode(byte EventType, uint Time)
        {
            _EventType = EventType;
            _Time = Time;
        }

        /// <summary>
        /// Gets or sets Event type
        /// </summary>
        public byte EventType
        {
            get
            { return _EventType; }
            set
            { _EventType = value; }
        }

        /// <summary>
        /// Gets or set Time of event
        /// </summary>
        public uint Time
        {
            get
            { return _Time; }
            set
            { _Time = value; }
        }

        /// <summary>
        /// Gets length of EventCode
        /// </summary>
        public int Length
        {
            get { return 5; }
        }

        public override string ToString()
        {
            return _Time.ToString() + ":" + _EventType.ToString();
        }
    }
}
