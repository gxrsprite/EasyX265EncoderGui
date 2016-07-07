using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easyx264CoderGUI
{
    [Serializable]
    public class AudioConfig
    {
        public bool UseEac3to = false;
        public bool Enabled = true;
        public float Quality = 0.65f;
        public bool CopyStream = false;
        public int Tracker = 2;
    }
}
