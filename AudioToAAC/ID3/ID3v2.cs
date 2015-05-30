using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using ID3.ID3v2Frames;
using ID3.ID3v2Frames.TextFrames;
using ID3.ID3v2Frames.BinaryFrames;
using ID3.ID3v2Frames.OtherFrames;
using ID3.ID3v2Frames.ArrayFrames;
using ID3.ID3v2Frames.StreamFrames;

namespace ID3
{
    public class ID3v2
    {
        private string _FilePath; // ID3 file path
        private FilterCollection _Filter; // Contain Filter Frames
        private FilterTypes _FilterType; //Indicate wich filter type use
        private ID3v2Flags _Flags;
        private bool _LoadLinkedFrames; // Indicate load Link frames when loading ID3 or not
        private bool _DropUnknown; // if true. unknown frames will not save
        private Version _ver; // Contain ID3 version information
        private bool _HaveTag; // Indicate if current file have ID3v2 Info
        private ErrorCollection _Errors; // Contain Errors that occured
        private static TextEncodings _DefaultUnicodeEncoding = TextEncodings.UTF_16; // when use AutoTextEncoding which unicode type must use
        private static bool _AutoTextEncoding = true; // when want to save frame use automatic text encoding

        #region -> Frame Variable <-

        // Frames that can be more than one
        private FramesCollection<TextFrame> _TextFrames;
        private FramesCollection<UserTextFrame> _UserTextFrames;
        private FramesCollection<PrivateFrame> _PrivateFrames;
        private FramesCollection<TextWithLanguageFrame> _TextWithLangFrames;
        private FramesCollection<SynchronisedText> _SynchronisedTextFrames;
        private FramesCollection<AttachedPictureFrame> _AttachedPictureFrames;
        private FramesCollection<GeneralFileFrame> _EncapsulatedObjectFrames;
        private FramesCollection<PopularimeterFrame> _PopularimeterFrames;
        private FramesCollection<AudioEncryptionFrame> _AudioEncryptionFrames;
        private FramesCollection<LinkFrame> _LinkFrames;
        private FramesCollection<TermOfUseFrame> _TermOfUseFrames;
        private FramesCollection<DataWithSymbolFrame> _DataWithSymbolFrames;
        private FramesCollection<BinaryFrame> _UnknownFrames;

        // Frames that can't repeat
        private BinaryFrame _MCDIFrame;
        private SynchronisedTempoFrame _SYTCFrame; // Synchronised tempo codes        
        private PlayCounterFrame _PCNTFrame; // Play Counter        
        private RecomendedBufferSizeFrame _RBUFFrame;
        private OwnershipFrame _OWNEFrame; // Owner ship
        private CommercialFrame _COMRFrame;
        private ReverbFrame _RVRBFrame;
        private Equalisation _EQUAFrame;
        private RelativeVolumeFrame _RVADFrame;
        private EventTimingCodeFrame _ETCOFrame;
        private PositionSynchronisedFrame _POSSFrame;

        #endregion

        /// <summary>
        /// Create new ID3v2 class for specific file
        /// </summary>
        /// <param name="FileAddress">FileAddress to read ID3 information from</param>
        /// <param name="LoadData">Indicate load ID3 in constructor or not</param>
        public ID3v2(string FilePath, bool LoadData)
        {
            // ------ Set default values -----------
            _LoadLinkedFrames = true;
            _DropUnknown = false;

            _FilePath = FilePath;

            Initializer();

            if (LoadData == true)
                Load();
        }

        private void Initializer()
        {
            _Filter = new FilterCollection();

            _FilterType = FilterTypes.NoFilter;
            _Errors = new ErrorCollection();

            _TextFrames = new FramesCollection<TextFrame>();
            _UserTextFrames = new FramesCollection<UserTextFrame>();
            _PrivateFrames = new FramesCollection<PrivateFrame>();
            _TermOfUseFrames = new FramesCollection<TermOfUseFrame>();
            _TextWithLangFrames = new FramesCollection<TextWithLanguageFrame>();
            _SynchronisedTextFrames = new FramesCollection<SynchronisedText>();
            _AttachedPictureFrames = new FramesCollection<AttachedPictureFrame>();
            _EncapsulatedObjectFrames = new FramesCollection<GeneralFileFrame>();
            _PopularimeterFrames = new FramesCollection<PopularimeterFrame>();
            _AudioEncryptionFrames = new FramesCollection<AudioEncryptionFrame>();
            _LinkFrames = new FramesCollection<LinkFrame>();
            _DataWithSymbolFrames = new FramesCollection<DataWithSymbolFrame>();
            _UnknownFrames = new FramesCollection<BinaryFrame>();
        }

        private void WriteID3Header(FileStream Data, int Ver)
        {
            byte[] Buf;
            Buf = Encoding.ASCII.GetBytes("ID3");
            Data.Write(Buf, 0, 3); // Write ID3

            // ----------- Write Version ---------
            Data.WriteByte(Convert.ToByte(Ver));
            Data.WriteByte(0);
            Data.WriteByte((byte)_Flags);

            // -------- Write Size --------------------
            // -- Calculating and writing length of ID3
            // for more information look at references
            Buf = new byte[4];
            int Len = Length;
            for (int i = 3; i >= 0; i--)
            {
                Buf[i] = Convert.ToByte(Len % 0x80);
                Len /= 0x80;
            }
            Data.Write(Buf, 0, 4);

            _ver = new Version("2.3.0");
        }

