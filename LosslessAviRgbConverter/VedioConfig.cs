using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LosslessAviRgbConverter
{
    public class VedioConfig
    {
        public float crf = 25f;
        public int depth = 10;
        public string preset = "slow";
        public string tune = "";
        public string UserArgs = "";
        public bool Resize = false;
        public int Width = 1920;
        public int Height = 1080;
        public string InputColorMatrix = "RGB24";
        public ColorMatrix ColorMatrix = ColorMatrix.bt709;

    }
}
