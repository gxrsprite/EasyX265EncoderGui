using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ID3.ID3v2Frames.TextFrames;

/*
 * This namespace contain frames that have binary information
 * like: pictures, files and etc
 * for storing Binary information in all classes i used MemoryStream
 */
namespace ID3.ID3v2Frames.BinaryFrames
{
    /// <summary>
    /// A class for frame that only include Data(binary)
    /// </summary>
    public class BinaryFrame : Frame
    {
        protected MemoryStream _Data;

        /// <summary>
        /// New BinaryFrame
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Flags">Frame Flag</param>
        /// <param name="Data">FileStream contain frame data</param>
        internal BinaryFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _Data = Data.ReadData(Length);
        }

        /// <summary>
        /// New BinaryFrame from specific information
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Flags">Frame Flags</param>
        /// <param name="Data">Data of BinaryFrame</param>
        public BinaryFrame(string FrameID, FrameFlags Flags, MemoryStream Data)
            : base(FrameID, Flags)
        {
            _Data = Data;
        }

        /// <summary>
        /// New BinaryFrame for inherited classes
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Flags">Frame Flags</param>
        protected BinaryFrame(string FrameID, FrameFlags Flags)
            : base(FrameID, Flags) { }

        /// <summary>
        /// Get or Set Data of current frame
        /// </summary>
        public MemoryStream Data
        {
            get
            {
                if (_Data == null)
                    return null;

                // Go to begining of stream
                _Data.Seek(0, SeekOrigin.Begin);

                return _Data;
            }
            set
            {
                if (value == null)
                    throw (new ArgumentNullException("Data can't set to null"));

                if (FrameID == "MCDI" && value.Length > 804)
                    throw (new ArgumentException("Music CD Identifier(MCDI) length must be equal or less than 804 byte"));

                _Data = value;
            }
        }

        #region -> Override method and properties <-

        /// <summary>
        /// Gets Length of current frame
        /// </summary>
        public override int Length
        {
            get
            { return Convert.ToInt32(_Data.Length); }
        }

        /// <summary>
        /// Gets MemoryStream to save current frame
        /// </summary>
        /// <param name="MinorVersion">Minor version of ID3v2</param>
        /// <returns>MemoryStream contain frame information</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            Data.WriteTo(ms);