        /// <summary>
        /// Get FileName according to specific formula
        /// </summary>
        /// <param name="Formula">Formula to make FileName</param>
        /// <returns>System.String contain FileName according to formula or String.Empty</returns>
        private string FormulaFileName(string Formula)
        {
            Formula = Formula.Replace("[Title]", GetTextFrame("TIT2"));

            if (Formula.Contains("[Track]"))
                Formula = Formula.Replace("[Track]", TrackNumber);

            if (Formula.Contains("[00Track]"))
            {
                string T = TrackNumber;
                if (T.Length < 2)
                    T = T.Insert(0, "0");
                Formula = Formula.Replace("[00Track]", T);
            }

            if (Formula.Contains("[000Track]"))
            {
                string T = TrackNumber;
                if (T.Length < 2)
                    T = T.Insert(0, "00");
                else if (T.Length < 3)
                    T = T.Insert(0, "0");
                Formula = Formula.Replace("[000Track]", T);
            }

            Formula = Formula.Replace("[Album]", GetTextFrame("TALB"));

            return Formula + ".mp3";
        }

        /// <summary>
        /// Get TrackNumber for renaming
        /// </summary>
        private string TrackNumber
        {
            get
            {
                string Track = GetTextFrame("TRCK");
                int i = Track.IndexOf('/');
                if (i != -1)
                    Track = Track.Substring(0, i);
                return Track;
            }
        }

        /// <summary>
        /// Add specific ID3Error to ErrorCollection
        /// </summary>
        /// <param name="Error">Error to add</param>
        private void AddError(ID3Error Error)
        {
            _Errors.Add(Error);
        }

        #region -> Public Properties <-

        /// <summary>
        /// Gets Collection of Errors that occured
        /// </summary>
        public ErrorCollection Errors
        {
            get
            { return _Errors; }
        }

        /// <summary>
        /// Gets FileAddress of current ID3v2
        /// </summary>
        public string FilePath
        {
            get
            { return _FilePath; }
        }

        /// <summary>
        /// Get FileName of current ID3v2
        /// </summary>
        public string FileName
        {
            get
            { return Path.GetFileName(_FilePath); }
        }

        /// <summary>
        /// Get Filter of current frame
        /// </summary>
        public FilterCollection Filter
        {
            get
            { return _Filter; }
        }

        /// <summary>
        /// Gets or Sets current Tag filter type
        /// </summary>
        public FilterTypes FilterType
        {
            get
            { return _FilterType; }
            set
            { _FilterType = value; }
        }

        /// <summary>
        /// Gets or Sets Flags of current ID3 Tag
        /// </summary>
        public ID3v2Flags Flags
        {
            get
            { return _Flags; }
            set
            { _Flags = value; }
        }

        /// <summary>
        /// Indicate load Linked frames info while loading Tag
        /// </summary>
        public bool LoadLinkedFrames
        {
            get
            { return _LoadLinkedFrames; }
            set
            { _LoadLinkedFrames = value; }
        }

        /// <summary>
        /// Indicate drop unknown frame while saving ID3 or not
        /// </summary>
        public bool DropUnknowFrames
        {
            get
            { return _DropUnknown; }
            set
            { _DropUnknown = value; }
        }

        /// <summary>
        /// Get version of current ID3 Tag
        /// </summary>
        public Version VersionInfo
        {
            get
            { return _ver; }
        }

        /// <summary>
        /// Indicate if current file have ID3v2 Information
        /// </summary>
        public bool HaveTag
        {
            get
            { return _HaveTag; }
            set
            {
                if (_HaveTag == true && value == false)
                    ClearAll();

                _HaveTag = value;
            }
        }

        /// <summary>
        /// Get length of current ID3 Tag
        /// </summary>
        public int Length
        {
            get
            {
                int RLen = 0;
                foreach (TextFrame TF in _TextFrames.Items)
                    if (TF.IsAvailable)
                        RLen += TF.Length + 10;
                foreach (UserTextFrame UTF in _UserTextFrames.Items)
                    if (UTF.IsAvailable)
                        RLen += UTF.Length + 10;
                foreach (PrivateFrame PF in _PrivateFrames.Items)
                    if (PF.IsAvailable)
                        RLen += PF.Length + 10;
                foreach (TextWithLanguageFrame TWLF in _TextWithLangFrames.Items)
                    if (TWLF.IsAvailable)
                        RLen += TWLF.Length + 10;
                foreach (SynchronisedText ST in _SynchronisedTextFrames.Items)
                    if (ST.IsAvailable)
                        RLen += ST.Length + 10;
                foreach (AttachedPictureFrame AP in _AttachedPictureFrames.Items)
                    if (AP.IsAvailable)
                        RLen += AP.Length + 10;
                foreach (GeneralFileFrame GF in _EncapsulatedObjectFrames.Items)
                    if (GF.IsAvailable)
                        RLen += GF.Length + 10;
                foreach (PopularimeterFrame PF in _PopularimeterFrames.Items)
                    if (PF.IsAvailable)
                        RLen += PF.Length + 10;
                foreach (AudioEncryptionFrame AE in _AudioEncryptionFrames.Items)
                    if (AE.IsAvailable)
                        RLen += AE.Length + 10;
                foreach (LinkFrame LF in _LinkFrames.Items)
                    if (LF.IsAvailable)
                        RLen += LF.Length + 10;
                foreach (TermOfUseFrame TU in _TermOfUseFrames.Items)
                    if (TU.IsAvailable)
                        RLen += TU.Length + 10;
                foreach (DataWithSymbolFrame DS in _DataWithSymbolFrames.Items)
                    if (DS.IsAvailable)
                        RLen += DS.Length + 10;
                if (!_DropUnknown)
                    foreach (BinaryFrame BF in _UnknownFrames.Items)
                        if (BF.IsAvailable)
                            RLen += BF.Length + 10;
                if (_MCDIFrame != null)
                    if (_MCDIFrame.IsAvailable)
                        RLen += _MCDIFrame.Length + 10;
                if (_SYTCFrame != null)
                    if (_SYTCFrame.IsAvailable)
                        RLen += _SYTCFrame.Length + 10;
                if (_PCNTFrame != null)
                    if (_PCNTFrame.IsAvailable)
                        RLen += _PCNTFrame.Length + 10;
                if (_RBUFFrame != null)
                    if (_RBUFFrame.IsAvailable)
                        RLen += _RBUFFrame.Length + 10;
                if (_OWNEFrame != null)
                    if (_OWNEFrame.IsAvailable)
                        RLen += _OWNEFrame.Length + 10;
                if (_COMRFrame != null)
                    if (_COMRFrame.IsAvailable)
                        RLen += _COMRFrame.Length + 10;
                if (_RVRBFrame != null)
                    if (_RVRBFrame.IsAvailable)
                        RLen += _RVRBFrame.Length + 10;
                if (_EQUAFrame != null)
                    if (_EQUAFrame.IsAvailable)
                        RLen += _EQUAFrame.Length + 10;
                if (_RVADFrame != null)
                    if (_RVADFrame.IsAvailable)
                        RLen += _RVADFrame.Length + 10;
                if (_ETCOFrame != null)
                    if (_ETCOFrame.IsAvailable)
                        RLen += _ETCOFrame.Length + 10;
                if (_POSSFrame != null)
                    if (_POSSFrame.IsAvailable)
                        RLen += _POSSFrame.Length + 10;
                return RLen;
            }
        }

