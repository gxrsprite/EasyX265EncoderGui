using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ID3.ID3v1Frame;

namespace ID3
{
    /// <summary>
    /// Provide ID3 Information for a file
    /// </summary>
    public class ID3Info
    {
        private ID3v1 _ID3v1;
        private ID3v2 _ID3v2;

        /// <summary>
        /// Create new ID3 Info class
        /// </summary>
        /// <param name="FileAddress">FileAddress for read ID3 info</param>
        /// <param name="LoadData">Indicate load data in constructor or not</param>
        public ID3Info(string FilePath, bool LoadData)
        {
            _ID3v1 = new ID3v1(FilePath, LoadData);
            _ID3v2 = new ID3v2(FilePath, LoadData);
        }

        /// <summary>
        /// Get ID3 version 2 Tags
        /// </summary>
        public ID3v2 ID3v2Info
        {
            get
            { return _ID3v2; }
        }

        /// <summary>
        /// Gets ID3 version Tag
        /// </summary>
        public ID3v1 ID3v1Info
        {
            get
            { return _ID3v1; }
        }

        /// <summary>
        /// Load both ID3v1 and ID3v2 information from file
        /// </summary>
        public void Load()
        {
            _ID3v2.Load();
            _ID3v1.Load();
        }

        /// <summary>
        /// Save both ID3v2 and ID3v1
        /// </summary>
        public void Save()
        {
            _ID3v2.Save();
            _ID3v1.Save();
        }

        /// <summary>
        /// Save both ID3v2 and ID3v1
        /// </summary>
        /// <param name="ID3v2Version">minor version of ID3v2 to save</param>
        /// <param name="RenameFormula">Rename Formula</param>
        public void Save(int ID3v2Version, string RenameFormula)
        {
            _ID3v1.Save();
            _ID3v2.Save(ID3v2Version, RenameFormula);
            _ID3v1.FilePath = _ID3v2.FilePath;
        }

        /// <summary>
        /// Save both ID3v2 and ID3v1
        /// </summary>
        /// <param name="ID3v2Version">minor version of ID3v2</param>
        public void Save(int ID3v2Version)
        {
            _ID3v1.Save();
            _ID3v2.Save(ID3v2Version);
        }

        public void AttachAnotherFile(string filepath)
        {
            _ID3v1.FilePath = filepath;
            _ID3v2.AttachAnotherFile(filepath);
        }

        /// <summary>
        /// Get FilePath current ID3Info
        /// </summary>
        public string FilePath
        {
            get
            { return _ID3v1.FilePath; }
        }

        /// <summary>
        /// Get FileName of current ID3Info file
        /// </summary>
        public string FileName
        {
            get
            { return _ID3v1.FileName; }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            if (this.FilePath == ((ID3Info)obj).FilePath)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
