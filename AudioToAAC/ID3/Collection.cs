using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.ComponentModel;

namespace ID3.ID3v2Frames
{
    public class FilterCollection
    {
        private ArrayList _Frames;

        internal FilterCollection()
        {
            _Frames = new ArrayList();
        }

        /// <summary>
        /// Add FrameID to FrameList if not exists
        /// </summary>
        /// <param name="FrameID">FrameID to add to list</param>
        public void Add(string FrameID)
        {
            if (!_Frames.Contains(FrameID))
                _Frames.Add(FrameID);
        }

        /// <summary>
        /// Remove Specific frame from list
        /// </summary>
        /// <param name="FrameID">FrameID to remove from list</param>
        public void Remove(string FrameID)
        {
            _Frames.Remove(FrameID);
        }

        /// <summary>
        /// Get list of frames
        /// </summary>
        public string[] Frames
        {
            get
            { return (string[])_Frames.ToArray(typeof(string)); }
        }

        /// <summary>
        /// Remove all frames from frame list
        /// </summary>
        public void Clear()
        {
            _Frames.Clear();
        }

        /// <summary>
        /// Indicate is specific frame in the list
        /// </summary>
        /// <param name="FrameID">FrameID to search</param>
        /// <returns>true if exists false if not</returns>
        public bool IsExists(string FrameID)
        {
            if (_Frames.Contains(FrameID))
                return true;
            else
                return false;
        }
    }

    public class FramesCollection<T>
    {
        private ArrayList _Items; // Store All Data

        /// <summary>
        /// New Frames Collection
        /// </summary>
        internal FramesCollection()
        {
            _Items = new ArrayList();
        }

        /// <summary>
        /// Add new item to list
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            // Remove Item if Exists
            _Items.Remove(item);

            _Items.Add(item);
        }

        /// <summary>
        /// Remove Specific Item from list
        /// </summary>
        /// <param name="item">Item to remove</param>
        public void Remove(T item)
        {
            _Items.Remove(item);
        }

        /// <summary>
        /// Remove at specific position
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            _Items.RemoveAt(index);
        }

        /// <summary>
        /// Clear all
        /// </summary>
        public void Clear()
        {
            _Items.Clear();
        }

        /// <summary>
        /// Array of items
        /// </summary>
        public T[] Items
        {
            get
            { return (T[])_Items.ToArray(typeof(T)); }
        }

        /// <summary>
        /// Get sum of lengths of items
        /// </summary>
        public int Length
        {
            get
            {
                int Len = 0;
                foreach (ILengthable IL in _Items)
                    Len += IL.Length;
                return Len;
            }
        }

        /// <summary>
        /// Sort Items
        /// </summary>
        public void Sort()
        {
            _Items.Sort();
        }

        /// <summary>
        /// Counts items of current FramesCollection
        /// </summary>
        public int Count
        {
            get
            { return _Items.Count; }
        }

        public IEnumerator GetEnumerator()
        {
            return _Items.GetEnumerator();
        }
    }
}
