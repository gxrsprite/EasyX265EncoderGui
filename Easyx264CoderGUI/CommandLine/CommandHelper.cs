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
            ProcessStartInfo processinfo = new ProcessStartInfo();
            string tmp = Config.Temp;
            string audiofile = FileUtility.RandomName(tmp) + ".aac";
            processinfo.FileName = Environment.GetEnvironmentVariable("ComSpec");

            string bat = string.Empty;
            if (fileConfig.InputType == InputType.AvisynthScriptFile)
            {
                return null;
            }
            else
            {
                bat = getAudiobat(fileConfig.AudioInputFile, audiofile, audioConfig);
            }

            processinfo.Arguments = "/c " + bat;
            processinfo.UseShellExecute = false;    //输出信息重定向
            processinfo.CreateNoWindow = true;
            processinfo.RedirectStandardInput = true;
            processinfo.RedirectStandardOutput = true;
            processinfo.RedirectStandardError = false;
            processinfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process ffmpeg = new Process();
            ffmpeg.StartInfo = processinfo;
            ffmpeg.Start();

            var result = ffmpeg.StandardOutput.ReadToEnd();
            ffmpeg.WaitForExit();
            //ffmpeg.Kill();//等待进程结束
            ffmpeg.Dispose();

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

            string neroAacEncfile = Path.Combine(Application.StartupPath, "tools\\neroAacEnc.exe");
            return string.Format("tools\\ffmpeg.exe -vn -i \"{0}\" -f  wav pipe:| tools\\neroAacEnc -ignorelength -q {2} -lc -if - -of \"{1}\"",
                input, output, audioconfig.Quality);
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
            //string neroAacEncfile = Path.Combine(Application.StartupPath, "tools\\opusenc");
            //return $"{ffmpegfile.Maohao()} -i {input.Maohao()} -f  wav pipe:| {neroAacEncfile.Maohao()} -ignorelength -  {output.Maohao()}";
            var audioargs = audioconfig.CommandLineArgs;
            if (audioargs.Contains("mixlfe") || audioargs.Contains("dB"))
            {
                audioargs = "";
            }
            return $"{ffmpegfile.Maohao()} -i {input.Maohao()}  {audioargs} -c:a libopus -vn -vbr on { output.Maohao()}";
        }



        public static string MKVmergin(FileConfig fileConfig, string vedio, string audio, int delay = 0)
        {

            string outfile = FileUtility.GetNoSameNameFile(fileConfig.OutputFile + ".mkv");
            if (string.IsNullOrEmpty(audio))
            {
                outfile = FileUtility.GetNoSameNameFile(fileConfig.OutputFile + ".h265.mkv");
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

            processinfo.Arguments = string.Format(" -o \"{0}\" \"{1}\" {3} {2}",
                outfile, vedio, audio.Maohao(), delay == 0 ? "" : ("--sync 0:" + delay)
                );
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
            string audiofile = Path.Combine(Path.GetDirectoryName(tmp), Path.ChangeExtension(tmp, ".mp4"));
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
