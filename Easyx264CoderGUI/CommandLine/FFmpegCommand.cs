using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommonLibrary;

namespace Easyx264CoderGUI
{
    public class FFmpegCommand
    {
        public static string FFmpegExecute = "tools" + Path.DirectorySeparatorChar + "ffmpeg.exe";
        public static string ffmpegPipex265Args = "  -i \"{0}\" -f yuv4mpegpipe -an -v 0 {1} -|";//-deinterlace

        public static string GetFfmpegArgs(FileConfig fileconfig)
        {
            VedioConfig vedioConfig = fileconfig.VedioConfig;
            string args = "";
            if (vedioConfig.deinterlace)
            {
                args += "-deinterlace ";
            }
            if (vedioConfig.csp == "i444")
            {
                args += "-pix_fmt yuv444p ";
            }
            else if (vedioConfig.csp == "i420")
            {
                args += "-pix_fmt yuv420p ";
            }
            else if (vedioConfig.csp == "i422")
            {
                args += "-pix_fmt yuv422p ";
            }
            else if (vedioConfig.csp == "rgb")
            {
                args += "-pix_fmt argb ";
            }
            if (vedioConfig.Resize)
            {
                args += string.Format("-s {0}x{1}", vedioConfig.Width, vedioConfig.Height);
            }
            var result = string.Format(ffmpegPipex265Args, fileconfig.VedioFileFullName.Maohao(), args);

            return result;
        }
    }
}