            return ms;
        }

        /// <summary>
        /// Indicate if this is frame available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (_Data == null)
                    return false;

                if (_Data.Length == 0)
                    return false;

                return true;
            }
        }

        #endregion
    }

    /// <summary>
    /// A class for frame that include Data, Owner
    /// </summary>
    public class PrivateFrame : BinaryFrame
    {
        // Private Frames can repeat with same Owner Identifier in one tag \\

        protected string _Owner;

        /// <summary>
        /// New PrivateFrame
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Flags">Frame Flags</param>
        /// <param name="Data">FileStream to read frame data from</param>
        internal PrivateFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _Owner = Data.ReadText(Length, TextEncodings.Ascii, ref Length, true);

            _Data = Data.ReadData(Length); // Read Data
        }

        /// <summary>
        /// New Private Frame
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Flags">Frame Flags</param>
        /// <param name="Owner">Owner of data</param>
        /// <param name="Data">Data</param>
        public PrivateFrame(string FrameID, FrameFlags Flags, string Owner,
            MemoryStream Data)
            : base(FrameID, Flags)
        {
            if (FrameID != "UFID" && FrameID != "PRIV")
                throw (new ArgumentException("FrameID can only be UFID(Unique file Identifier) or PRIV(Private Frame)"));

            this.OwnerIdentifier = Owner;
            this.Data = Data;
        }

        /// <summary>
        /// Create new PrivateFrame for inherited classes
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flags">2 Bytes flags identifier</param>
        protected PrivateFrame(string FrameID, FrameFlags Flags)
            : base(FrameID, Flags) { }

        /// <summary>
        /// Get/Set OwnerIdentifier of current frame in ascii encoding
        /// </summary>
        public string OwnerIdentifier
        {
            get
            { return _Owner; }
            set
            {
                if (value == null)
                    throw (new ArgumentNullException("Owner can't set to null"));

                _Owner = value;
            }
        }

        /// <summary>
        /// Get or Set Data of current frame
        /// </summary>
        public new MemoryStream Data
        {
            get
            { return _Data; }
            set
            {
                if (value == null)
                {
                    _Data = null;
                    return;
                }

                if (this.FrameID == "UFID" && value.Length > 64)
                    throw (new ArgumentException("For Unique File Identifier(UFID) the Data length must be less than 64 bytes"));

                _Data = value;
            }
        }

        #region -> Override Method and properties <-

        /// <summary>
        /// Gets Length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                // 1: Seprator
                return base.Length + 1 + _Owner.Length;
            }
        }

        /// <summary>
        /// Gets MemoryStream for saving current frame
        /// </summary>
        /// <returns>MemoryStream contain frame information</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            WriteText(ms, _Owner, TextEncodings.Ascii, true);

            _Data.WriteTo(ms);

            return ms;
        }

        /// <summary>
        /// Indicate if this frame is available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (_Owner != "" || _Data.Length > 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        /// <summary>
        /// Convert current PrivateFrame to System.String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _Owner;
        }
    }

    /// <summary>
    /// A class for frame that include Data, Owner, Symbol
    /// </summary>
    public class DataWithSymbolFrame : PrivateFrame
    {
        protected byte _Symbol;

        /// <summary>
        /// New DataWithSymbolFrame
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Flags">Frame Flags</param>
        /// <param name="Data">FileStream to read frame data from</param>
        internal DataWithSymbolFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _Owner = Data.ReadText(Length, TextEncodings.Ascii, ref Length, true);

            _Symbol = Data.ReadByte();
            Length--;

            _Data = Data.ReadData(Length);
        }

        /// <summary>
        /// New DataWithSymbol from specific information
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Flags">Frame Flags</param>
        /// <param name="Owner">Owner identifier</param>
        /// <param name="Symbol">Symbol of owner</param>
        /// <param name="Data">Data of frame</param>
        public DataWithSymbolFrame(string FrameID, FrameFlags Flags, string Owner,
            byte Symbol, MemoryStream Data)
            : base(FrameID, Flags, Owner, Data)
        {
            if (FrameID != "ENCR" && FrameID != "GRID")
                throw (new ArgumentException("FrameID must be ENCR(Encryption method) or GRID(Group Identification)"));

            _Symbol = Symbol;
        }

        /// <summary>
        /// Gets or Sets Symbol of current frame
        /// </summary>
        public byte Symbol
        {
            get
            { return _Symbol; }
            set
            { _Symbol = value; }
        }

        #region -> Override Method and properties <-

        /// <summary>
        /// Gets Length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                //1: Symbol is only 1 byte
                return base.Length + 1;
            }
        }

        /// <summary>
        /// Get MemoryStream for saving current Frame
        /// </summary>
        /// <returns>MemoryStream contain frame information</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            WriteText(ms, _Owner, TextEncodings.Ascii, true);

            ms.WriteByte(_Symbol);

            Data.WriteTo(ms);

            return ms;
        }

        /// <summary>
        /// Indicate if this frame is available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                return base.IsAvailable;
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(DataWithSymbolFrame))
                return false;

            if (((DataWithSymbolFrame)obj).OwnerIdentifier == this.OwnerIdentifier &&
                ((DataWithSymbolFrame)obj).FrameID == this.FrameID)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// A class for frame that include Data, Owner, PreviewStart, PreviewLength
    /// </summary>
    public class AudioEncryptionFrame : PrivateFrame
    {
        protected int _PreviewStart;
        protected int _PreviewLength;

        /// <summary>
        /// Create new AudioEncryptionFrame
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flags">2 Bytes flags identifier</param>
        /// <param name="Data">Contain Data for this frame</param>
        internal AudioEncryptionFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _Owner = Data.ReadText(Length, TextEncodings.Ascii, ref Length, true);

            _PreviewStart = Convert.ToInt32(Data.ReadUInt(2));
            _PreviewLength = Convert.ToInt32(Data.ReadUInt(2));
            Length -= 4;

            _Data = Data.ReadData(Length);
        }

        /// <summary>
        /// Create new AudioEncryptionFrame
        /// </summary>
        /// <param name="Flags">Flags of frame</param>
        /// <param name="Owner">Owner identifier</param>
        /// <param name="PreviewStart">PreviewStart time</param>
        /// <param name="PreviewLength">PreviewLength time</param>
        /// <param name="Data">Data that this frame must contain</param>
        public AudioEncryptionFrame(FrameFlags Flags, string Owner,
            int PreviewStart, int PreviewLength, MemoryStream Data)
            : base("AENC", Flags)
        {
            _PreviewStart = PreviewStart;
            _PreviewLength = PreviewLength;
            this.Data = Data;
            this.OwnerIdentifier = Owner;
        }

        /// <summary>
        /// Gets or Sets PreviewStart of current frame
        /// </summary>
        public int PreviewStart
        {
            get
            {
                return _PreviewStart;
            }
            set
            {
                if (value > 0xFFFF || value < 0)
                    throw (new ArgumentOutOfRangeException("Preview Start must be less than 65,535(0xFFFF) and minimum be zero"));

                _PreviewStart = value;
            }
        }

        /// <summary>
        /// Gets or Sets PreviewLength of current frame
        /// </summary>
        public int PreviewLength
        {
            get
            {
                return _PreviewLength;
            }
            set
            {
                if (value > 0xFFFF || value < 0)
                    throw (new ArgumentOutOfRangeException("Preview Length must be less than 65,535(0xFFFF) and minimum be zero"));

                _PreviewLength = value;
            }
        }

        #region -> Override Method and properties <-

        /// <summary>
        /// Gets Length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                //4: PreviewStart and PreviewLength Length
                return base.Length + 4;
            }
        }

        /// <summary>
        /// Gets MemoryStream to save current frame
        /// </summary>
        /// <returns>MemoryStream contain frame information</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            WriteText(ms, _Owner, TextEncodings.Ascii, true);

            ushort temp;
            byte[] Buf;
            temp = Convert.ToUInt16(_PreviewStart);
            Buf = BitConverter.GetBytes(temp);
            ms.Write(Buf, 0, 2);

            temp = Convert.ToUInt16(_PreviewLength);
            Buf = BitConverter.GetBytes(temp);
            ms.Write(Buf, 0, 2);

            Data.WriteTo(ms);

            return ms;
        }

        /// <summary>
        /// Indicate if this frame is available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                return base.IsAvailable;
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(AudioEncryptionFrame))
                return false;

            if (((AudioEncryptionFrame)obj).OwnerIdentifier == this.OwnerIdentifier)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// A class for frame that include TextEncoding, Description, MIMEType, Data
    /// </summary>
    public abstract class BaseFileFrame : BinaryFrame
    {
        protected TextEncodings _TextEncoding;
        protected string _MIMEType;
        protected string _Description;

        /// <summary>
        /// New BaseFileFrame
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Flags">Frame Flags</param>
        protected BaseFileFrame(string FrameID, FrameFlags Flags)
            : base(FrameID, Flags) { }

        /// <summary>
        /// New BaseFileFrame
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Flags">Frame Flags</param>
        /// <param name="Description">Description</param>
        /// <param name="MIMEType">MimeType of Data</param>
        /// <param name="TextEncoding">TextEncoding for texts</param>
        /// <param name="Data">Data of frame</param>
        protected BaseFileFrame(string FrameID, FrameFlags Flags, string Description,
            string MIMEType, TextEncodings TextEncoding, MemoryStream Data)
            : base(FrameID, Flags)
        {
            _TextEncoding = TextEncoding;
            _MIMEType = MIMEType;
            _Description = Description;
            _Data = Data;
        }

        /// <summary>
        /// Gets or Sets current frame TextEncoding
        /// </summary>
        public TextEncodings TextEncoding
        {
            get
            { return _TextEncoding; }
            set
            { _TextEncoding = value; }
        }

        /// <summary>
        /// Gets or Sets current frame MIMEType
        /// </summary>
        public string MIMEType
        {
            get
            { return _MIMEType; }
            set
            { _MIMEType = value; }
        }

        /// <summary>
        /// Gets or sets Description of current frame
        /// </summary>
        public string Description
        {
            get
            { return _Description; }
            set
            {
                if (value == null)
                    throw (new ArgumentException("Description can't set to null"));
                _Description = value;
            }
        }
    }

    /// <summary>
    /// A class for frame that include TextEncoding, Description, FileName, MIMEType, Data
    /// </summary>
    public class GeneralFileFrame : BaseFileFrame
    {
        protected string _FileName;

        /// <summary>
        /// Create new GeneralFileFrame
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flags">2 Bytes flags identifier</param>
        /// <param name="Data">Contain Data for this frame</param>
        internal GeneralFileFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _TextEncoding = (TextEncodings)Data.ReadByte();
            Length--;
            if (!IsValidEnumValue(_TextEncoding, ValidatingErrorTypes.ID3Error))
            {
                _MustDrop = true;
                return;
            }

            _MIMEType = Data.ReadText(Length, TextEncodings.Ascii, ref Length, true);

            _FileName = Data.ReadText(Length, _TextEncoding, ref Length, true);

            _Description = Data.ReadText(Length, _TextEncoding, ref Length, true);

            _Data = Data.ReadData(Length);
        }

        /// <summary>
        /// Create new GeneralFile frame
        /// </summary>
        /// <param name="Flags">Flags of frame</param>
        /// <param name="Description">Description of frame</param>
        /// <param name="MIMEType">MimeType of file</param>
        /// <param name="TextEncoding">TextEncoding for storing texts</param>
        /// <param name="FileName">Filename</param>
        /// <param name="Data">Data contain file</param>
        public GeneralFileFrame(FrameFlags Flags, string Description,
            string MIMEType, TextEncodings TextEncoding, string FileName, MemoryStream Data)
            : base("GEOB", Flags, Description, MIMEType, TextEncoding, Data)
        {
            _FileName = FileName;
        }

        /// <summary>
        /// Get/Set FileName of current frame
        /// </summary>
        public string FileName
        {
            get
            { return _FileName; }
            set
            {
                if (value == null)
                    throw (new ArgumentNullException("FileName can't set to null"));

                _FileName = value;
            }
        }

        #region -> Override Method and properties <-

        /// <summary>
        /// Gets Length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                return base.Length + GetTextLength(_Description, _TextEncoding, true) +
                    GetTextLength(_MIMEType, TextEncodings.Ascii, true) +
                    GetTextLength(_FileName, _TextEncoding, true) + 1;
                //1: Text Encoding
            }
        }

        /// <summary>
        /// Gets MemoryStream to save current frame
        /// </summary>
        /// <returns>MemoryStream contain frame information</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            if (ID3v2.AutoTextEncoding)
                SetEncoding();

            ms.WriteByte((byte)_TextEncoding);

            WriteText(ms, _MIMEType, TextEncodings.Ascii, true);

            WriteText(ms, _FileName, _TextEncoding, true);

            WriteText(ms, _Description, _TextEncoding, true);

            _Data.WriteTo(ms);

            return ms;
        }

        /// <summary>
        /// Indicate if this frame is available
        /// </summary>
        public override bool IsAvailable
        {
            get
            { return base.IsAvailable; }
        }

        private void SetEncoding()
        {
            if (IsAscii(FileName) && IsAscii(_Description))
                TextEncoding = TextEncodings.Ascii;
            else
                TextEncoding = ID3v2.DefaultUnicodeEncoding;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(GeneralFileFrame))
                return false;

            if (this.FrameID != ((GeneralFileFrame)obj).FrameID)
                return false;

            if (((GeneralFileFrame)obj)._Description == this._Description)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Convert current GeneralFileFrame to String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Description + " [" + FileName + "]";
        }
    }

    /// <summary>
    /// A class for frame that include Text, Encoding, MIMEType, PictureType, Data
    /// </summary>
    public class AttachedPictureFrame : BaseFileFrame
    {
        protected PictureTypes _PictureType;

        public enum PictureTypes
        {
            Other = 0,
            FileIcon,
            OtherFileIcon,
            Cover_Front,
            Cover_Back,
            LeafletPage,
            Media,
            Soloist,
            Artist,
            Conductor,
            Band,
            Composer,
            Lyricist_TextWriter,
            RecordingLocation,
            DuringRecording,
            DuringPerformance,
            Movie,
            ABrightColouredFish,
            Illustration,
            BandLogo,
            PublisherLogo
        }

        /// <summary>
        /// Create new AttachedPictureFrame
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flags">2 Bytes flags identifier</param>
        /// <param name="Data">Contain Data for this frame</param>
        /// <param name="Length">MaxLength of frame</param>
        internal AttachedPictureFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _TextEncoding = (TextEncodings)Data.ReadByte();
            Length--;
            if (!IsValidEnumValue(_TextEncoding, ValidatingErrorTypes.ID3Error))
            {
                _MustDrop = true;
                return;
            }

            _MIMEType = Data.ReadText(Length, TextEncodings.Ascii, ref Length, true);

            _PictureType = (PictureTypes)Data.ReadByte();
            Length--;

            _Description = Data.ReadText(Length, _TextEncoding, ref Length, true);

            _Data = Data.ReadData(Length);
        }

        /// <summary>
        /// Create new AttachedPicture frame
        /// </summary>
        /// <param name="Flags">Flags of frame</param>
        /// <param name="Description">Description of picture</param>
        /// <param name="TextEncoding">TextEncoding use for texts</param>
        /// <param name="MIMEType">MimeType of picture</param>
        /// <param name="PictureType">Picture type</param>
        /// <param name="Data">Data Contain picture</param>
        public AttachedPictureFrame(FrameFlags Flags, string Description,
            TextEncodings TextEncoding, string MIMEType, PictureTypes PictureType,
            MemoryStream Data)
            : base("APIC", Flags, Description, MIMEType, TextEncoding, Data)
        {
            _PictureType = PictureType;
        }

        /// <summary>
        /// Get/Set PictureType of current frame
        /// </summary>
        public PictureTypes PictureType
        {
            get
            { return _PictureType; }
            set
            { _PictureType = value; }
        }

        /// <summary>
        /// Gets or Sets current frame Description
        /// </summary>
        public new string Description
        {
            get
            { return _Description; }
            set
            {
                if (value.Length > 64)
                    throw (new ArgumentException("Attached Picture Description length can't be more than 64 characters"));

                _Description = value;
            }
        }

        #region -> Override Method and properties <-

        /// <summary>
        /// Gets Length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                return base.Length +
                    GetTextLength(_Description, _TextEncoding, true) +
                    GetTextLength(_MIMEType, TextEncodings.Ascii, true) + 2;
                //1 for Text Encoding and 1 for PictureType
            }
        }

        /// <summary>
        /// Gets MemoryStream to save current frame
        /// </summary>
        /// <returns>MemoryStream contain frame information</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            if (ID3v2.AutoTextEncoding)
                SetEncoding();

            ms.WriteByte((byte)_TextEncoding);

            WriteText(ms, _MIMEType, _TextEncoding, true);

            ms.WriteByte((byte)_PictureType);

            WriteText(ms, _Description, _TextEncoding, true);

            _Data.WriteTo(ms);

            return ms;
        }

        /// <summary>
        /// Indicate if this frame is available
        /// </summary>
        public override bool IsAvailable
        {
            get
            { return base.IsAvailable; }
        }

        private void SetEncoding()
        {
            if (IsAscii(MIMEType) && IsAscii(Description))
                TextEncoding = TextEncodings.Ascii;
            else
                TextEncoding = ID3v2.DefaultUnicodeEncoding;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(AttachedPictureFrame))
                return false;

            // There can be only one Picture with type of
            // FileIcon and OtherFileIcon
            if (this._PictureType == PictureTypes.FileIcon ||
                this._PictureType == PictureTypes.OtherFileIcon)
                if (((AttachedPictureFrame)obj)._PictureType == this._PictureType)
                    return true;

            if (_Description == ((AttachedPictureFrame)obj)._Description)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Convert current Attached Picture to String in format of Description [PictureType]
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Description + " [" + PictureType.ToString() + "]";
        }
    }

    /// <summary>
    /// A class for frame that include TextEncoding, Price, ValidUntil, ContactUrl,
    /// RecievedAs, Seller, Description, MIMEType, Logo
    /// </summary>
    public class CommercialFrame : BaseFileFrame
    {
        protected Price _Price;
        protected SDate _ValidUntil;
        protected string _ContactUrl;
        protected RecievedAsEnum _RecievedAs;
        protected string _SellerName;

        public enum RecievedAsEnum
        {
            Other = 0,
            StandardCdAlbum,
            CompressedAudio,
            FileOverInternet,
            StreamOverInternet,
            AsNoteSheet,
            AsNoteSheetInBook,
            MusicOnOtherMedia,
            NonMusicalMerchandise,
            Unknown
        }

        /// <summary>
        /// New CommercialFrame
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Flags">Frame Flags</param>
        /// <param name="Data">Data of frame</param>
        /// <param name="Length">MaxLength of frame</param>
        internal CommercialFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _TextEncoding = (TextEncodings)Data.ReadByte();
            Length--;
            if (!IsValidEnumValue(_TextEncoding, ValidatingErrorTypes.ID3Error))
                return;

            _Price = new Price(Data, Length);
            Length -= _Price.Length;

            _ValidUntil = new SDate(Data);
            Length -= 8;


            _ContactUrl = Data.ReadText(Length, TextEncodings.Ascii, ref Length, true);

            _RecievedAs = (RecievedAsEnum)Data.ReadByte();
            Length--;

            _SellerName = Data.ReadText(Length, _TextEncoding, ref Length, true);

            _Description = Data.ReadText(Length, _TextEncoding, ref Length, true);

            if (Length < 1) // Data finished
                return;

            _MIMEType = Data.ReadText(Length, TextEncodings.Ascii, ref Length, true);

            _Data = Data.ReadData(Length);
        }

        /// <summary>
        /// Create new Commercial frame
        /// </summary>
        /// <param name="Flags">Flags of frame</param>
        /// <param name="Description">Description for current frame</param>
        /// <param name="TextEncoding">TextEncoding use for texts</param>
        /// <param name="Price">Price that payed for song</param>
        /// <param name="ValidUntil">Validation date</param>
        /// <param name="ContactURL">Contact URL to seller</param>
        /// <param name="RecievedAs">RecievedAd type</param>
        /// <param name="SellerName">SellerName</param>
        /// <param name="MIMEType">MimeType for seller Logo</param>
        /// <param name="Logo">Data Contain Seller Logo</param>
        public CommercialFrame(FrameFlags Flags, string Description,
            TextEncodings TextEncoding, Price Price , SDate ValidUntil, string ContactURL,
            RecievedAsEnum RecievedAs, string SellerName, string MIMEType, MemoryStream Logo)
            : base("COMR", Flags, Description, MIMEType, TextEncoding, Logo)
        {
            _ValidUntil = ValidUntil;
            this.ContactUrl = ContactURL;
            this.SellerName = SellerName;
            this.RecievedAs = RecievedAs;
            _Price = Price;
        }

        /// <summary>
        /// Gets or sets Price payed
        /// </summary>
        public Price Price
        {
            get
            { return _Price; }
        }

        /// <summary>
        /// Gets or sets Validation date
        /// </summary>
        public SDate ValidUntil
        {
            get
            { return _ValidUntil; }
            set
            { _ValidUntil = value; }
        }

        /// <summary>
        /// Gets or sets Contact URL of seller
        /// </summary>
        public string ContactUrl
        {
            get
            { return _ContactUrl; }
            set
            {
                if (value == null)
                    throw (new ArgumentNullException("Can't set Contact url to null"));

                _ContactUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets Recieved As type
        /// </summary>
        public RecievedAsEnum RecievedAs
        {
            get
            { return _RecievedAs; }
            set
            {
                if (!Enum.IsDefined(typeof(RecievedAsEnum), value))
                    throw (new ArgumentException("This is not valid for RecievedAsEnum"));

                _RecievedAs = value;
            }
        }

        /// <summary>
        /// Gets or sets seller name
        /// </summary>
        public string SellerName
        {
            get
            { return _SellerName; }
            set
            {
                if (value == null)
                    throw (new ArgumentNullException("Seller name can't set to null"));

                _SellerName = value;
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
                // 1byte: PriceString seprator
                // 1byte: TextEncoding                
                // 8Byte: ValidUntil date
                // 1Byte: Recieved as
                //--------------------------
                // Sum: 11 Byte
                int RInt;
                RInt = _Price.Length +
                    GetTextLength(_ContactUrl, TextEncodings.Ascii, true) +
                    GetTextLength(_SellerName, _TextEncoding, true) +
                    GetTextLength(_Description, _TextEncoding, true) + 11;

                if (_MIMEType != "" && _MIMEType != null)
                {
                    RInt += GetTextLength(_MIMEType, TextEncodings.Ascii, true);
                    RInt += Convert.ToInt32(_Data.Length);
                }

                return RInt;
            }
        }

        /// <summary>
        /// Gets MemoryStream to save current frame
        /// </summary>
        /// <returns>MemoryStream contain frame information</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            if (ID3v2.AutoTextEncoding)
                SetEncoding();

            ms.WriteByte((byte)_TextEncoding);

            WriteText(ms, _Price.ToString(), TextEncodings.Ascii, true);

            WriteText(ms, _ValidUntil.String, TextEncodings.Ascii, true);

            ms.WriteByte((byte)_RecievedAs);

            WriteText(ms, _SellerName, _TextEncoding, true);

            WriteText(ms, _Description, _TextEncoding, true);

            if (!LogoExists)
                return ms;

            WriteText(ms, _MIMEType, TextEncodings.Ascii, true);

            _Data.WriteTo(ms);

            return ms;
        }

        /// <summary>
        /// Indicate is current frame Available or not
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (_Price.Value == "")
                    return false;

                return true;
            }
        }

        private void SetEncoding()
        {
            if (IsAscii(SellerName) && IsAscii(Description))
                TextEncoding = TextEncodings.Ascii;
            else
                TextEncoding = ID3v2.DefaultUnicodeEncoding;
        }

        #endregion

        /// <summary>
        /// Indicate if logo exists for this frame
        /// </summary>
        private bool LogoExists
        {
            get
            {
                if (_Data == null || _Data.Length < 1)
                    return false;

                return true;
            }
        }
    }
}