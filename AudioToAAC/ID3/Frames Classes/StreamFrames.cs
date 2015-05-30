using System;
using System.Collections.Generic;
using System.Text;
using ID3.ID3v2Frames;
using System.IO;

/*
 * This namespace contain Frames that is usefull for sending and recieving
 * mpeg files over streams. ex listening to audio from internet
 */
namespace ID3.ID3v2Frames.StreamFrames
{
    /// <summary>
    /// A class for PositionSynchronised frame
    /// </summary>
    public class PositionSynchronisedFrame : Frame
    {
        private TimeStamps _TimeStamp;
        private long _Position;

        internal PositionSynchronisedFrame(string FrameID, FrameFlags Flags, FileStream Data, int Length)
            : base(FrameID, Flags)
        {
            _TimeStamp = (TimeStamps)Data.ReadByte();
            if (!IsValidEnumValue(_TimeStamp, ValidatingErrorTypes.ID3Error))
            {
                _MustDrop = true;
                return;
            }
            Length--;

            byte[] Long = new byte[8];
            byte[] Buf = new byte[Length];

            Data.Read(Buf, 0, Length);
            Buf.CopyTo(Long, 8 - Buf.Length);
            Array.Reverse(Long);
            _Position = BitConverter.ToInt64(Long, 0);
        }

        /// <summary>
        /// create new PositionSynchronised frame
        /// </summary>
        /// <param name="Flags">Flags of frame</param>
        /// <param name="TimeStamp">TimeStamp to use for frame</param>
        /// <param name="Position">Position of frame</param>
        public PositionSynchronisedFrame(FrameFlags Flags, TimeStamps TimeStamp,
            long Position)
            : base("POSS", Flags)
        {
            this.TimeStamp = TimeStamp;
            _Position = Position;
        }

        /// <summary>
        /// Gets or sets current frame TimeStamp
        /// </summary>
        public TimeStamps TimeStamp
        {
            get
            { return _TimeStamp; }
            set
            {
                if (!Enum.IsDefined(typeof(TimeStamps), value))
                    throw (new ArgumentException("This is not valid value for TimeStamp"));

                _TimeStamp = value;
            }
        }

        /// <summary>
        /// Gets or sets current frame Position
        /// </summary>
        public long Position
        {
            get
            { return _Position; }
            set
            { _Position = value; }
        }

        #region -> Override Method and properties <-

        /// <summary>
        /// Indicate is current frame information availabe or not
        /// </summary>
        public override bool IsAvailable
        {
            get { return true; }
        }

        /// <summary>
        /// Gets MemoryStream For saving this Frame
        /// </summary>
        /// <returns>MemoryStream to save this frame</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);
            ms.WriteByte((byte)_TimeStamp);

            byte[] Buf;
            Buf = BitConverter.GetBytes(_Position);
            Array.Reverse(Buf);
            ms.Write(Buf, 0, 8);

            return ms;
        }

        /// <summary>
        /// Gets length of current frame
        /// </summary>
        public override int Length
        {
            get { return 9; }
        }

        #endregion
    }

    /// <summary>
    /// A class for RecomendedBufferSize Frame
    /// </summary>
    public class RecomendedBufferSizeFrame : Frame
    {
        protected uint _BufferSize;
        protected bool _EmbededInfoFlag;
        protected uint _OffsetToNextTag;

        /// <summary>
        /// Create new RecomendedBufferSize
        /// </summary>
        /// <param name="FrameID">Characters tag identifier</param>
        /// <param name="Flags">2 Bytes flags identifier</param>
        /// <param name="Data">Contain Data for this frame</param>
        /// <param name="Lenght">Length to read from FileStream</param>
        internal RecomendedBufferSizeFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _BufferSize = Data.ReadUInt(3);
            _EmbededInfoFlag = Convert.ToBoolean(Data.ReadByte());

            if (Length > 4)
                _OffsetToNextTag = Data.ReadUInt(4);
        }

        /// <summary>
        /// Create new RecomendedBufferSize
        /// </summary>
        /// <param name="Flags">Flags of frame</param>
        /// <param name="BufferSize">Recommended Buffer size</param>
        /// <param name="EmbededInfoFlag">EmbededInfoFlag</param>
        /// <param name="OffsetToNextTag">Offset to next tag</param>
        public RecomendedBufferSizeFrame(FrameFlags Flags, uint BufferSize,
            bool EmbededInfoFlag, uint OffsetToNextTag)
            : base("RBUF", Flags)
        {
            _BufferSize = BufferSize;
            _EmbededInfoFlag = EmbededInfoFlag;
            _OffsetToNextTag = OffsetToNextTag;
        }

        /// <summary>
        /// Gets or Sets Buffer size for current frame
        /// </summary>
        public uint BufferSize
        {
            get
            {
                return _BufferSize;
            }
            set
            {
                if (value > 0xFFFFFF)
                    throw (new ArgumentException("Buffer size can't be greater 16,777,215(0xFFFFFF)"));

                _BufferSize = value;
            }
        }

        /// <summary>
        /// Gets or Sets current frame EmbeddedInfoFlag
        /// </summary>
        public bool EmbededInfoFlag
        {
            get { return _EmbededInfoFlag; }
            set { _EmbededInfoFlag = value; }
        }

        /// <summary>
        /// Gets or Sets Offset to next tag
        /// </summary>
        public uint OffsetToNextTag
        {
            get
            {
                return _OffsetToNextTag;
            }
            set
            {
                _OffsetToNextTag = value;
            }
        }

        #region -> Override Method and properties <-

        /// <summary>
        /// Gets length of current frame
        /// </summary>
        public override int Length
        {
            get
            { 
                // 3: Buffer Size
                // 1: Info Flag
                // 4: Offset to next tag (if available)
                return 4 + (_OffsetToNextTag > 0 ? 4 : 0); 
            }
        }

        /// <summary>
        /// Gets MemoryStream to saving this Frame
        /// </summary>
        /// <returns>MemoryStream to save this frame</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            byte[] Buf;
            int Len = Length;
            MemoryStream ms = FrameHeader(MinorVersion);

            Buf = BitConverter.GetBytes(_BufferSize);
            Array.Reverse(Buf);
            ms.Write(Buf, 0, Buf.Length);

            ms.WriteByte(Convert.ToByte(_EmbededInfoFlag));

            if (_OffsetToNextTag > 0)
            {
                Buf = BitConverter.GetBytes(_OffsetToNextTag);
                Array.Reverse(Buf);
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
                if (_BufferSize != 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion
    }
}
