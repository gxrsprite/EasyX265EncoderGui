using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ID3.ID3v2Frames.OtherFrames
{
    /// <summary>
    /// Provide a class for Reverb frame
    /// </summary>
    public class ReverbFrame : Frame
    {
        private int _ReverbLeft;
        private int _ReverbRight;
        private byte _ReverbBouncesLeft;
        private byte _ReverbBouncesRight;
        private byte _ReverbFeedbackLeftToRight;
        private byte _ReverbFeedbackRightToLeft;
        private byte _ReverbFeedbackRightToRight;
        private byte _ReverbFeedbackLeftToLeft;
        private byte _PremixLeftToRight;
        private byte _PremixRightToLeft;

        /// <summary>
        /// Create new reveb frame
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Flags">Flags of frame</param>
        /// <param name="Data">Data for frame to read from</param>
        /// <param name="Length">Maximum length of frame</param>
        internal ReverbFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            if (Length != 12)
            {
                //RaiseErrorEvent(new ID3Error(208, ID3Versions.ID3v2, _FrameID,
                //"Reveb frame is not in correct length. it will drop", ErrorType.Error));
                _MustDrop = true;
                return;
            }

            _ReverbLeft = Convert.ToInt32(Data.ReadUInt(2));
            _ReverbRight = Convert.ToInt32(Data.ReadUInt(2));
            _ReverbBouncesLeft = Data.ReadByte();
            _ReverbBouncesRight = Data.ReadByte();
            _ReverbFeedbackLeftToLeft = Data.ReadByte();
            _ReverbFeedbackLeftToRight = Data.ReadByte();
            _ReverbFeedbackRightToRight = Data.ReadByte();
            _ReverbFeedbackRightToLeft = Data.ReadByte();
            _PremixLeftToRight = Data.ReadByte();
            _PremixRightToLeft = Data.ReadByte();
        }

        /// <summary>
        /// Create new Reverb frame and set all values to zero
        /// </summary>
        /// <param name="Flags">Frame Flags</param>
        public ReverbFrame(FrameFlags Flags)
            : base("RVRB", Flags) { }

        #region -> Public properties <-

        /// <summary>
        /// ReverbLeft of current Reveb frame
        /// </summary>
        public int ReverbLeft
        {
            get
            { return _ReverbLeft; }
            set
            { _ReverbLeft = value; }
        }

        /// <summary>
        /// ReverbRight of current reverb frame
        /// </summary>
        public int ReverbRight
        {
            get
            { return _ReverbRight; }
            set
            { _ReverbRight = value; }
        }

        /// <summary>
        /// ReverbBouncesLeft of current reverb frame
        /// </summary>
        public byte ReverbBouncesLeft
        {
            get
            { return _ReverbBouncesLeft; }
            set
            { _ReverbBouncesLeft = value; }
        }

        /// <summary>
        /// ReverbBouncesRight of current reverb frame
        /// </summary>
        public byte ReverbBouncesRight
        {
            get
            { return _ReverbBouncesRight; }
            set
            { _ReverbBouncesRight = value; }
        }

        /// <summary>
        /// ReverbFeedbackLeftToRight of current reverb frame
        /// </summary>
        public byte ReverbFeedbackLeftToRight
        {
            get
            { return _ReverbFeedbackLeftToRight; }
            set
            { _ReverbFeedbackLeftToRight = value; }
        }

        /// <summary>
        /// ReverbFeedbackRightToLeft of current reverb frame
        /// </summary>
        public byte ReverbFeedbackRightToLeft
        {
            get
            { return _ReverbFeedbackRightToLeft; }
            set
            { _ReverbFeedbackRightToLeft = value; }
        }

        /// <summary>
        /// ReverbFeedbackRightToRight of current reverb frame
        /// </summary>
        public byte ReverbFeedbackRightToRight
        {
            get
            { return _ReverbFeedbackRightToRight; }
            set
            { _ReverbFeedbackRightToRight = value; }
        }

        /// <summary>
        /// ReverbFeedbackLeftToLeft of current reverb frame
        /// </summary>
        public byte ReverbFeedbackLeftToLeft
        {
            get
            { return _ReverbFeedbackLeftToLeft; }
            set
            { _ReverbFeedbackLeftToLeft = value; }
        }

        /// <summary>
        /// PremixLeftToRight of current reverb frame
        /// </summary>
        public byte PremixLeftToRight
        {
            get
            { return _PremixLeftToRight; }
            set
            { _PremixLeftToRight = value; }
        }

        /// <summary>
        /// PremixRightToLeft of current reverb frame
        /// </summary>
        public byte PremixRightToLeft
        {
            get
            { return _PremixRightToLeft; }
            set
            { _PremixRightToLeft = value; }
        }

        #endregion

        #region -> Override Method and properties <-

        /// <summary>
        /// Get stream of current fram to write
        /// </summary>
        /// <returns>MemoryStream according to current frame</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);
            byte[] Buf;

            Buf = BitConverter.GetBytes(_ReverbLeft);
            Array.Reverse(Buf);
            ms.Write(Buf, 2, 2);

            Buf = BitConverter.GetBytes(_ReverbRight);
            Array.Reverse(Buf);
            ms.Write(Buf, 2, 2);

            ms.WriteByte(_ReverbBouncesLeft);
            ms.WriteByte(_ReverbBouncesRight);
            ms.WriteByte(_ReverbFeedbackLeftToLeft);
            ms.WriteByte(_ReverbFeedbackLeftToRight);
            ms.WriteByte(_ReverbFeedbackRightToRight);
            ms.WriteByte(_ReverbFeedbackRightToLeft);
            ms.WriteByte(_PremixLeftToRight);
            ms.WriteByte(_PremixRightToLeft);

            return ms;
        }

        /// <summary>
        /// Indicate if this frame available
        /// </summary>
        public override bool IsAvailable
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        /// <summary>
        /// Gets Length of current frame
        /// </summary>
        public override int Length
        {
            get { return 12; }
        }

        #endregion
    }

    //TODO: Not Complete
    public class MpegLocationLookupTable : Frame
    {
        protected uint _FrameBetweenRef; // 2 Bytes
        protected uint _ByteBetweenRef; // 3 Bytes
        protected uint _MilisecondBetweenRef; // 3 Bytes
        protected byte _BitsForByteDeviation;
        protected byte _BitsForMilisecondDeviation;

        /// <summary>
        /// Create new MpegLocationLookupTable
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Flags">Flags for this frame</param>
        /// <param name="Data">FileStream to read data from</param>
        internal MpegLocationLookupTable(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _FrameBetweenRef = Data.ReadUInt(2);
            _ByteBetweenRef = Data.ReadUInt(3);
            _ByteBetweenRef = Data.ReadUInt(3);
            _BitsForByteDeviation = Data.ReadByte();
            _BitsForMilisecondDeviation = Data.ReadByte();
            Length -= 10;
            int Sum = _BitsForByteDeviation + _BitsForMilisecondDeviation;

            ////BitForByteDev + BitForMilisecondDev must be multiple of four
            //if (Sum % 4 != 0)
            //{
            //    RaiseErrorEvent(new ID3Error(208, ID3Versions.ID3v2, _FrameID,
            //        "Error in MpegLocationLookupTable, it's not standard. it will drop", ErrorType.Error));
            //    _MustDrop = true;
            //    return;
            //}

            //if (Sum > 32 || Sum % 8 != 0)
            //{
            //    RaiseErrorEvent(new ID3Error(208, ID3Versions.ID3v2, _FrameID,
            //        "this program can't process MpegLocation Table", ErrorType.Error));
            //    _MustDrop = true;
            //    return;
            //}

            uint Temp;
            Sum /= 8;
            while (Length >= Sum)
            {
                Temp = Data.ReadUInt(Sum);

                Length -= Sum;
            }
        }

        #region -> Properties <-

        /// <summary>
        /// Indicate number of Mpeg Frames between each reffrence
        /// </summary>
        public uint FramesBetweenReferences
        {
            get
            { return _FrameBetweenRef; }
            set
            {
                if (value < 0)
                    throw (new ArgumentOutOfRangeException("FramesBetween References can't be less than zero"));


                if (value > 0xFFFF)
                    throw (new ArgumentOutOfRangeException("Maximum value for frames between references is 65,535(0x FF FF)"));

                _FrameBetweenRef = value;
            }
        }

        /// <summary>
        /// Indicate number of bytes between each references
        /// </summary>
        public uint ByteBetweenReferences
        {
            get
            { return _ByteBetweenRef; }
            set
            {
                if (value < 0)
                    throw (new ArgumentOutOfRangeException("Bytes Between References can't be less than zero"));

                if (value > 0xFFFFFF)
                    throw (new ArgumentOutOfRangeException("Maximum value for frames between references is 16,777,215(0x FF FF FF)"));

                _ByteBetweenRef = value;
            }
        }

        /// <summary>
        /// Indicate Miliseconds Between References
        /// </summary>
        public uint MilisecondsBetweenReferences
        {
            get
            { return _MilisecondBetweenRef; }
            set
            {
                if (value < 0)
                    throw (new ArgumentOutOfRangeException("Miliseconds Between References can't be less than zero"));

                if (value > 0xFFFFFF)
                    throw (new ArgumentOutOfRangeException("Maximum value for Miliseconds between references is 16,777,215(0x FF FF FF)"));

                _MilisecondBetweenRef = value;
            }
        }

        /// <summary>
        /// Indicate howmany bits use for ByteDeviation
        /// </summary>
        public byte BitsForByteDeviation
        {
            get
            { return _BitsForByteDeviation; }
            set
            { _BitsForByteDeviation = value; }
        }

        /// <summary>
        /// Indicate howmany bit use for Milisecond Deviation
        /// </summary>
        public byte BitsForMilisecondDeviation
        {
            get
            { return _BitsForMilisecondDeviation; }
            set
            { _BitsForMilisecondDeviation = value; }
        }

        #endregion

        /// <summary>
        /// Indicate if this frame available
        /// </summary>
        public override bool IsAvailable
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override MemoryStream FrameStream(int MinorVersion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override int Length
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
    }

    /// <summary>
    /// A Class for frames that include Counter
    /// </summary>
    public class PlayCounterFrame : Frame
    {
        protected long _Counter;

        /// <summary>
        /// Create new PlayCounter
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flags">2 Bytes flags identifier</param>
        /// <param name="Data">Contain Data for this frame</param>
        internal PlayCounterFrame(string FrameID, FrameFlags Flags, FileStream Data, int Length)
            : base(FrameID, Flags)
        {
            byte[] Long = new byte[8];
            byte[] Buf = new byte[Length];
            // Less than 4 Characters
            Data.Read(Buf, 0, Length);
            Buf.CopyTo(Long, 8 - Buf.Length);
            Array.Reverse(Long);
            _Counter = BitConverter.ToInt64(Long, 0);
        }

        /// <summary>
        /// Create new PlayCounter
        /// </summary>
        /// <param name="Flags">Flags of frame</param>
        public PlayCounterFrame(FrameFlags Flags)
            : base("PCNT", Flags) { }

        /// <summary>
        /// Add one to counter
        /// </summary>
        public void AddOne()
        { Counter++; }

        /// <summary>
        /// Gets or Sets Counter of current PlayCounter
        /// </summary>
        public long Counter
        {
            get
            { return _Counter; }
            set
            {
                if (value < 0)
                    throw (new ArgumentException("Counter value can't be less than zero"));

                _Counter = value;
            }
        }

        #region -> Override Methods and properties <-

        /// <summary>
        /// Get Length of current PlayCounter
        /// </summary>
        public override int Length
        {
            get
            {
                // The Length of counter can't be less than 4 (32bit)
                // In this program we always save it in 8 byte (64bit value)
                // 8: Long value (Counter)
                return 8;
            }
        }

        /// <summary>
        /// Get bytes for saving this frame
        /// </summary>
        /// <returns>Bytes for saving this frame</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);
            byte[] Buf;
            Buf = BitConverter.GetBytes(_Counter);
            Array.Reverse(Buf);
            ms.Write(Buf, 0, 8);

            return ms;
        }

        /// <summary>
        /// Indicate if this frame available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (_Counter <= 0)
                    return false;

                return true;
            }
        }

        #endregion
    }

    /// <summary>
    /// A class for RelativeVolumeAdjustment
    /// </summary>
    public class RelativeVolumeFrame : Frame
    {
        protected byte _IncDec; // Increment Decrement Byte

        protected byte _BitForVolumeDescription;
        // All volume descriptors are 12 items we store them in a array
        protected uint[] _Descriptors;

        /// <summary>
        /// Create new RaltiveVolumeFrame
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flags">2 Bytes flags identifier</param>
        /// <param name="Data">Contain Data for this frame</param>
        /// <param name="Lenght">Length to read from FileStream</param>
        internal RelativeVolumeFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _Descriptors = new uint[12];

            _IncDec = Data.ReadByte(); // Read Increment Decrement Byte

            _BitForVolumeDescription = Data.ReadByte(); // Read Volume Description Length
            Length -= 2;

            if (_BitForVolumeDescription == 0)
            {
                ErrorOccur("BitForVolumeDescription of Relative volume information frame can't be zero", true);
                return;
            }

            if (_BitForVolumeDescription / 8 > 4 ||
                _BitForVolumeDescription % 8 != 0)
            {
                ErrorOccur("This program don't support " + _BitForVolumeDescription.ToString() +
                    " Bits of description for Relative Volume information", true);
                return;
            }

            int DesLen = _BitForVolumeDescription / 8; // Length of each volume descriptor
            int Counter = 0;
            while (CanContinue(Length, DesLen, 2))
            {
                _Descriptors[Counter++] = Data.ReadUInt(DesLen);
                _Descriptors[Counter++] = Data.ReadUInt(DesLen);
                Length -= 2;
            }
        }

        /// <summary>
        /// Create new RelativeVolumeInformation class
        /// </summary>
        /// <param name="Flags">Flags of Frame</param>
        /// <param name="IncDec">Increment Decrement for each channel</param>
        /// <param name="BitForVolumeDescription">Length of volume description</param>
        public RelativeVolumeFrame(FrameFlags Flags, byte IncDec,
            byte BitForVolumeDescription)
            : base("RVAD", Flags)
        {
            _Descriptors = new uint[12];

            _IncDec = IncDec;
            this.BitsForVolumeDescription = BitForVolumeDescription;
        }

        /// <summary>
        /// Indicate if reading data can continue
        /// </summary>
        /// <param name="MaxLength">Maximum available length</param>
        /// <param name="DesLen">Length of each Volume Descriptor</param>
        /// <param name="BlockToRead">How many descriptors want to read</param>
        /// <returns>true if data is availabe otherwise false</returns>
        private bool CanContinue(int MaxLength, int DesLen, int BlockToRead)
        {
            if (MaxLength >= DesLen * BlockToRead)
                return true;

            return false;
        }

        #region -> IncrementDecrement Properties <-

        public IncrementDecrement Right
        {
            get
            { return (IncrementDecrement)(_IncDec & 1); }
            set
            {
                if (value == IncrementDecrement.Increment)
                    _IncDec |= 1; // 00000001
                else
                    _IncDec &= 254; // 11111110
            }
        }

        public IncrementDecrement Left
        {
            get
            { return (IncrementDecrement)(_IncDec & 2); }
            set
            {
                if (value == IncrementDecrement.Increment)
                    _IncDec |= 2; // 00000010
                else
                    _IncDec &= 253; // 11111101
            }
        }

        public IncrementDecrement RightBack
        {
            get
            { return (IncrementDecrement)(_IncDec & 4); }
            set
            {
                if (value == IncrementDecrement.Increment)
                    _IncDec |= 4; // 00000100
                else
                    _IncDec &= 251; // 11111011
            }
        }

        public IncrementDecrement LeftBack
        {
            get
            { return (IncrementDecrement)(_IncDec & 8); }
            set
            {
                if (value == IncrementDecrement.Increment)
                    _IncDec |= 8; // 00001000
                else
                    _IncDec &= 247; // 11110111
            }
        }

        public IncrementDecrement Center
        {
            get
            { return (IncrementDecrement)(_IncDec & 16); }
            set
            {
                if (value == IncrementDecrement.Increment)
                    _IncDec |= 16; // 00010000
                else
                    _IncDec &= 239; // 11101111
            }
        }

        public IncrementDecrement Bass
        {
            get
            { return (IncrementDecrement)(_IncDec & 32); }
            set
            {
                if (value == IncrementDecrement.Increment)
                    _IncDec |= 32; // 00100000
                else
                    _IncDec &= 223; // 11011111
            }
        }

        #endregion

        #region -> Volumes Properties <-

        public uint RelativeVolumeChangeRight
        {
            get
            { return (uint)_Descriptors[0]; }
            set
            { _Descriptors[0] = value; }
        }

        public uint RelativeVolumeChangeLeft
        {
            get
            { return (uint)_Descriptors[1]; }
            set
            { _Descriptors[1] = value; }
        }

        public uint PeakVolumeRight
        {
            get
            { return (uint)_Descriptors[2]; }
            set
            { _Descriptors[2] = value; }
        }

        public uint PeakVolumeLeft
        {
            get
            { return (uint)_Descriptors[3]; }
            set
            { _Descriptors[3] = value; }
        }

        public uint RelativeVolumeChangeRightBack
        {
            get
            { return (uint)_Descriptors[4]; }
            set
            { _Descriptors[4] = value; }
        }

        public uint RelativeVolumeChangeLeftBack
        {
            get
            { return (uint)_Descriptors[5]; }
            set
            { _Descriptors[5] = value; }
        }

        public uint PeakVolumeRightBack
        {
            get
            { return (uint)_Descriptors[6]; }
            set
            { _Descriptors[6] = value; }
        }

        public uint PeakVolumeLeftBack
        {
            get
            { return (uint)_Descriptors[7]; }
            set
            { _Descriptors[7] = value; }
        }

        public uint RelativeVolumeChangeCenter
        {
            get
            { return (uint)_Descriptors[8]; }
            set
            { _Descriptors[8] = value; }
        }

        public uint PeakVolumeCenter
        {
            get
            { return (uint)_Descriptors[9]; }
            set
            { _Descriptors[9] = value; }
        }

        public uint RelativeVolumeChangeBass
        {
            get
            { return (uint)_Descriptors[10]; }
            set
            { _Descriptors[10] = value; }
        }

        public uint PeakVolumeBass
        {
            get
            { return (uint)_Descriptors[11]; }
            set
            { _Descriptors[11] = value; }
        }

        #endregion

        /// <summary>
        /// Indicate how many bits used for volume descripting (usually 16)
        /// </summary>
        public byte BitsForVolumeDescription
        {
            get
            {
                return _BitForVolumeDescription;
            }
            set
            {
                if (value % 8 != 0 || value > 32 || value < 8)
                    throw (new ArgumentException("Need multiple of 8 number between 8 to 32."));

                _BitForVolumeDescription = value;
            }
        }

        /// <summary>
        /// Indicate how many bytes need to descripting volume
        /// </summary>
        private int ByteFotVoulmeDescription
        {
            get
            { return (_BitForVolumeDescription / 8); }
        }

        /// <summary>
        /// Convert a uint to byte array
        /// </summary>
        /// <param name="Num">number to convert</param>
        /// <param name="ArrayLength">length of return array</param>
        /// <returns>byte array contain information of num</returns>
        private byte[] ToByteArray(uint Num, int ArrayLength)
        {
            byte[] Buf, R;
            Buf = BitConverter.GetBytes(Num);
            R = new byte[ArrayLength];
            Array.Copy(Buf, 0, R, 0, ArrayLength);
            Array.Reverse(R);
            return R;
        }

        #region -> Override Methods and properties <-

        /// <summary>
        /// Get the length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                // 1: Increment/Decrement
                // 1: BitUsedForVolumeDescription
                // 12: Volume Descriptors
                return 2 + (12 * ByteFotVoulmeDescription);
            }
        }

        /// <summary>
        /// Get bytes for saving current frame
        /// </summary>
        /// <returns>Bytes for saving this frame</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            byte[] Buf;
            int DesLength = ByteFotVoulmeDescription;
            MemoryStream ms = FrameHeader(MinorVersion);

            ms.WriteByte(_IncDec); // Write Increment/Decrement
            ms.WriteByte(_BitForVolumeDescription); // Write Bits For volume descripting

            for (int i = 0; i < 12; i++)
            {
                Buf = ToByteArray((uint)_Descriptors[i], DesLength);
                ms.Write(Buf, 0, DesLength);
            }

            return ms;
        }

        /// <summary>
        /// Indicate if this frame available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (_Descriptors == null || _Descriptors.Length == 0)
                    return false;
                return true;
            }
        }

        #endregion
    }
}