        /// <summary>
        /// Indicate if current ID3Info had error while openning
        /// </summary>
        public bool HadError
        {
            get
            {
                if (_Errors.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Gets or sets unicode encoding of AutoTextEncoding
        /// </summary>
        public static TextEncodings DefaultUnicodeEncoding
        {
            get
            { return _DefaultUnicodeEncoding; }
            set
            {
                if ((int)value > 3 || (int)value < 2)
                    throw (new ArgumentOutOfRangeException("Default unicode must be one of (UTF_16, UTF_16BE, UTF_8)"));

                _DefaultUnicodeEncoding = value;
            }
        }

        /// <summary>
        /// Indicate while saving automatically detect encoding of texts of not
        /// </summary>
        public static bool AutoTextEncoding
        {
            get
            { return _AutoTextEncoding; }
            set
            { _AutoTextEncoding = value; }
        }

        #endregion

        #region -> Public Methods <-

        /// <summary>
        /// Load ID3 information from file
        /// </summary>
        /// <exception cref="FileNotFoundException">File Not Found</exception>
        public void Load()
        {
            FileStreamEx ID3File = new FileStreamEx(_FilePath, FileMode.Open);
            if (!ID3File.HaveID3v2()) // If file don't contain ID3v2 exit function
            {
                _HaveTag = false;
                ID3File.Close();
                return;
            }

            _ver = ID3File.ReadVersion(); // Read ID3v2 version           
            _Flags = (ID3v2Flags)ID3File.ReadByte();

            // Extended Header Must Read Here

            ReadFrames(ID3File, ID3File.ReadSize());
            ID3File.Close();
            _HaveTag = true;
        }

        /// <summary>
        /// Save ID3v2 data without renaming file with minor version of 3
        /// </summary>
        public void Save()
        {
            Save(3, "");
        }

        /// <summary>
        /// Save ID3 info to file
        /// </summary>
        /// <param name="Ver">minor version of ID3v2</param>
        /// <param name="Formula">Formula to renaming file</param>
        public void Save(int Ver, string Formula)
        {
            if (Ver < 3 || Ver > 4)
                throw (new ArgumentOutOfRangeException("Version of ID3 can be between 3-4"));

            string NewFilePath;
            if (Formula == "")
                NewFilePath = _FilePath;
            else
            {
                string DirName = Path.GetDirectoryName(_FilePath);
                NewFilePath = DirName + ((DirName.Length > 0) ? "\\" : "") +
                    this.FormulaFileName(Formula);
            }

            FileStreamEx Orgin = new FileStreamEx(_FilePath, FileMode.Open);
            FileStreamEx Temp = new FileStreamEx(NewFilePath + "~TEMP", FileMode.Create);

            int StartIndex = 0;
            if (Orgin.HaveID3v2())
            {
                Orgin.Seek(3, SeekOrigin.Current);
                StartIndex = Orgin.ReadSize();
            }

            if (!_HaveTag)
            {
                SaveRestOfFile(StartIndex, Orgin, Temp, 0);
                return;
            }
            // if Orginal file and current file both don't contain ID3
            // we don't need to do anything

            WriteID3Header(Temp, Ver);

            foreach (TextFrame TF in _TextFrames.Items)
            {
                if (!FramesInfo.IsCompatible(TF.FrameID, Ver))
                    continue;

                if (TF.IsAvailable)
                    TF.FrameStream(Ver).WriteTo(Temp);
            }
            foreach (UserTextFrame UTF in _UserTextFrames.Items)
            {
                if (!FramesInfo.IsCompatible(UTF.FrameID, Ver))
                    continue;

                if (UTF.IsAvailable)
                    UTF.FrameStream(Ver).WriteTo(Temp);
            }
            foreach (PrivateFrame PF in _PrivateFrames.Items)
            {
                if (!FramesInfo.IsCompatible(PF.FrameID, Ver))
                    continue;

                if (PF.IsAvailable)
                    PF.FrameStream(Ver).WriteTo(Temp);
            }
            foreach (TextWithLanguageFrame TWLF in _TextWithLangFrames.Items)
            {
                if (!FramesInfo.IsCompatible(TWLF.FrameID, Ver))
                    continue;

                if (TWLF.IsAvailable)
                    TWLF.FrameStream(Ver).WriteTo(Temp);
            }
            foreach (SynchronisedText ST in _SynchronisedTextFrames.Items)
            {
                if (!FramesInfo.IsCompatible(ST.FrameID, Ver))
                    continue;

                if (ST.IsAvailable)
                    ST.FrameStream(Ver).WriteTo(Temp);
            }
            foreach (AttachedPictureFrame AP in _AttachedPictureFrames.Items)
            {
                if (!FramesInfo.IsCompatible(AP.FrameID, Ver))
                    continue;

                if (AP.IsAvailable)
                    AP.FrameStream(Ver).WriteTo(Temp);
            }
            foreach (GeneralFileFrame GF in _EncapsulatedObjectFrames.Items)
            {
                if (!FramesInfo.IsCompatible(GF.FrameID, Ver))
                    continue;

                if (GF.IsAvailable)
                    GF.FrameStream(Ver).WriteTo(Temp);
            }
            foreach (PopularimeterFrame PF in _PopularimeterFrames.Items)
            {
                if (!FramesInfo.IsCompatible(PF.FrameID, Ver))
                    continue;

                if (PF.IsAvailable)
                    PF.FrameStream(Ver).WriteTo(Temp);
            }
            foreach (AudioEncryptionFrame AE in _AudioEncryptionFrames.Items)
            {
                if (!FramesInfo.IsCompatible(AE.FrameID, Ver))
                    continue;

                if (AE.IsAvailable)
                    AE.FrameStream(Ver).WriteTo(Temp);
            }
            foreach (LinkFrame LF in _LinkFrames.Items)
            {
                if (!FramesInfo.IsCompatible(LF.FrameID, Ver))
                    continue;

                if (LF.IsAvailable)
                    LF.FrameStream(Ver).WriteTo(Temp);
            }
            foreach (TermOfUseFrame TU in _TermOfUseFrames.Items)
            {
                if (!FramesInfo.IsCompatible(TU.FrameID, Ver))
                    continue;

                if (TU.IsAvailable)
                    TU.FrameStream(Ver).WriteTo(Temp);
            }
            foreach (DataWithSymbolFrame DS in _DataWithSymbolFrames.Items)
            {
                if (!FramesInfo.IsCompatible(DS.FrameID, Ver))
                    continue;

                if (DS.IsAvailable)
                    DS.FrameStream(Ver).WriteTo(Temp);
            }
            // Saving Unknown Frames
            if (!_DropUnknown)
                foreach (BinaryFrame BF in _UnknownFrames.Items)
                    if (BF.IsAvailable)
                        BF.FrameStream(Ver).WriteTo(Temp);

            if (_MCDIFrame != null && FramesInfo.IsCompatible(_MCDIFrame.FrameID, Ver))
                if (_MCDIFrame.IsAvailable)
                    _MCDIFrame.FrameStream(Ver).WriteTo(Temp);
            if (_SYTCFrame != null && FramesInfo.IsCompatible(_SYTCFrame.FrameID, Ver))
                if (_SYTCFrame.IsAvailable)
                    _SYTCFrame.FrameStream(Ver).WriteTo(Temp);
            if (_PCNTFrame != null && FramesInfo.IsCompatible(_PCNTFrame.FrameID, Ver))
                if (_PCNTFrame.IsAvailable)
                    _PCNTFrame.FrameStream(Ver).WriteTo(Temp);
            if (_RBUFFrame != null && FramesInfo.IsCompatible(_RBUFFrame.FrameID, Ver))
                if (_RBUFFrame.IsAvailable)
                    _RBUFFrame.FrameStream(Ver).WriteTo(Temp);
            if (_OWNEFrame != null && FramesInfo.IsCompatible(_OWNEFrame.FrameID, Ver))
                if (_OWNEFrame.IsAvailable)
                    _OWNEFrame.FrameStream(Ver).WriteTo(Temp);
            if (_COMRFrame != null && FramesInfo.IsCompatible(_COMRFrame.FrameID, Ver))
                if (_COMRFrame.IsAvailable)
                    _COMRFrame.FrameStream(Ver).WriteTo(Temp);
            if (_RVRBFrame != null && FramesInfo.IsCompatible(_RVRBFrame.FrameID, Ver))
                if (_RVRBFrame.IsAvailable)
                    _RVRBFrame.FrameStream(Ver).WriteTo(Temp);
            if (_EQUAFrame != null && FramesInfo.IsCompatible(_EQUAFrame.FrameID, Ver))
                if (_EQUAFrame.IsAvailable)
                    _EQUAFrame.FrameStream(Ver).WriteTo(Temp);
            if (_RVADFrame != null && FramesInfo.IsCompatible(_RVADFrame.FrameID, Ver))
                if (_RVADFrame.IsAvailable)
                    _RVADFrame.FrameStream(Ver).WriteTo(Temp);
            if (_ETCOFrame != null && FramesInfo.IsCompatible(_ETCOFrame.FrameID, Ver))
                if (_ETCOFrame.IsAvailable)
                    _ETCOFrame.FrameStream(_ver.Minor).WriteTo(Temp);
            if (_POSSFrame != null && FramesInfo.IsCompatible(_POSSFrame.FrameID, Ver))
                if (_POSSFrame.IsAvailable)
                    _POSSFrame.FrameStream(Ver).WriteTo(Temp);

            SaveRestOfFile(StartIndex, Orgin, Temp, Ver);
        }

        private void SaveRestOfFile(int StartIndex, FileStreamEx Orgin,
            FileStreamEx Temp, int Ver)
        {
            Orgin.Seek(StartIndex, SeekOrigin.Begin);

            byte[] Buf = new byte[Orgin.Length - StartIndex];
            Orgin.Read(Buf, 0, Buf.Length);
            Temp.Write(Buf, 0, Buf.Length);
            Orgin.Close();
            Temp.Close();

            if (Ver != 0)
                SetMinorVersion(Ver);

            File.Delete(Orgin.Name);
            string FinallyName = Temp.Name.Remove(Temp.Name.Length - 5);
            File.Move(Temp.Name, FinallyName);
            _FilePath = FinallyName;
        }

        /// <summary>
        /// Save ID3 info to file
        /// </summary>
        /// <param name="Ver">minor version of ID3v2</param>
        public void Save(int Ver)
        {
            Save(Ver, "");
        }

        /// <summary>
        /// Clear all ID3 Tag information
        /// </summary>
        public void ClearAll()
        {
            _TextFrames.Clear();
            _UserTextFrames.Clear();
            _PrivateFrames.Clear();
            _TextWithLangFrames.Clear();
            _SynchronisedTextFrames.Clear();
            _SynchronisedTextFrames.Clear();
            _AttachedPictureFrames.Clear();
            _EncapsulatedObjectFrames.Clear();
            _PopularimeterFrames.Clear();
            _AudioEncryptionFrames.Clear();
            _LinkFrames.Clear();
            _TermOfUseFrames.Clear();
            _DataWithSymbolFrames.Clear();
            _UnknownFrames.Clear();

            _MCDIFrame = null;
            _SYTCFrame = null;
            _PCNTFrame = null;
            _RBUFFrame = null;
            _OWNEFrame = null;
            _COMRFrame = null;
            _RVRBFrame = null;
            _EQUAFrame = null;
            _RVADFrame = null;
            _ETCOFrame = null;
        }

        /// <summary>
        /// Load all linked information frames
        /// </summary>
        public void LoadAllLinkedFrames()
        {
            foreach (LinkFrame LF in _LinkFrames.Items)
                LoadFrameFromFile(LF.FrameIdentifier, LF.URL);
        }

        /// <summary>
        /// Load spefic frame information
        /// </summary>
        /// <param name="FrameID">FrameID to load</param>
        /// <param name="FileAddress">FileAddress to read tag from</param>
        private void LoadFrameFromFile(string FrameID, string FileAddress)
        {
            ID3v2 LinkedInfo = new ID3v2(FileAddress, false);
            LinkedInfo.Filter.Add(FrameID);
            LinkedInfo.FilterType = FilterTypes.LoadFiltersOnly;
            LinkedInfo.Load();

            if (LinkedInfo.HadError)
                foreach (ID3Error IE in LinkedInfo.Errors)
                    _Errors.Add(new ID3Error("In Linked Info(" +
                        FileAddress + "): " + IE.Message, IE.FrameID));

            foreach (TextFrame TF in LinkedInfo._TextFrames)
                _TextFrames.Add(TF);

            foreach (UserTextFrame UT in LinkedInfo._UserTextFrames)
                _UserTextFrames.Add(UT);

            foreach (PrivateFrame PF in LinkedInfo._PrivateFrames)
                _PrivateFrames.Add(PF);

            foreach (TextWithLanguageFrame TWLF in LinkedInfo._TextWithLangFrames)
                _TextWithLangFrames.Add(TWLF);

            foreach (SynchronisedText ST in LinkedInfo._SynchronisedTextFrames)
                _SynchronisedTextFrames.Add(ST);

            foreach (AttachedPictureFrame AP in LinkedInfo._AttachedPictureFrames)
                _AttachedPictureFrames.Add(AP);

            foreach (GeneralFileFrame GF in LinkedInfo._EncapsulatedObjectFrames)
                _EncapsulatedObjectFrames.Add(GF);

            foreach (PopularimeterFrame PF in LinkedInfo._PopularimeterFrames)
                _PopularimeterFrames.Add(PF);

            foreach (AudioEncryptionFrame AE in LinkedInfo._AudioEncryptionFrames)
                _AudioEncryptionFrames.Add(AE);

            // Link to LinkFrame is not available

            foreach (TermOfUseFrame TU in LinkedInfo._TermOfUseFrames)
                _TermOfUseFrames.Add(TU);

            foreach (DataWithSymbolFrame DWS in LinkedInfo._DataWithSymbolFrames)
                _DataWithSymbolFrames.Add(DWS);

            foreach (BinaryFrame BF in LinkedInfo._UnknownFrames)
                _UnknownFrames.Add(BF);

            if (LinkedInfo._MCDIFrame != null)
                _MCDIFrame = LinkedInfo._MCDIFrame;

            if (LinkedInfo._SYTCFrame != null)
                _SYTCFrame = LinkedInfo._SYTCFrame;

            if (LinkedInfo._PCNTFrame != null)
                _PCNTFrame = LinkedInfo._PCNTFrame;

            if (LinkedInfo._RBUFFrame != null)
                _RBUFFrame = LinkedInfo._RBUFFrame;

            if (LinkedInfo._OWNEFrame != null)
                _OWNEFrame = LinkedInfo._OWNEFrame;

            if (LinkedInfo._COMRFrame != null)
                _COMRFrame = LinkedInfo._COMRFrame;

            if (LinkedInfo._RVRBFrame != null)
                _RVRBFrame = LinkedInfo._RVRBFrame;

            if (LinkedInfo._EQUAFrame != null)
                _EQUAFrame = LinkedInfo._EQUAFrame;

            if (LinkedInfo._RVADFrame != null)
                _RVADFrame = LinkedInfo._RVADFrame;

            if (LinkedInfo._ETCOFrame != null)
                _ETCOFrame = LinkedInfo._ETCOFrame;
        }

        /// <summary>
        /// Search TextFrames for specific FrameID
        /// </summary>
        /// <param name="FrameID">FrameID to search in TextFrames</param>
        /// <returns>TextFrame according to FrameID</returns>
        public string GetTextFrame(string FrameID)
        {
            foreach (TextFrame TF in _TextFrames)
                if (TF.FrameID == FrameID)
                    return TF.Text;

            return "";
        }

        /// <summary>
        /// Set text of specific TextFrame
        /// </summary>
        /// <param name="FrameID">FrameID</param>
        /// <param name="Text">Text to set</param>
        /// <param name="TextEncoding">Enxoding of text</param>
        /// <param name="ver">minor version of ID3v2</param>
        public void SetTextFrame(string FrameID, string Text)
        {
            if (!FramesInfo.IsValidFrameID(FrameID))
                return;

            for (int i = 0; i < _TextFrames.Count - 1; i++)
            {
                if (_TextFrames.Items[i].FrameID == FrameID)
                {
                    _TextFrames.RemoveAt(i);
                    break;
                }
            }

            if (Text != "")
            {
                _TextFrames.Add(new TextFrame(FrameID, new FrameFlags(),
                    Text, (Frame.IsAscii(Text) ? TextEncodings.Ascii : _DefaultUnicodeEncoding),
                    _ver.Minor));
            }
        }

        /// <summary>
        /// Set minor version of current ID3v2
        /// </summary>
        /// <param name="ver"></param>
        public void SetMinorVersion(int ver)
        {
            if (ver == 4 || ver == 3)
                _ver = new Version(2, ver, 0);
            else
                throw (new ArgumentException("Minor version can be 3 of 4"));
        }

        public void AttachAnotherFile(string filepath)
        {
            _FilePath = filepath;
        }

        #endregion

        #region -> Private 'Read Methods' <-

        /// <summary>
        /// Read all frames from specific FileStream
        /// </summary>
        /// <param name="Data">FileStream to read data from</param>
        /// <param name="Length">Length of data to read from FileStream</param>
        private void ReadFrames(FileStreamEx Data, int Length)
        {
            string FrameID;
            int FrameLength;
            FrameFlags Flags = new FrameFlags();
            byte Buf;
            // If ID3v2 is ID3v2.2 FrameID, FrameLength of Frames is 3 byte
            // otherwise it's 4 character
            int FrameIDLen = VersionInfo.Minor == 2 ? 3 : 4;

            // Minimum frame size is 10 because frame header is 10 byte
            while (Length > 10)
            {
                // check for padding( 00 bytes )
                Buf = Data.ReadByte();
                if (Buf == 0)
                {
                    Length--;
                    continue;
                }

                // if readed byte is not zero. it must read as FrameID
                Data.Seek(-1, SeekOrigin.Current);

                // ---------- Read Frame Header -----------------------
                FrameID = Data.ReadText(FrameIDLen, TextEncodings.Ascii);
                if (FrameIDLen == 3)
                    FrameID = FramesInfo.Get4CharID(FrameID);
                FrameLength = Convert.ToInt32(Data.ReadUInt(FrameIDLen));
                if (FrameIDLen == 4)
                    Flags = (FrameFlags)Data.ReadUInt(2);
                else
                    Flags = 0; // must set to default flag

                long Position = Data.Position;

                if (Length > 0x10000000)
                    throw (new FileLoadException("This file contain frame that have more than 256MB data"));

                bool Added = false;
                if (IsAddable(FrameID)) // Check if frame is not filter
                    Added = AddFrame(Data, FrameID, FrameLength, Flags);

                if (!Added)
                    // if don't read this frame
                    // we must go forward to read next frame
                    Data.Position = Position + FrameLength;

                Length -= FrameLength + 10;
            }
        }

        /// <summary>
        /// Indicate can add specific frame according to Filter
        /// </summary>
        /// <param name="FrameID">FrameID to check</param>
        /// <returns>true if can add otherwise false</returns>
        private bool IsAddable(string FrameID)
        {
            if (_FilterType == FilterTypes.NoFilter)
                return true;
            else if (_FilterType == FilterTypes.LoadFiltersOnly)
                return _Filter.IsExists(FrameID);
            else // Not Load Filters
                return !_Filter.IsExists(FrameID);
        }

        /// <summary>
        /// Add Frame information to where it must store
        /// </summary>
        /// <param name="Data">FileStream contain Frame</param>
        /// <param name="FrameID">FrameID of frame</param>
        /// <param name="Length">Maximum available length to read</param>
        /// <param name="Flags">Flags of frame</param>
        private bool AddFrame(FileStreamEx Data, string FrameID, int Length, FrameFlags Flags)
        {
            // NOTE: All FrameIDs must be capital letters
            if (!FramesInfo.IsValidFrameID(FrameID))
            {
                AddError(new ID3Error("nonValid Frame found and dropped", FrameID));
                return false;
            }

            int IsText = FramesInfo.IsTextFrame(FrameID, _ver.Minor);
            if (IsText == 1)
            {
                TextFrame TempTextFrame = new TextFrame(FrameID, Flags, Data, Length);
                if (TempTextFrame.IsReadableFrame)
                {
                    _TextFrames.Add(TempTextFrame);
                    return true;
                }
                return false;
            }

            if (IsText == 2)
            {
                UserTextFrame TempUserTextFrame = new UserTextFrame(FrameID, Flags, Data, Length);
                if (TempUserTextFrame.IsReadableFrame)
                {
                    _UserTextFrames.Add(TempUserTextFrame);
                    return true;
                }
                return false;
            }

            switch (FrameID)
            {
                case "UFID":
                case "PRIV":
                    PrivateFrame TempPrivateFrame = new PrivateFrame(FrameID, Flags, Data, Length);
                    if (TempPrivateFrame.IsReadableFrame)
                    {
                        _PrivateFrames.Add(TempPrivateFrame); return true;
                    }
                    else
                        AddError(new ID3Error(TempPrivateFrame.ErrorMessage, FrameID));
                    break;
                case "USLT":
                case "COMM":
                    TextWithLanguageFrame TempTextWithLangFrame = new TextWithLanguageFrame(FrameID, Flags, Data, Length);
                    if (TempTextWithLangFrame.IsReadableFrame)
                    { _TextWithLangFrames.Add(TempTextWithLangFrame); return true; }
                    else
                        AddError(new ID3Error(TempTextWithLangFrame.ErrorMessage, FrameID));
                    break;
                case "SYLT":
                    SynchronisedText TempSynchronisedText = new SynchronisedText(FrameID, Flags, Data, Length);
                    if (TempSynchronisedText.IsReadableFrame)
                    { _SynchronisedTextFrames.Add(TempSynchronisedText); return true; }
                    else
                        AddError(new ID3Error(TempSynchronisedText.ErrorMessage, FrameID));
                    break;
                case "GEOB":
                    GeneralFileFrame TempGeneralFileFrame = new GeneralFileFrame(FrameID, Flags, Data, Length);
                    if (TempGeneralFileFrame.IsReadableFrame)
                    { _EncapsulatedObjectFrames.Add(TempGeneralFileFrame); return true; }
                    else
                        AddError(new ID3Error(TempGeneralFileFrame.ErrorMessage, FrameID));
                    break;
                case "POPM":
                    PopularimeterFrame TempPopularimeterFrame = new PopularimeterFrame(FrameID, Flags, Data, Length);
                    if (TempPopularimeterFrame.IsReadableFrame)
                    { _PopularimeterFrames.Add(TempPopularimeterFrame); return true; }
                    else
                        AddError(new ID3Error(TempPopularimeterFrame.ErrorMessage, FrameID));
                    break;
                case "AENC":
                    AudioEncryptionFrame TempAudioEncryptionFrame = new AudioEncryptionFrame(FrameID, Flags, Data, Length);
                    if (TempAudioEncryptionFrame.IsReadableFrame)
                    { _AudioEncryptionFrames.Add(TempAudioEncryptionFrame); return true; }
                    else
                        AddError(new ID3Error(TempAudioEncryptionFrame.ErrorMessage, FrameID));
                    break;
                case "USER":
                    TermOfUseFrame TempTermOfUseFrame = new TermOfUseFrame(FrameID, Flags, Data, Length);
                    if (TempTermOfUseFrame.IsReadableFrame)
                    { _TermOfUseFrames.Add(TempTermOfUseFrame); return true; }
                    else
                        AddError(new ID3Error(TempTermOfUseFrame.ErrorMessage, FrameID));
                    break;
                case "ENCR":
                case "GRID":
                    DataWithSymbolFrame TempDataWithSymbolFrame = new DataWithSymbolFrame(FrameID, Flags, Data, Length);
                    if (TempDataWithSymbolFrame.IsReadableFrame)
                    { _DataWithSymbolFrames.Add(TempDataWithSymbolFrame); return true; }
                    else
                        AddError(new ID3Error(TempDataWithSymbolFrame.ErrorMessage, FrameID));
                    break;
                case "LINK":
                    LinkFrame LF = new LinkFrame(FrameID, Flags, Data, Length);
                    if (LF.IsReadableFrame)
                    {
                        _LinkFrames.Add(LF);
                        if (_LoadLinkedFrames)
                        { LoadFrameFromFile(LF.FrameIdentifier, LF.URL); return true; }
                    }
                    else
                        AddError(new ID3Error(LF.ErrorMessage, FrameID));
                    break;
                case "APIC":
                    AttachedPictureFrame TempAttachedPictureFrame = new AttachedPictureFrame(FrameID, Flags, Data, Length);
                    if (TempAttachedPictureFrame.IsReadableFrame)
                    { _AttachedPictureFrames.Add(TempAttachedPictureFrame); return true; }
                    else
                        AddError(new ID3Error(TempAttachedPictureFrame.ErrorMessage, FrameID));
                    break;
                case "MCDI":
                    BinaryFrame MCDI = new BinaryFrame(FrameID, Flags, Data, Length);
                    if (MCDI.IsReadableFrame)
                    { _MCDIFrame = MCDI; return true; }
                    else
                        AddError(new ID3Error(MCDI.ErrorMessage, FrameID));
                    break;
                case "SYTC":
                    SynchronisedTempoFrame SYTC = new SynchronisedTempoFrame(FrameID, Flags, Data, Length);
                    if (SYTC.IsReadableFrame)
                    { _SYTCFrame = SYTC; return true; }
                    else
                        AddError(new ID3Error(SYTC.ErrorMessage, FrameID));
                    break;
                case "PCNT":
                    PlayCounterFrame PCNT = new PlayCounterFrame(FrameID, Flags, Data, Length);
                    if (PCNT.IsReadableFrame)
                    { _PCNTFrame = PCNT; return true; }
                    else
                        AddError(new ID3Error(PCNT.ErrorMessage, FrameID));
                    break;
                case "RBUF":
                    RecomendedBufferSizeFrame RBUF = new RecomendedBufferSizeFrame(FrameID, Flags, Data, Length);
                    if (RBUF.IsReadableFrame)
                    { _RBUFFrame = RBUF; return true; }
                    else
                        AddError(new ID3Error(RBUF.ErrorMessage, FrameID));
                    break;
                case "OWNE":
                    OwnershipFrame OWNE = new OwnershipFrame(FrameID, Flags, Data, Length);
                    if (OWNE.IsReadableFrame)
                    { _OWNEFrame = OWNE; return true; }
                    else
                        AddError(new ID3Error(OWNE.ErrorMessage, FrameID));
                    break;
                case "COMR":
                    CommercialFrame COMR = new CommercialFrame(FrameID, Flags, Data, Length);
                    if (COMR.IsReadableFrame)
                    { _COMRFrame = COMR; return true; }
                    else
                        AddError(new ID3Error(COMR.ErrorMessage, FrameID));
                    break;
                case "RVRB":
                    ReverbFrame RVRB = new ReverbFrame(FrameID, Flags, Data, Length);
                    if (RVRB.IsReadableFrame)
                    { _RVRBFrame = RVRB; return true; }
                    else
                        AddError(new ID3Error(RVRB.ErrorMessage, FrameID));
                    break;
                case "EQUA":
                    Equalisation EQUA = new Equalisation(FrameID, Flags, Data, Length);
                    if (EQUA.IsReadableFrame)
                    { _EQUAFrame = EQUA; return true; }
                    else
                        AddError(new ID3Error(EQUA.ErrorMessage, FrameID));
                    break;
                case "RVAD":
                    RelativeVolumeFrame RVAD = new RelativeVolumeFrame(FrameID, Flags, Data, Length);
                    if (RVAD.IsReadableFrame)
                    { _RVADFrame = RVAD; return true; }
                    else
                        AddError(new ID3Error(RVAD.ErrorMessage, FrameID));
                    break;
                case "ETCO":
                    EventTimingCodeFrame ETCO = new EventTimingCodeFrame(FrameID, Flags, Data, Length);
                    if (ETCO.IsReadableFrame)
                    { _ETCOFrame = ETCO; return true; }
                    else
                        AddError(new ID3Error(ETCO.ErrorMessage, FrameID));
                    break;
                case "POSS":
                    PositionSynchronisedFrame POSS = new PositionSynchronisedFrame(FrameID, Flags, Data, Length);
                    if (POSS.IsReadableFrame)
                    { _POSSFrame = POSS; return true; }
                    else
                        AddError(new ID3Error(POSS.ErrorMessage, FrameID));
                    break;
                default:
                    BinaryFrame Temp = new BinaryFrame(FrameID, Flags, Data, Length);
                    if (Temp.IsReadableFrame)
                    { _UnknownFrames.Add(Temp); return true; }
                    else
                        AddError(new ID3Error(Temp.ErrorMessage, FrameID));
                    break;
                // TODO: Mpeg Location
            }

            return false;
        }

        #endregion

        #region -> Public 'Single Time Frames Properties' <-

        /// <summary>
        /// Get MusicCDIdentifier of current ID3
        /// </summary>
        public BinaryFrame MusicCDIdentifier
        {
            get
            { return _MCDIFrame; }
            set
            { _MCDIFrame = value; }
        }

        /// <summary>
        /// Get SynchronisedTempoCodes of current ID3
        /// </summary>
        public SynchronisedTempoFrame SynchronisedTempoCodes
        {
            get
            { return _SYTCFrame; }
        }

        /// <summary>
        /// Get PlayCounter of current ID3
        /// </summary>
        public PlayCounterFrame PlayCounter
        {
            get
            { return _PCNTFrame; }
            set
            { _PCNTFrame = value; }
        }

        /// <summary>
        /// Get RecomendedBuffer of current ID3
        /// </summary>
        public RecomendedBufferSizeFrame RecomendedBuffer
        {
            get
            { return _RBUFFrame; }
        }

        /// <summary>
        /// Get OwnerShip of current ID3
        /// </summary>
        public OwnershipFrame OwnerShip
        {
            get
            { return _OWNEFrame; }
            set
            { _OWNEFrame = value; }
        }

        /// <summary>
        /// Get Commercial of current ID3
        /// </summary>
        public CommercialFrame Commercial
        {
            get
            { return _COMRFrame; }
            set
            { _COMRFrame = value; }
        }

        /// <summary>
        /// Get Reverb of current ID3
        /// </summary>
        public ReverbFrame Reverb
        {
            get
            { return _RVRBFrame; }
        }

        /// <summary>
        /// Get Equalisations of current ID3
        /// </summary>
        public Equalisation Equalisations
        {
            get
            { return _EQUAFrame; }
        }

        /// <summary>
        /// Get RelativeVolume of current ID3
        /// </summary>
        public RelativeVolumeFrame RelativeVolume
        {
            get
            { return _RVADFrame; }
        }

        /// <summary>
        /// Get EventTimingCode of current ID3
        /// </summary>
        public EventTimingCodeFrame EventTimingCode
        {
            get
            { return _ETCOFrame; }
        }

        /// <summary>
        /// Get PositionSynchronised of current ID3
        /// </summary>
        public PositionSynchronisedFrame PositionSynchronised
        {
            get
            { return _POSSFrame; }
        }

        #endregion

        #region -> Public 'Collection Properties' <-

        /// <summary>
        /// Get TextFrame Collection of current ID3
        /// </summary>
        public FramesCollection<TextFrame> TextFrames
        {
            get { return _TextFrames; }
        }

        /// <summary>
        /// Get UserTextFrame Collection of current ID3
        /// </summary>
        public FramesCollection<UserTextFrame> UserTextFrames
        {
            get { return _UserTextFrames; }
        }

        /// <summary>
        /// Get PrivateFrame Collection of current ID3
        /// </summary>
        public FramesCollection<PrivateFrame> PrivateFrames
        {
            get { return _PrivateFrames; }
        }

        /// <summary>
        /// Get TextWithLanguageFrame Collection of current ID3
        /// </summary>
        public FramesCollection<TextWithLanguageFrame> TextWithLanguageFrames
        {
            get { return _TextWithLangFrames; }
        }

        /// <summary>
        /// Get SynchronisedText Collection of current ID3
        /// </summary>
        public FramesCollection<SynchronisedText> SynchronisedTextFrames
        {
            get { return _SynchronisedTextFrames; }
        }

        /// <summary>
        /// Get AttachedPictureFrame Collection of current ID3
        /// </summary>
        public FramesCollection<AttachedPictureFrame> AttachedPictureFrames
        {
            get { return _AttachedPictureFrames; }
        }

        /// <summary>
        /// Get GeneralFileFrame Collection of current ID3
        /// </summary>
        public FramesCollection<GeneralFileFrame> EncapsulatedObjectFrames
        {
            get { return _EncapsulatedObjectFrames; }
        }

        /// <summary>
        /// Get PopularimeterFrame Collection of current ID3
        /// </summary>
        public FramesCollection<PopularimeterFrame> PopularimeterFrames
        {
            get { return _PopularimeterFrames; }
        }

        /// <summary>
        /// Get AudioEncryptionFrame Collection of current ID3
        /// </summary>
        public FramesCollection<AudioEncryptionFrame> AudioEncryptionFrames
        {
            get { return _AudioEncryptionFrames; }
        }

        /// <summary>
        /// Get LinkFrame Collection of current ID3
        /// </summary>
        public FramesCollection<LinkFrame> LinkFrames
        {
            get { return _LinkFrames; }
        }

        /// <summary>
        /// Get TermOfUseFrame Collection of current ID3
        /// </summary>
        public FramesCollection<TermOfUseFrame> TermOfUseFrames
        {
            get { return _TermOfUseFrames; }
        }

        /// <summary>
        /// Get DataWithSymbolFrame Collection of current ID3
        /// </summary>
        public FramesCollection<DataWithSymbolFrame> DataWithSymbolFrames
        {
            get { return _DataWithSymbolFrames; }
        }

        /// <summary>
        /// Get BinaryFrame Collection of current ID3
        /// </summary>
        public FramesCollection<BinaryFrame> UnKnownFrames
        {
            get { return _UnknownFrames; }
        }

        #endregion
    }
}
