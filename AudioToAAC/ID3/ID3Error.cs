using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace ID3
{
    public class ID3Error
    {
        private string _Message;
        private string _FrameID;

        /// <summary>
        /// Create new ID3Error
        /// </summary>
        /// <param name="ErrorNumber">Error number according to Standard</param>
        /// <param name="Version">Version of ID3</param>
        /// <param name="FrameID">FrameID that error occured in</param>
        /// <param name="Message">Message of error</param>
        /// <param name="ErrorLevel">Error level</param>
        internal ID3Error(string Message, string FrameID)
        {
            this.FrameID = FrameID;
            _Message = Message;
        }

        /// <summary>
        /// Get message of error that occured
        /// </summary>
        public string Message
        { get { return _Message; } }

        /// <summary>
        /// Get FrameID that error occured in
        /// </summary>
        public string FrameID
        {
            get { return _FrameID; }
            private set
            {
                if (value == null)
                    _FrameID = "";
                else
                    _FrameID = value;
            }
        }
    }

    /// <summary>
    /// Static class contain error occured
    /// </summary>
    public class ErrorCollection : IEnumerator  
    {
        private ArrayList _Errors;
        int _Index = -1;

        public ErrorCollection()
        {
            _Errors = new ArrayList();
        }

        internal void Add(ID3Error item)
        { _Errors.Add(item); }

        /// <summary>
        /// Get list of Errors
        /// </summary>
        public ID3Error[] List
        {
            get
            { return (ID3Error[])_Errors.ToArray(typeof(ID3Error)); }
        }

        /// <summary>
        /// Get number of errors that occured
        /// </summary>
        public int Count
        {
            get
            { return _Errors.Count; }
        }

        /// <summary>
        /// Clear list of errors
        /// </summary>
        public void Clear()
        { _Errors.Clear(); }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        #region IEnumerator Members

        public object Current
        {
            get { return _Errors[_Index]; }
        }

        public bool MoveNext()
        {
            _Index++;
            if (_Index < _Errors.Count)
                return true;
            else
            {
                Reset();
                return false;
            }
        }

        public void Reset()
        {
            _Index = -1;
        }

        #endregion
    }
}
