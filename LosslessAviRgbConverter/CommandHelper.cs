using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LosslessAviRgbConverter
{
    public static class CommandHelper
    {
        public static string GetAvsScriptFileName(FileConfig fileConfig)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            string avspluginpath = Path.Combine(Application.StartupPath, "tools\\avsplugin");
            string avstemplete = Resource1.AvsScriptTemplete.Replace("$avspluginpath$", avspluginpath);
            avstemplete = avstemplete.Replace("$inputVedioFileName$", fileConfig.FullName);
            if (vedioConfig.Resize)
            {
                avstemplete = avstemplete.Replace("$resize$", string.Format("nnedi3_resize16({0}, {1},lsb_in=true,lsb=true)", vedioConfig.Width, vedioConfig.Height));
            }
            else
            {
                avstemplete = avstemplete.Replace("$resize$", string.Empty);
            }
            avstemplete = avstemplete.Replace("$depth$", vedioConfig.depth.ToString());
            avstemplete = avstemplete.Replace("$colormatrix$", vedioConfig.ColorMatrix.avs)
                .Replace("$tvrange$", vedioConfig.ColorMatrix.tvrange);

            string filename = Path.GetTempFileName() + ".avs";
            File.WriteAllText(filename, avstemplete);
            return filename;
        }

        public static string RunX264Command(FileConfig fileConfig, string avsPath)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            ProcessStartInfo processinfo = new ProcessStartInfo();
            processinfo.FileName = Path.Combine(Application.StartupPath, "tools\\avs4x264mod.exe");
            //processinfo.FileName = Environment.GetEnvironmentVariable("ComSpec");

            string x264Line = Resource1.x264Line;
            string x264exe = "";
            if (vedioConfig.depth == 10)
            {
                x264exe = Path.Combine(Application.StartupPath, "tools\\x264_64_tMod-10bit-all.exe");
            }
            else
            {
                x264exe = Path.Combine(Application.StartupPath, "tools\\x264_64_tMod-8bit-all.exe");
            }
            x264Line = x264Line.Replace("$x264execute$", x264exe);
            x264Line = x264Line.Replace("$depth$", vedioConfig.depth.ToString());
            x264Line = x264Line.Replace("$preset$", vedioConfig.preset);
            if (string.IsNullOrEmpty(vedioConfig.tune))
            {
                x264Line = x264Line.Replace("$tune$", "");
            }
            else
            {
                x264Line = x264Line.Replace("$tune$", "--tune " + vedioConfig.tune);
            }
            x264Line = x264Line.Replace("$crf$", vedioConfig.crf.ToString());

            string outputpath = string.Empty;
            if (fileConfig.AudioConfig.Enabled)
            {//临时输出
                outputpath = Path.GetTempFileName() + ".mp4";
            }
            else
            {
                outputpath = fileConfig.OutputFile + ".mp4";
            }
            x264Line = x264Line.Replace("$outputfile$", outputpath);
            x264Line = x264Line.Replace("$intputavs$", avsPath);
            x264Line = x264Line.Replace("$userargs$", vedioConfig.UserArgs);
            x264Line = x264Line.Replace("$colormatrix$", vedioConfig.ColorMatrix.x264);
            processinfo.Arguments = x264Line;
            //processinfo.Arguments = "/c \"" + Path.Combine(Application.StartupPath, "tools\\avs4x264mod.exe") + "\" " + x264Line;
            //processinfo.UseShellExecute = false;    //输出信息重定向
            //processinfo.CreateNoWindow = true;
            //processinfo.RedirectStandardInput = true;
            //processinfo.RedirectStandardOutput = true;
            //processinfo.RedirectStandardError = false;
            //processinfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process avsx264mod = new Process();
            avsx264mod.StartInfo = processinfo;
            avsx264mod.Start();

            //var result = avsx264mod.StandardOutput.ReadToEnd();
            //while (!avsx264mod.HasExited)
            //{
            //    Thread.Sleep(1000);
            //    continue;
            //}

            avsx264mod.WaitForExit();
            avsx264mod.Dispose();
            return outputpath;

        }



        public static string RunFFmpegToAAC(FileConfig fileConfig)
        {
            AudioConfig audioConfig = fileConfig.AudioConfig;
            ProcessStartInfo processinfo = new ProcessStartInfo();
            string tmp = Path.GetTempFileName();
            string audiofile = Path.Combine(Path.GetDirectoryName(tmp), Path.GetFileNameWithoutExtension(tmp) + ".aac");
            processinfo.FileName = Environment.GetEnvironmentVariable("ComSpec");
            string bat = getAudiobat(fileConfig.FullName, audiofile, audioConfig);
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

        private static string getAudiobat(string input, string output, AudioConfig audioconfig)
        {
            string ffmpegfile = Path.Combine(Application.StartupPath, "tools\\ffmpeg.exe");
            string neroAacEncfile = Path.Combine(Application.StartupPath, "tools\\neroAacEnc.exe");
            return string.Format("tools\\ffmpeg.exe -vn -i \"{0}\" -f  wav pipe:| tools\\neroAacEnc -ignorelength -q {2} -lc -if - -of \"{1}\"",
                input, output, audioconfig.Quality);
        }



        public static void MKVmergin(FileConfig fileConfig, string vedio, string audio)
        {
            string outfile = fileConfig.OutputFile + ".mkv";
            ProcessStartInfo processinfo = new ProcessStartInfo();
            processinfo.FileName = Path.Combine(Application.StartupPath, "tools\\mkvmerge.exe");
            processinfo.Arguments = string.Format(" -o \"{0}\" \"{1}\" \"{2}\"",
                outfile, vedio, audio
                );
            processinfo.UseShellExecute = false;    //输出信息重定向
            processinfo.CreateNoWindow = true;
            processinfo.RedirectStandardInput = true;
            processinfo.RedirectStandardOutput = true;
            processinfo.RedirectStandardError = false;
            processinfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process avsx264mod = new Process();
            avsx264mod.StartInfo = processinfo;
            avsx264mod.Start();

            avsx264mod.WaitForExit();
            avsx264mod.Dispose();
        }
    }
}
