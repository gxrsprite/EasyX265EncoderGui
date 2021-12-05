using Easyx264CoderGUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CommonLibrary;

namespace Easyx264CoderGUI
{
    public static class CommandHelper
    {

        public static string RunFFmpegToAAC(FileConfig fileConfig)
        {
            AudioConfig audioConfig = fileConfig.AudioConfig;
            string tmp = Config.Temp;
            string audiofile = FileUtility.RandomName(tmp) + ".aac";

            string bat = string.Empty;
            if (fileConfig.InputType == InputType.AvisynthScriptFile)
            {
                return null;
            }
            else
            {
                bat = getAudiobat(fileConfig.AudioInputFile, audiofile, audioConfig);
            }

            ProcessCmd.RunBat(bat, Config.Temp);

            return audiofile;
        }

        public static string RunFFmpegToOpus(FileConfig fileConfig)
        {
            AudioConfig audioConfig = fileConfig.AudioConfig;
            ProcessStartInfo processinfo = new ProcessStartInfo();
            string tmp = Config.Temp;
            string audiofile = FileUtility.RandomName(tmp) + ".opus";

            string bat = string.Empty;
            if (fileConfig.InputType == InputType.AvisynthScriptFile)
            {
                return null;
            }
            else
            {
                bat = getAudioOpus(fileConfig.AudioInputFile, audiofile, audioConfig);

            }
            ProcessCmd.RunBat(bat, Config.Temp);

            return audiofile;
        }

        private static string getAudiobat(string input, string output, AudioConfig audioconfig)
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
            var audioargs = audioconfig.CommandLineArgs;
            string neroAacEncfile = Path.Combine(Application.StartupPath, "tools\\neroAacEnc.exe");
            return $"{ffmpegfile.Maohao()} -vn -i {input.Maohao()} {audioargs} -f  wav pipe:| {neroAacEncfile.Maohao()} -ignorelength -q {audioconfig.Quality} -lc -if - -of {output.Maohao()}";
        }

        private static string getAudioOpus(string input, string output, AudioConfig audioconfig)
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


