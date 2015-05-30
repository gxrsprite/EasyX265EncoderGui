using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easyx264CoderGUI
{

    public class ColorMatrix
    {
        public string x264 = "";
        public string avs = "";
        public string tvrange = "";
        public static ColorMatrix From(string _x264, string _avs, string _tvrange)
        {
            return new ColorMatrix(_x264, _avs, _tvrange);
        }
        public ColorMatrix(string _x264, string _avs, string _tvrange)
        {
            x264 = _x264;
            avs = _avs;
            tvrange = _tvrange;
        }

        public static ColorMatrix bt709 = ColorMatrix.From("bt709", "709", "false");
        public static ColorMatrix YCgCo = ColorMatrix.From("YCgCo", "YCgCo", "true");
        public static ColorMatrix Convert(string Matrix)
        {
            switch (Matrix)
            {
                case "YCbCr.BT709":
                    return ColorMatrix.bt709;
                case "YCgCo":
                    return ColorMatrix.YCgCo;
                default:
                    return ColorMatrix.bt709;
            }
        }
    }

}
