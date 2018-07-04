using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;

namespace Easyx264CoderGUI
{
    public class FFmpegCommand
    {
        public static string FFmpegExecute = "tools" + Path.DirectorySeparatorChar + "ffmpeg.exe";
        public static string ffmpegPipex265Args = "  -i {0} -f yuv4mpegpipe  {1} -|";//-deinterlace

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

        public static string RunFFmpegToFlac(FileConfig fileConfig)
        {
            AudioConfig audioConfig = fileConfig.AudioConfig;
            ProcessStartInfo processinfo = new ProcessStartInfo();
            string tmp = Config.Temp;
            string audiofile = FileUtility.RandomName(tmp) + ".flac";

            string bat = string.Empty;
            if (fileConfig.InputType == InputType.AvisynthScriptFile)
            {
                return null;
            }
            else
            {
                bat = getAudioFlacBat(fileConfig.AudioInputFile, audiofile, audioConfig);
            }
            ProcessCmd.RunBat(bat, Config.Temp);

            return audiofile;
        }

        private static string getAudioFlacBat(string input, string output, AudioConfig audioconfig)
        {
            string ffmpegfile = "";
            if (Config.IsWindows)
            {
                ffmpegfile = Path.Combine(Application.StartupPath, "tools\\ffmpeg.exe");
            }
            else
            {
                ffmpegfile = "ffmpeg";
            }
            
            return $"{ffmpegfile.Maohao()} -i {input.Maohao()} -c:a flac -compression_level 9  {output.Maohao()}";
            //return $"{ffmpegfile.Maohao()} -i {input.Maohao()}  {audioargs} -c:a libopus -vn -vbr on { output.Maohao()}";
        }
    }
}
