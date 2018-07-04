using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioToAAC
{
    [Serializable]
    public class AudioConfig
    {
        public string Input;
        public string Output;
        public int Channel = 0;
        public bool UseEac3to = false;
        public bool Enabled = true;
        public float Quality = 0.65f;
        public int Tracker = 2;
        public AudioEncoder Encoder = AudioEncoder.aac;
        public string CommandLineArgs = "";
    }

    public enum AudioEncoder
    {
        aac,
        opus,
        flac
    }
}
