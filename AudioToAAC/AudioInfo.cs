using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioToAAC
{
    public class AudioInfo
    {
        public AudioInfo()
        { }
        public AudioInfo(string fullname)
        {
            FullName = fullname;
        }
        public AudioInfo(string fullname, string dirpath)
        {
            FullName = fullname;
            DirPath = dirpath;
        }

        public string FullName { set; get; }
        public string DirPath { set; get; }
    }
}
