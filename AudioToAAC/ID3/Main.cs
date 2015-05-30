using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using ID3.ID3v2Frames;
using System.Text.RegularExpressions;

namespace ID3
{
    /// <summary>
    /// The main class for any type of frame to inherit
    /// </summary>
    public abstract class Frame
    {
        private string _FrameID; // Contain FrameID of current Frame
        private FrameFlags _FrameFlags; // Contain Flags of current frame
        // After reading frame if must drop value were true it means frame is not readable
        protected bool _MustDrop;
        private bool _IsLinked; // indicate is current frame a linked frame or not
        private string _ErrorMessage; // Contain Error Message if occur

        /// <summary>
        /// Create a new Frame class
        /// </summary>
        /// <param name="FrameID">4 Characters tag identifier</param>
        /// <param name="Flag">Frame Falgs</param>
        protected Frame(string FrameID, FrameFlags Flags)
        {
            // All FrameID letters must be capital
            FrameID = FrameID.ToUpper();

            if (!ValidatingFrameID(FrameID, ValidatingErrorTypes.Exception))
            {
                _MustDrop = true;
                return;
            }

            _FrameFlags = Flags;
            _FrameID = FrameID;
            _MustDrop = false;
            _IsLinked = false;
        }

        /// <summary>
        /// Get or Set flags of current frame
        /// </summary>
        protected FrameFlags FrameFlag
        {
            get
            { return _FrameFlags; }
            set
            { _FrameFlags = value; }
        }

        /// <summary>
        /// Get header of current frame according to specific Size
        /// </summary>
        /// <param name="MinorVersion">Minor version of ID3v2</param>
        /// <returns>MemoryStream contain frame header</returns>
        protected MemoryStream FrameHeader(int MinorVersion)
        {
            byte[] Buf;
            MemoryStream ms = new MemoryStream();
            int FrameIDLength = MinorVersion == 2 ? 3 : 4; // Length of FrameID according to version
            string Temp = _FrameID;

            // if minor version of ID3 were 2, the frameID is 3 character length
            if (MinorVersion == 2)
            {
                Temp = FramesInfo.Get3CharID(Temp);
                if (Temp == null) // This frame is not availabe in this version
                    return null;
            }

            ms.Write(Encoding.ASCII.GetBytes(Temp), 0, FrameIDLength); // Write FrameID
            Buf = BitConverter.GetBytes(Length);
            Array.Reverse(Buf);
            if (MinorVersion == 2)
                ms.Write(Buf, 1, Buf.Length - 1); // Write Frame Size
            else
                ms.Write(Buf, 0, Buf.Length); // Write Frame Size

            if (MinorVersion != 2)
            {
                // If newer than version 2 it have Flags
                Buf = BitConverter.GetBytes((ushort)_FrameFlags);
                Array.Reverse(Buf);
                ms.Write(Buf, 0, Buf.Length); // Write Frame Flag
            }

            return ms;
        }

        /// <summary>
        /// Indicate if this frame is readable
        /// </summary>
        public bool IsReadableFrame
        {
            get
            {
                return (!_MustDrop | IsAvailable);
            }
        }

        protected void ErrorOccur(string Message, bool MustDrop)
        {
            _ErrorMessage = Message;
            _MustDrop = MustDrop;
        }

        #region -> Static Get Members <-

        /// <summary>
        /// Get length of Specific string according to Encoding
        /// </summary>
        /// <param name="Text">Text to get length</param>
        /// <param name="TEncoding">TextEncoding to use for Length calculation</param>
        /// <returns>Length of text</returns>
        protected static int GetTextLength(string Text, TextEncodings TEncoding, bool AddNullCharacter)
        {
            int StringLength;

            StringLength = Text.Length;
            if (TEncoding == TextEncodings.UTF_16 || TEncoding == TextEncodings.UTF_16BE)
                StringLength *= 2; // in UTF-16 each character is 2 bytes

            if (AddNullCharacter)
            {
                if (TEncoding == TextEncodings.UTF_16 || TEncoding == TextEncodings.UTF_16BE)
                    StringLength += 2;
                else
                    StringLength++;
            }

            return StringLength;
        }

        #endregion

        #region -> Write Methods <-

        /// <summary>
        /// Write specific string to specific MemoryStream
        /// </summary>
        /// <param name="Data">MemoryStream to write text to</param>
        /// <param name="Text">Text to write in MemoryStream</param>
        /// <param name="TEncoding">TextEncoding use for text</param>
        /// <param name="AddNullCharacter">indicate if need to add null characters</param>
        protected void WriteText(MemoryStream Data, string Text, TextEncodings TEncoding, bool AddNullCharacter)
        {
            byte[] Buf;
            Buf = FileStreamEx.GetEncoding(TEncoding).GetBytes(Text);
            Data.Write(Buf, 0, Buf.Length);
            if (AddNullCharacter)
            {
                Data.WriteByte(0);
                if (TEncoding == TextEncodings.UTF_16 || TEncoding == TextEncodings.UTF_16BE)
                    Data.WriteByte(0);
            }
        }

        #endregion

        #region -> Validating Methods <-

        protected enum ValidatingErrorTypes
        {
            Nothing = 0,
            ID3Error,
            Exception
        }

