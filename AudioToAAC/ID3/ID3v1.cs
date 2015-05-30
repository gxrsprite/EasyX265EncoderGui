using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ID3.ID3v1Frame
{
    /// <summary>
    /// Provide a class to read and write ID3v1
    /// </summary>
    public class ID3v1
    {
        private string _FilePath;
        private string _Title;
        private string _Artist;
        private string _Album;
        private string _Year;
        private string _Comment;
        private byte _TrackNumber;
        private byte _Genre;
        private bool _HaveTag;

        #region -> Public Properties <-

        /// <summary>
        /// Get file path of current ID3v1
        /// </summary>
        public string FilePath
        {
            get
            { return _FilePath; }
            internal set
            { _FilePath = value; }
        }

        /// <summary>
        /// Get file name of current ID3v1
        /// </summary>
        public string FileName
        {
            get
            { return Path.GetFileName(_FilePath); }
        }

        /// <summary>
        /// Get or set Title of current ID3v1
        /// </summary>
        public string Title
        {
            get
            { return _Title; }
            set
            {
                if (value.Length > 30)
                    throw (new ArgumentException("Title Length must be less than 30 characters"));

                _Title = value;
            }
        }

        /// <summary>
        /// Get or set Artist of current ID3v1
        /// </summary>
        public string Artist
        {
            get
            { return _Artist; }
            set
            {
                if (value.Length > 30)
                    throw (new ArgumentException("Artist Length must be less than 30 characters"));
                _Artist = value;
            }
        }

        /// <summary>
        /// Get or set Album of current ID3v1
        /// </summary>
        public string Album
        {
            get
            { return _Album; }
            set
            {
                if (value.Length > 30)
                    throw (new ArgumentException("Album Length must be less than 30 characters"));

                _Album = value;
            }
        }

        /// <summary>
        /// Get or set Year of current ID3v1
        /// </summary>
        public string Year
        {
            get
            { return _Year; }
            set
            {
                if (value.Length > 4)
                    throw (new ArgumentException("Year Length must be less than 4 characters"));
                _Year = value;
            }
        }

        /// <summary>
        /// Get or set Comment of current ID3v1
        /// </summary>
        public string Comment
        {
            get
            { return _Comment; }
            set
            {
                if (value.Length > 28)
                    throw (new ArgumentException("Comment Length must be less than 4 characters"));
                _Comment = value;
            }
        }

        /// <summary>
        /// Get or set TrackNumber of current ID3v1
        /// </summary>
        public byte TrackNumber
        {
            get
            { return _TrackNumber; }
            set
            { _TrackNumber = value; }
        }

        /// <summary>
        /// Get or set Genre of current ID3v1
        /// </summary>
        public byte Genre
        {
            get
            { return _Genre; }
            set
            { _Genre = value; }
        }

        /// <summary>
        /// Indicate if current File contain ID3v1 Information
        /// </summary>
        public bool HaveTag
        {
            get
            { return _HaveTag; }
            set
            {
                if (value == true)
                {
                    FileStream stream = File.Open(_FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                    stream.Seek(-128L,SeekOrigin.End);
                    stream.Write(Encoding.Default.GetBytes("TAG"), 0, 3);
                    stream.Close();                 
                }
                _HaveTag = value;
            }
        }

        #endregion

        /// <summary>
        /// Create new ID3v1 class
        /// </summary>
        /// <param name="FilePath">Path of file</param>
        /// <param name="LoadData">Indicate load data in constructor or not</param>
		public ID3v1(string FilePath, bool LoadData)
		{
			_FilePath = FilePath;
			if (LoadData)
				Load();
			else
			{
				_FilePath = "";
				_Title = "";
				_Artist = "";
				_Album = "";
				_Year = "";
				_Comment = "";
				_TrackNumber = 0;
				_Genre = 0;
				_HaveTag = false;
			}
		}

        /// <summary>
        /// Load ID3v1 information from file
        /// </summary>
		public void Load()
		{
			FileStreamEx FS = new FileStreamEx(_FilePath, FileMode.Open);
			if (!FS.HaveID3v1())
			{
				FS.Close();
				_HaveTag = false;
				return;
			}
			_Title = FS.ReadText(30, TextEncodings.Ascii);
			FS.Seek(-95, SeekOrigin.End);
            _Artist = FS.ReadText(30, TextEncodings.Ascii);
			FS.Seek(-65, SeekOrigin.End);
            _Album = FS.ReadText(30, TextEncodings.Ascii);
			FS.Seek(-35, SeekOrigin.End);
            _Year = FS.ReadText(4, TextEncodings.Ascii);
			FS.Seek(-31, SeekOrigin.End);
            _Comment = FS.ReadText(28, TextEncodings.Ascii);
			FS.Seek(-2, SeekOrigin.End);
			_TrackNumber = FS.ReadByte();
			_Genre = FS.ReadByte();
			FS.Close();
			_HaveTag = true;
		}

        /// <summary>
        /// Save ID3v1 information to file
        /// </summary>
		public void Save()
		{
			FileStreamEx fs = new FileStreamEx(_FilePath, FileMode.Open);
			bool HTag = fs.HaveID3v1();
			if (HTag && !_HaveTag) // just delete ID3
				fs.SetLength(fs.Length - 128);
			else if (!HTag && _HaveTag)
			{
				fs.Seek(0, SeekOrigin.End);
				fs.Write(GetTagBytes, 0, 128);
			}
			else if (HTag && _HaveTag)
			{
				fs.Seek(-128, SeekOrigin.End);
				fs.Write(GetTagBytes, 0, 128);
			}
			fs.Close();
		}

        /// <summary>
        /// Convert data tot Byte Array to write to file
        /// </summary>
        private byte[] GetTagBytes
        {
            get
            {
                byte[] Buf = new byte[128];
                Array.Clear(Buf, 0, 128);
                Encoding.Default.GetBytes("TAG").CopyTo(Buf, 0);
                Encoding.Default.GetBytes(_Title).CopyTo(Buf, 3);
                Encoding.Default.GetBytes(_Artist).CopyTo(Buf, 33);
                Encoding.Default.GetBytes(_Album).CopyTo(Buf, 63);
                Encoding.Default.GetBytes(_Year).CopyTo(Buf, 93);
                Encoding.Default.GetBytes(_Comment).CopyTo(Buf, 97);
                Buf[126] = _TrackNumber;
                Buf[127] = _Genre;
                return Buf;
            }
        }
    }
}