            var audioargs = audioconfig.CommandLineArgs;
            if (audioargs.Contains("mixlfe"))
            {
                audioargs = "";
            }
            int bitrat = 0;
            if (audioconfig.Quality != 0)
            {
                if (audioconfig.Quality < 20)
                {
                    bitrat = (int)(audioconfig.Quality * 500);
                }
                else
                {
                    bitrat = (int)audioconfig.Quality;
                }
                audioargs += " -ab " + bitrat.ToString();
            }
            string neroAacEncfile = Path.Combine(Application.StartupPath, "tools\\opusenc");
            return $"{ffmpegfile.Maohao()} -i {input.Maohao()} {audioargs} -f  wav pipe:| {neroAacEncfile.Maohao()} --quiet --ignorelength --vbr --bitrate {bitrat}k  -  {output.Maohao()}";
            //return $"{ffmpegfile.Maohao()} -i {input.Maohao()}  {audioargs} -c:a libopus -vn -vbr on { output.Maohao()}";
        }



        public static string MKVmergin(FileConfig fileConfig, string vedio, string audio, int delay = 0)
        {

            string outfile = FileUtility.GetNoSameNameFile(fileConfig.OutputFile + ".mkv");
            if (string.IsNullOrEmpty(audio))
            {
                if (EncoderHelper.IsHevc(fileConfig.VedioConfig.Encoder))
                {
                    outfile = FileUtility.GetNoSameNameFile(fileConfig.OutputFile + ".h265.mkv");
                }
                else
                {
                    outfile = FileUtility.GetNoSameNameFile(fileConfig.OutputFile + ".h264.mkv");
                }
            }
            ProcessStartInfo processinfo = new ProcessStartInfo();
            if (Config.IsWindows)
            {
                processinfo.FileName = Path.Combine(Application.StartupPath, "tools\\mkvmerge");
            }
            else
            {
                processinfo.FileName = Path.Combine(Application.StartupPath, "mkvmerge");
            }

            var delaystr = delay == 0 ? "" : ("--sync 0:" + delay);
            processinfo.Arguments = $" -o \"{outfile}\" \"{vedio}\" {delaystr} {audio.Maohao()}";
            processinfo.UseShellExecute = false;    //输出信息重定向
            processinfo.CreateNoWindow = true;
            processinfo.RedirectStandardInput = false;
            processinfo.RedirectStandardOutput = false;
            processinfo.RedirectStandardError = false;
            processinfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process avsx264mod = new Process();
            avsx264mod.StartInfo = processinfo;
            avsx264mod.Start();

            avsx264mod.WaitForExit();
            avsx264mod.Dispose();
            return outfile;
        }



        public static string ffmpegmux(FileConfig fileConfig, string vedio, string audio, string extension)
        {
            string outfile = FileUtility.GetNoSameNameFile(fileConfig.OutputFile + "." + extension);
            ProcessStartInfo processinfo = new ProcessStartInfo();
            if (Config.IsWindows)
            {
                processinfo.FileName = Path.Combine(Application.StartupPath, "tools\\ffmpeg.exe");
            }
            else
            {
                processinfo.FileName = "ffmpeg";
            }
            processinfo.Arguments = string.Format("-y -i \"{1}\" -i \"{2}\" -vcodec copy -acodec copy \"{0}\"",
                outfile, vedio, audio
                );
            processinfo.UseShellExecute = false;    //输出信息重定向
            processinfo.CreateNoWindow = true;
            processinfo.RedirectStandardInput = true;
            processinfo.RedirectStandardOutput = true;
            processinfo.RedirectStandardError = false;
            processinfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process ffmpeg = new Process();
            ffmpeg.StartInfo = processinfo;
            ffmpeg.Start();

            ffmpeg.WaitForExit();
            ffmpeg.Dispose();
            return outfile;
        }

        /// <summary>
        /// 无损提取音频
        /// </summary>
        internal static string DemuxAudio(FileConfig fileConfig)
        {
            string tmp = Path.GetRandomFileName();
            string extension;
            if (fileConfig.Muxer == "mkv")
            {
                extension = ".mka";
            }
            else
            {
                extension = ".m4a";
            }

            string audiofile = FileUtility.AppendRandomName(Config.Temp, Path.GetFileNameWithoutExtension(fileConfig.VedioFileFullName) + extension);
            ProcessStartInfo processinfo = new ProcessStartInfo();
            if (Config.IsWindows)
            {
                processinfo.FileName = Path.Combine(Application.StartupPath, "tools\\ffmpeg.exe");
            }
            else
            {
                processinfo.FileName = "ffmpeg";
            }
            processinfo.Arguments = string.Format(" -i \"{0}\" -vn -acodec copy \"{1}\"",
                fileConfig.AudioInputFile, audiofile
                );
            processinfo.UseShellExecute = false;    //输出信息重定向
            processinfo.CreateNoWindow = true;
            processinfo.RedirectStandardInput = true;
            processinfo.RedirectStandardOutput = true;
            processinfo.RedirectStandardError = false;
            processinfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process ffmpeg = new Process();
            ffmpeg.StartInfo = processinfo;
            ffmpeg.Start();
            ffmpeg.WaitForExit();
            ffmpeg.Dispose();
            return audiofile;
        }

        #region Mp4Box
        public static string mp4box(FileConfig fileConfig, string vedio, string audio, int audiodelay = 0)
        {
            string outfile = FileUtility.GetNoSameNameFile(fileConfig.OutputFile + ".mp4");
            ProcessStartInfo processinfo = new ProcessStartInfo();
            if (Config.IsWindows)
            {
                processinfo.FileName = Path.Combine(Application.StartupPath, "tools\\mp4box.exe");
            }
            else
            {
                processinfo.FileName = "mp4box";
            }
            processinfo.Arguments = string.Format("-add \"{1}\" -add \"{2}\" {3} \"{0}\"",
                outfile, vedio, audio, audiodelay == 0 ? "" : ("-delay 2=" + audiodelay)
                );
            processinfo.UseShellExecute = false;    //输出信息重定向
            processinfo.CreateNoWindow = true;
            processinfo.RedirectStandardInput = false;
            processinfo.RedirectStandardOutput = false;
            processinfo.RedirectStandardError = false;
            processinfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process ffmpeg = new Process();
            ffmpeg.StartInfo = processinfo;
            ffmpeg.Start();

            ffmpeg.WaitForExit();
            ffmpeg.Dispose();
            return outfile;
        }
        #endregion
    }
}