        /// <summary>
        /// Indicate is value of Enumeration valid for that enum
        /// </summary>
        /// <param name="Enumeration">Enumeration to control value for</param>
        /// <param name="ErrorType">if not valid how error occur</param>
        /// <returns>true if valid otherwise false</returns>
        protected bool IsValidEnumValue(Enum Enumeration, ValidatingErrorTypes ErrorType)
        {
            if (Enum.IsDefined(Enumeration.GetType(), Enumeration))
                return true;
            else
            {
                if (ErrorType == ValidatingErrorTypes.ID3Error)
                {
                    ErrorOccur(Enumeration.ToString() +
                        " is out of range of " + Enumeration.GetType().ToString(), true);
                }
                else if (ErrorType == ValidatingErrorTypes.Exception)
                    throw (new ArgumentOutOfRangeException(Enumeration.ToString() +
                        " is out of range of " + Enumeration.GetType().ToString()));

                return false;
            }
        }

        protected bool ValidatingFrameID(string FrameIdentifier, ValidatingErrorTypes ErrorType)
        {
            bool IsValid = FramesInfo.IsValidFrameID(FrameIdentifier);

            if (!IsValid)
            {
                if (ErrorType == ValidatingErrorTypes.Exception)
                    throw (new ArgumentException("FrameID must be 4 capital letters"));
                else if (ErrorType == ValidatingErrorTypes.ID3Error)
                    ErrorOccur(FrameIdentifier + " is not valid FrameID", true);
            }

            return IsValid;
        }

        #endregion

        #region -> Abstract method and properties <-

        /// <summary>
        /// Indicate if this frame available
        /// </summary>
        public abstract bool IsAvailable
        {
            get;
        }

        /// <summary>
        /// Get stream containing this frame information
        /// </summary>
        /// <param name="MinorVersion">Minor version of ID3v2</param>
        /// <returns>MemoryStream according to this frame</returns>
        public abstract MemoryStream FrameStream(int MinorVersion);

        /// <summary>
        /// Get Length of current frame
        /// </summary>
        public abstract int Length
        {
            get;
        }

        #endregion

        #region -> Frame Flags Properties <-

        /// <summary>
        /// Get FrameID of current frame
        /// </summary>
        public string FrameID
        {
            get
            { return _FrameID; }
        }

        /// <summary>
        /// Gets or sets if current frame is ReadOnly
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                if ((_FrameFlags & FrameFlags.ReadOnly)
                    == FrameFlags.ReadOnly)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    _FrameFlags |= FrameFlags.ReadOnly;
                else
                    _FrameFlags &= ~FrameFlags.ReadOnly;
            }
        }

        /// <summary>
        /// Gets or sets if current frame is Encrypted
        /// </summary>
        public bool Encryption
        {
            get
            {
                if ((_FrameFlags & FrameFlags.Encryption)
                    == FrameFlags.Encryption)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    _FrameFlags |= FrameFlags.Encryption;
                else
                    _FrameFlags &= ~FrameFlags.Encryption;
            }
        }

        /// <summary>
        /// Gets or sets whether or not frame belongs in a group with other frames
        /// </summary>
        public bool GroupIdentity
        {
            get
            {
                if ((_FrameFlags & FrameFlags.GroupingIdentity)
                    == FrameFlags.GroupingIdentity)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    _FrameFlags |= FrameFlags.GroupingIdentity;
                else
                    _FrameFlags &= ~FrameFlags.GroupingIdentity;
            }
        }

        /// <summary>
        /// Gets or sets whether or not this frame was compressed
        /// </summary>
        public bool Compression
        {
            get
            {
                if ((_FrameFlags & FrameFlags.Compression)
                   == FrameFlags.Compression)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    _FrameFlags |= FrameFlags.Compression;
                else
                    _FrameFlags &= ~FrameFlags.Compression;
            }
        }

        /// <summary>
        /// Gets or sets if it's unknown frame it should be preserved or discared
        /// </summary>
        public bool TagAlterPreservation
        {
            get
            {
                if ((_FrameFlags & FrameFlags.TagAlterPreservation)
                   == FrameFlags.TagAlterPreservation)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    _FrameFlags |= FrameFlags.TagAlterPreservation;
                else
                    _FrameFlags &= ~FrameFlags.TagAlterPreservation;
            }
        }

        /// <summary>
        /// Gets or sets what to do if file excluding frame, Preseved or discared
        /// </summary>
        public bool FileAlterPreservation
        {
            get
            {
                if ((_FrameFlags & FrameFlags.FileAlterPreservation)
                   == FrameFlags.FileAlterPreservation)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    _FrameFlags |= FrameFlags.FileAlterPreservation;
                else
                    _FrameFlags &= ~FrameFlags.FileAlterPreservation;
            }
        }

        /// <summary>
        /// Gets or sets is current frame a linked frame
        /// </summary>
        public bool IsLinked
        {
            get
            { return _IsLinked; }
            set
            { _IsLinked = value; }
        }

        #endregion

        /// <summary>
        /// Retrun a string that represent FrameID of current Frame
        /// </summary>
        /// <returns>FrameID of current Frame</returns>
        public override string ToString()
        {
            return _FrameID;
        }

        /// <summary>
        /// Get error message of current Frame
        /// </summary>
        internal string ErrorMessage
        {
            get
            { return _ErrorMessage; }
        }

        /// <summary>
        /// Indicate if specific text is Ascii
        /// </summary>
        /// <param name="Text">Text to detect</param>
        /// <returns>true if is ascii otherwise false</returns>
        internal static bool IsAscii(string Text)
        {
            return (Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(Text)) == Text);
        }
    }
}