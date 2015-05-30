using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using ID3.ID3v2Frames.OtherFrames;

/*
 * This namespace contain frames that their base information is text(string)
 */
namespace ID3.ID3v2Frames.TextFrames
{
    /// <summary>
    /// A class for frame that only include Text member
    /// </summary> 
    public abstract class TextOnlyFrame : Frame
    {
        private string _Text; // Contain text of current frame

        protected TextOnlyFrame(string FrameID, FrameFlags Flags)
            : base(FrameID, Flags) { }

        /// <summary>
        /// Get or Set current TextOnlyFrame text
        /// </summary>
        public string Text
        {
            get
            { return _Text; }
            set
            { _Text = value; }
        }

        #region -> Override Methods and properties <-

        /// <summary>
        /// Indicate if this frame have usefull data (Text!=null,Empty)
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (Text == null || Text == "")
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Get stream to save current Frame
        /// </summary>
        /// <returns>Bytes for saving this frame</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            WriteText(ms, _Text, TextEncodings.Ascii, false);

            return ms;
        }

        /// <summary>
        /// Get length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                // in Ascii Encoding each character is one byte
                return Text.Length;
            }
        }

        #endregion
    }

    /// <summary>
    /// A Class for frames that include Text with TextEncoding
    /// </summary>
    public class TextFrame : TextOnlyFrame
    {
        /*
         * Note: This class support both URL and Text frames
         * the diffrence between these two types is: URL frame don't contain
         * TextEncoding and always use Ascii as Encoding but TextFrames contain
         * URLs start with 'W' texts with 'T'
         */
        private TextEncodings _TextEncoding;

        /// <summary>
        /// Create new TextFrame Class
        /// </summary>
        /// <param name="FrameID">4 Characters frame identifier</param>
        /// <param name="Flags">Flag of frame</param>
        /// <param name="Data">FileStream to read frame data from</param>
        /// <param name="Length">Maximum length of frame</param>
        internal TextFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            // If it was URL frame the TextEncoding is ascii and must not read
            if (IsUrl)
                TextEncoding = TextEncodings.Ascii;
            else
            {
                TextEncoding = (TextEncodings)Data.ReadByte();
                Length--;
                if (!IsValidEnumValue(TextEncoding, ValidatingErrorTypes.ID3Error))
                    return;
            }

            Text = Data.ReadText(Length, _TextEncoding);
        }

        /// <summary>
        /// Create new TextFrame with specific information
        /// </summary>
        /// <param name="Text">Text of TextFrame</param>
        /// <param name="TextEncoding">TextEncoding of TextFrame</param>
        /// <param name="FrameID">FrameID of TextFrame</param>
        /// <param name="Flags">Flags of Frame</param>
        public TextFrame(string FrameID, FrameFlags Flags, string Text, TextEncodings TextEncoding,
            int Ver)
            : base(FrameID, Flags)
        {
            if (FramesInfo.IsTextFrame(FrameID, Ver) != 1)
                throw (new ArgumentException(FrameID + " is not valid TextFrame FrameID"));

            this.Text = Text;
            this.TextEncoding = TextEncoding;
        }

        protected TextFrame(string FrameID, FrameFlags Flags)
            : base(FrameID, Flags) { }

        /// <summary>
        /// Get or Set current frame TextEncoding
        /// </summary>
        public TextEncodings TextEncoding
        {
            get
            { return _TextEncoding; }
            set
            {
                if (IsValidEnumValue(value, ValidatingErrorTypes.Exception))
                    _TextEncoding = value;
            }
        }

        #region -> Override method and properties <-

        /// <summary>
        /// Get MemoryStream of frame data
        /// </summary>
        /// <returns>MemoryStream of current frame</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            if (!IsUrl)
            {
                if (ID3v2.AutoTextEncoding)
                    SetEncoding();

                ms.WriteByte((byte)_TextEncoding); // Write Text Encoding
                WriteText(ms, Text, _TextEncoding, false); // Write Text
            }
            else
                WriteText(ms, Text, TextEncodings.Ascii, false);

            return ms;
        }

        /// <summary>
        /// Get length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                // 1: Encoding
                // TextLength ( Ascii Or Unicode )
                // this frame don't contain text seprator
                return (1 + GetTextLength(Text, _TextEncoding, false));
            }
        }

        /// <summary>
        /// Indicate if this frame have usefull data (Text:null;Empty) (TextEncoding:Valid)
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                // if TextEncoding and Text value is valid this frame is valid
                // otherwise not
                if (!IsValidEnumValue(_TextEncoding, ValidatingErrorTypes.Nothing) ||
                    Text == null || Text == "")
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Set TextEncoding according to Data of current frame
        /// </summary>
        private void SetEncoding()
        {
            if (IsAscii(Text))
                TextEncoding = TextEncodings.Ascii;
            else
                TextEncoding = ID3v2.DefaultUnicodeEncoding;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            // if FrameID of two text frames were equal they are equal
            // ( the text is not important )
            return (this.FrameID == ((TextFrame)obj).FrameID);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Indicate if current frame contain URL information
        /// </summary>
        protected bool IsUrl
        {
            get
            {
                // first character of URL frames always is 'W'
                return (FrameID[0] == 'W');
            }
        }
    }

    /// <summary>
    /// A Class for frames that include Rating, Counter, Email
    /// </summary>
    public class PopularimeterFrame : TextOnlyFrame
    {
        protected long _Counter;
        protected byte _Rating;

        /// <summary>
        /// New PopularimeterFrame
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flags">Frame Flags</param>
        /// <param name="Data">FileStream contain frame data</param>
        internal PopularimeterFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            EMail = Data.ReadText(Length, TextEncodings.Ascii, ref Length, true); // Read Email Address

            _Rating = Data.ReadByte(); // Read Rating
            Length--;

            if (Length > 8)
            {
                ErrorOccur("Counter value for Popularimeter frame is more than 8 byte." +
                    " this is not supported by this program", true);
                return;
            }

            byte[] LBuf = new byte[8];
            byte[] Buf = new byte[Length];

            Data.Read(Buf, 0, Length);
            Buf.CopyTo(LBuf, 8 - Buf.Length);
            Array.Reverse(LBuf);

            _Counter = BitConverter.ToInt64(LBuf, 0);
        }

        /// <summary>
        /// New PopulariMeter frame from specific information
        /// </summary>
        /// <param name="Flags">Frame Flags</param>
        /// <param name="EMail">Email of user</param>
        /// <param name="Rating">User Rated value</param>
        /// <param name="Counter">How many times user listened to audio</param>
        public PopularimeterFrame(FrameFlags Flags, string EMail,
            byte Rating, long Counter)
            : base("POPM", Flags)
        {
            base.Text = EMail;
            _Rating = Rating;
            _Counter = Counter;
        }

        /// <summary>
        /// Get or Set Rating value for current Email Address
        /// </summary>
        public byte Rating
        {
            get
            { return _Rating; }
            set
            { _Rating = value; }
        }

        /// <summary>
        /// Get or Set Counter for current User (Mail Address)
        /// </summary>
        public long Counter
        {
            get
            { return _Counter; }
            set
            { _Counter = value; }
        }

        /// <summary>
        /// Gets or sets Email for current User
        /// </summary>
        public string EMail
        {
            get
            { return base.Text; }
            set
            { base.Text = value; }
        }

        #region -> Override method and properties <-

        /// <summary>
        /// Get length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                return 10 + EMail.Length;
                // 1:   Rating Length
                // 1:   Seprator
                // 8:   Counter
            }
        }

        /// <summary>
        /// Get MemoryStream ot save current frame
        /// </summary>
        /// <returns>MemoryStream that represent current frame data</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);
            WriteText(ms, EMail, TextEncodings.Ascii, true);

            ms.WriteByte(_Rating);

            byte[] Buf = BitConverter.GetBytes(_Counter);
            Array.Reverse(Buf);
            ms.Write(Buf, 0, 8);

            return ms;
        }

        /// <summary>
        /// Indicate if current frame is available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (EMail != "")
                    return true;

                return false;
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            if (((PopularimeterFrame)obj).EMail == this.EMail)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// This property is not usable for this class
        /// </summary>
        public new string Text
        {
            get
            { throw (new Exception("This property is not useable for this class")); }
        }
    }

    /// <summary>
    /// A Class for frames that include Text, Encoding and Description
    /// </summary>
    public class UserTextFrame : TextFrame
    {
        protected string _Description;

        /// <summary>
        /// Create new UserTextFrameClass
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flags">Frame Flagsr</param>
        /// <param name="Data">MemoryStream to read information from</param>
        internal UserTextFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            TextEncoding = (TextEncodings)Data.ReadByte();
            Length--;
            if (!IsValidEnumValue(TextEncoding, ValidatingErrorTypes.ID3Error))
            {
                _MustDrop = true;
                return;
            }

            _Description = Data.ReadText(Length, TextEncoding, ref Length, true);

            if (!IsUrl) // is text frame
                Text = Data.ReadText(Length, TextEncoding);
            else
                Text = Data.ReadText(Length, TextEncodings.Ascii);

            // User URL frames use this class and use Text property as URL
            // URL property must be in AScii format
            // all URL frames start with W and text frames with T
        }

        /// <summary>
        /// Create new UserTextFrame from specific information
        /// </summary>
        /// <param name="FrameID">FrameID of frame</param>
        /// <param name="Flags">Frame flags</param>
        /// <param name="Text">Frame text</param>
        /// <param name="Description">Frame description</param>
        /// <param name="TextEncoding">TextEncoding of texts</param>
        /// <param name="Ver">Minor version of ID3v2</param>
        public UserTextFrame(string FrameID, FrameFlags Flags, string Text,
            string Description, TextEncodings TextEncoding, int Ver)
            : base(FrameID, Flags)
        {
            if (FramesInfo.IsTextFrame(FrameID, Ver) != 2)
                throw (new ArgumentException(FrameID + " is not valid for UserTextFrame class"));

            this.Text = Text;
            this.TextEncoding = TextEncoding;
            this.Description = Description;
        }

        protected UserTextFrame(string FrameID, FrameFlags Flags)
            : base(FrameID, Flags) { }

        /// <summary>
        /// Get/Set current frame Description
        /// </summary>
        public string Description
        {
            set
            {
                if (value == null)
                    throw (new ArgumentException("Description can't be null"));

                _Description = value;
            }
            get
            { return _Description; }
        }

        #region -> Override Method and properties <-

        /// <summary>
        /// Get length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                // TextLength
                // Description Length ( + seprator )
                // 1: Encoding
                int TextLen;

                if (!IsUrl)
                    TextLen = GetTextLength(Text, TextEncoding, false);
                else
                    TextLen = GetTextLength(Text, TextEncodings.Ascii, false); ;

                return 1 + TextLen + GetTextLength(_Description, TextEncoding, true);
            }
        }

        /// <summary>
        /// Get MemoryStream to save this frame
        /// </summary>
        /// <returns>MemoryStream that represent current frame data</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            if (ID3v2.AutoTextEncoding)
                SetEncoding();

            ms.WriteByte((byte)TextEncoding); // Write Encoding

            WriteText(ms, _Description, TextEncoding, true);

            if (!IsUrl)
                WriteText(ms, Text, TextEncoding, false);
            else // URL frames always use ascii encoding for text value
                WriteText(ms, Text, TextEncodings.Ascii, false);

            return ms;
        }

        /// <summary>
        /// Indicate if this frame contain data
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if ((_Description != "" || Text != "") && IsValidEnumValue(TextEncoding, ValidatingErrorTypes.Nothing))
                    return true;

                return false;
            }
        }

        private void SetEncoding()
        {
            if (IsAscii(Text) && IsAscii(Description))
                TextEncoding = TextEncodings.Ascii;
            else
                TextEncoding = ID3v2.DefaultUnicodeEncoding;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            if (this.FrameID == ((UserTextFrame)obj).FrameID
                && this._Description == ((UserTextFrame)obj)._Description)
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
    /// A Class for frames that include Text, Encoding and Language
    /// </summary>
    public class TermOfUseFrame : TextFrame
    {
        protected Language _Language;

        /// <summary>
        /// Create new TermOfUseFrame class
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flags">2 Bytes flags identifier</param>
        /// <param name="Data">Contain Data for this frame</param>
        internal TermOfUseFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            TextEncoding = (TextEncodings)Data.ReadByte();
            Length--;
            if (!IsValidEnumValue(TextEncoding, ValidatingErrorTypes.ID3Error))
            {
                _MustDrop = true;
                return;
            }

            _Language = new Language(Data);
            Length -= 3;

            Text = Data.ReadText(Length, TextEncoding);
        }

        public TermOfUseFrame(FrameFlags Flags, string Text,
            TextEncodings TextEncoding, string Lang)
            : base("USER", Flags)
        {
            this.Text = Text;
            this.TextEncoding = TextEncoding;
            Language = new Language(Lang);
        }

        protected TermOfUseFrame(string FrameID, FrameFlags Flags)
            : base(FrameID, Flags) { }

        /// <summary>
        /// Gets or sets language of current frame
        /// </summary>
        public Language Language
        {
            get
            { return _Language; }
            set
            { _Language = value; }
        }

        #region -> Override method's and properties <-

        /// <summary>
        /// Get Length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                // 3: Language Length
                return (base.Length + 3);
            }
        }

        /// <summary>
        /// Get bytes for saving this frame
        /// </summary>
        /// <returns>Bytes for saving this frame</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            if (ID3v2.AutoTextEncoding)
                SetEncoding();

            ms.WriteByte((byte)TextEncoding); // Write Text Encoding

            _Language.Write(ms);

            WriteText(ms, Text, TextEncoding, false);

            return ms;
        }

        /// <summary>
        /// Indicate if current frame is available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (base.IsAvailable == false)
                    return false;

                return _Language.IsValidLanguage;
            }
        }

        private void SetEncoding()
        {
            if (IsAscii(Text))
                TextEncoding = TextEncodings.Ascii;
            else
                TextEncoding = ID3v2.DefaultUnicodeEncoding;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            if (((TermOfUseFrame)obj)._Language == this._Language &&
                ((TermOfUseFrame)obj).FrameID == this.FrameID)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "Term of use [" + _Language + "]";
        }
    }

    /// <summary>
    /// A Class for frames that include PricePayed, DateOfPurch, TextEncoding, Text(Seller)
    /// </summary>
    public class OwnershipFrame : TextFrame
    {
        // Inherits:
        //      Text
        //      Encoding
        private Price _Price;
        private SDate _DateOfPurch;

        /// <summary>
        /// Create new OwnershipFrame
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flags">2 Bytes flags identifier</param>
        /// <param name="Data">Contain Data for this frame</param>
        internal OwnershipFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            TextEncoding = (TextEncodings)Data.ReadByte();
            Length--;
            if (!IsValidEnumValue(TextEncoding, ValidatingErrorTypes.ID3Error))
                return;

            _Price = new Price(Data, Length);
            Length -= _Price.Length;
            if (!_Price.IsValid)
            {
                ErrorOccur("Price is not valid value. ownership frame will not read", true);
                return;
            }

            if (Length >= 8)
            {
                _DateOfPurch = new SDate(Data);
                Length -= 8;
            }
            else
            {
                ErrorOccur("Date is not valid for this frame", true);
                return;
            }

            Seller = Data.ReadText(Length, TextEncoding);
        }

        public OwnershipFrame(FrameFlags Flags, Price PricePayed, SDate PurchDate,
            string Seller, TextEncodings TEncoding)
            : base("OWNE", Flags)
        {
            _Price = PricePayed;
            _DateOfPurch = PurchDate;
            this.Seller = Seller;
        }

        /// <summary>
        /// Get/Set DateOfPurch for current frame
        /// </summary>
        public SDate DateOfPurch
        {
            get
            { return _DateOfPurch; }
            set
            { _DateOfPurch = value; }
        }

        /// <summary>
        /// Get price of current frame
        /// </summary>
        public Price Price
        {
            get
            {
                return _Price;
            }
        }

        #region -> Override method and properties <-

        /// <summary>
        /// Get Length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                // base.Length: 10(Header) + 1(Encoding) + Text.Length(According to encoding)
                // Price.Length + 8(Date) + 1(Seprator of Price)
                return (base.Length + _Price.Length) + 9;
            }
        }

        /// <summary>
        /// Get MemoryStream to save current frame
        /// </summary>
        /// <returns>MemoryStream that represent current frame data</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            if (ID3v2.AutoTextEncoding)
                SetEncoding();

            ms.WriteByte((byte)TextEncoding); // Write Text Encoding

            WriteText(ms, _Price.ToString(), TextEncodings.Ascii, true);

            WriteText(ms, _DateOfPurch.String, TextEncodings.Ascii, false);

            WriteText(ms, Seller, TextEncoding, false);

            return ms;
        }

        /// <summary>
        /// Indicate if current frame is available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (_DateOfPurch == null && _Price == null)
                    return base.IsAvailable;

                return false;
            }
        }

        private void SetEncoding()
        {
            if (IsAscii(Text))
                TextEncoding = TextEncodings.Ascii;
            else
                TextEncoding = ID3v2.DefaultUnicodeEncoding;
        }

        #endregion

        /// <summary>
        /// This property is not available for Ownership
        /// </summary>
        public new string Text
        {
            get
            {
                throw (new InvalidOperationException("This property not available for Ownership"));
            }
        }

        /// <summary>
        /// Get/Set Current frame seller
        /// </summary>
        public string Seller
        {
            get
            { return base.Text; }
            set
            {
                base.Text = value;
                // Base.Text control the value for null
            }
        }
    }

    /// <summary>
    /// A Class for frames that include FrameIdentifier, URL, AdditionalData
    /// </summary>
    public class LinkFrame : TextFrame
    {
        protected string _FrameIdentifier;
        protected string _AdditionalData;

        /// <summary>
        /// Create new LinkFrame
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flags">2 Bytes flags identifier</param>
        /// <param name="Data">Contain Data for this frame</param>
        /// <param name="Length"></param>
        internal LinkFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            _FrameIdentifier = Data.ReadText(4, TextEncodings.Ascii);
            if (!ValidatingFrameID(_FrameIdentifier, ValidatingErrorTypes.ID3Error))
                return;
            Length -= 4;
            // There is 3 byte in article that i think it's not true
            // because frame identifier is 4 character

            // use Text variable as URL
            URL = Data.ReadText(Length, TextEncodings.Ascii, ref Length, true);

            _AdditionalData = Data.ReadText(Length, TextEncodings.Ascii);
        }

        /// <summary>
        /// New LinkedFrame from specific information
        /// </summary>
        /// <param name="Flags">Frame Flags</param>
        /// <param name="FrameIdentifier">FrameIdentifier of frame that linked</param>
        /// <param name="URL">URL address of Linked Frame</param>
        /// <param name="AdditionalData">Additional data of Linked Frame</param>
        public LinkFrame(FrameFlags Flags, string FrameIdentifier,
            string URL, string AdditionalData)
            : base("LINK", Flags)
        {
            this.URL = URL;
            _AdditionalData = AdditionalData;

            // Check if FrameIdentifier is valid
            ValidatingFrameID(FrameIdentifier, ValidatingErrorTypes.Exception);

            _FrameIdentifier = FrameIdentifier;
        }

        /// <summary>
        /// URL of current Link Frame
        /// </summary>
        public string URL
        {
            get
            { return base.Text; }
            set
            {
                // Check for null value (base.Text check it)
                base.Text = value;
            }
        }

        /// <summary>
        /// Get/Set Additional Data of Current Frame
        /// </summary>
        public string AdditionalData
        {
            get
            { return _AdditionalData; }
            set
            { _AdditionalData = value; }
        }

        /// <summary>
        /// Frame Identifier of Linked Frame
        /// </summary>
        public string FrameIdentifier
        {
            get
            { return _FrameIdentifier; }
            set
            {
                if (ValidatingFrameID(value, ValidatingErrorTypes.Exception))
                    _FrameIdentifier = value;
            }
        }

        #region -> Override Method and properties <-

        /// <summary>
        /// Get Length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                // 4: FrameIdentifier
                // 1: Seprator
                return 5 + URL.Length + _AdditionalData.Length;
            }
        }

        /// <summary>
        /// Get MemoryStream to save current frame
        /// </summary>
        /// <returns>MemoryStream represent current frame data</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            WriteText(ms, _FrameIdentifier, TextEncodings.Ascii, false);

            WriteText(ms, URL, TextEncodings.Ascii, true); // Write URL

            WriteText(ms, _AdditionalData, TextEncodings.Ascii, false);

            return ms;
        }

        /// <summary>
        /// Indicate if current frame is available
        /// </summary>
        public override bool IsAvailable
        {
            get
            {
                if (_FrameIdentifier == "")
                    return false;

                return true;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            if (((LinkFrame)obj)._FrameIdentifier == this._FrameIdentifier)
                return true;
            else
                return false;
        }

        public override int GetHashCode() { return base.GetHashCode(); }

        #endregion

        /// <summary>
        /// This property is not Available for current class
        /// </summary>
        public new TextEncodings TextEncoding
        {
            get
            { return TextEncodings.Ascii; }
        }

        /// <summary>
        /// This property is not Available for current class
        /// </summary>
        public new string Text
        {
            get
            { throw (new Exception("This property is not available for current class")); }
        }
    }

    /// <summary>
    /// A Class for frames that include Text, Description, Encoding and Language
    /// </summary>
    public class TextWithLanguageFrame : UserTextFrame
    {
        protected Language _Language;

        /// <summary>
        /// Create new TextWithLanguageFrame
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flags">2 Bytes flags identifier</param>
        /// <param name="Data">Contain Data for this frame</param>
        internal TextWithLanguageFrame(string FrameID, FrameFlags Flags, FileStreamEx Data, int Length)
            : base(FrameID, Flags)
        {
            TextEncoding = (TextEncodings)Data.ReadByte();
            Length--;
            if (!IsValidEnumValue(TextEncoding, ValidatingErrorTypes.ID3Error))
            {
                _MustDrop = true;
                return;
            }

            _Language = new Language(Data);
            Length -= 3;

            _Description = Data.ReadText(Length, TextEncoding, ref Length, true);

            Text = Data.ReadText(Length, TextEncoding);
        }

        public TextWithLanguageFrame(string FrameID, FrameFlags Flags, string Text,
            string Description, TextEncodings TextEncoding, string Lang)
            : base(FrameID, Flags)
        {
            if (FrameID != "USLT" && FrameID != "COMM")
                throw (new ArgumentException(FrameID + " is not valid Frame for TextWithLanguageFrame"));

            Language = new Language(Lang);
            this.Text = Text;
            this.Description = Description;
            this.TextEncoding = TextEncoding;
        }

        /// <summary>
        /// Get/Set current frame Language 
        /// </summary>
        public Language Language
        {
            get
            { return _Language; }
            set
            { _Language = value; }
        }

        #region -> Override method and properties <-

        /// <summary>
        /// Get Length of current frame
        /// </summary>
        public override int Length
        {
            get
            {
                // 3: Language Length
                return (base.Length + 3);
            }
        }

        /// <summary>
        /// Get MemoryStream for saving current frame
        /// </summary>
        /// <returns>MemoryStream that represent current frame information</returns>
        public override MemoryStream FrameStream(int MinorVersion)
        {
            MemoryStream ms = FrameHeader(MinorVersion);

            if (ID3v2.AutoTextEncoding)
                SetEncoding();

            ms.WriteByte((byte)TextEncoding); // Write Text Encoding

            _Language.Write(ms);

            WriteText(ms, _Description, TextEncoding, true);

            WriteText(ms, Text, TextEncoding, false);

            return ms;
        }

        public override bool IsAvailable
        {
            get
            {
                if (IsValidEnumValue(TextEncoding, ValidatingErrorTypes.Nothing) &&
                    (Text != "" || _Description != ""))
                    return true;
                else
                    return false;
            }
        }

        private void SetEncoding()
        {
            if (IsAscii(Text) && IsAscii(Description))
                TextEncoding = TextEncodings.Ascii;
            else
                TextEncoding = ID3v2.DefaultUnicodeEncoding;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            if (((TextWithLanguageFrame)obj).FrameID == this.FrameID &&
                ((TextWithLanguageFrame)obj)._Language == this._Language &&
                ((TextWithLanguageFrame)obj)._Description == this._Description)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// convert current Frame to System.String
        /// </summary>
        /// <returns>Description [Language]</returns>
        public override string ToString()
        {
            return _Description + " [" + _Language + "]";
        }
    }
}