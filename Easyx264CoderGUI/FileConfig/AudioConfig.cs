using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easyx264CoderGUI
{
   [Serializable]
    public class AudioConfig
    {
        public bool Enabled = true;
        public float Quality = 0.65f;
        public bool CopyStream = false;
    }
}
