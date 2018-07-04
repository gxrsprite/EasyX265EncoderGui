using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            VSx265();
        }

        private static void x264tmod()
        {

            ProcessStartInfo processinfo = new ProcessStartInfo();
            processinfo.FileName = "tools\\x264_64_tMod-8bit-all.exe";

            processinfo.Arguments = " --demuxer lavf  -o output1.mp4 \"E:\\BaiduYunDownload\\[13.10.28] 2013 한류드림콘서트 티아라 [ Number 9 + 빛(All the cast) ]-io.ts\"";
            processinfo.UseShellExecute = false;    //输出信息重定向
            processinfo.CreateNoWindow = false;
            processinfo.RedirectStandardInput = false;
            processinfo.RedirectStandardOutput = false;
            processinfo.RedirectStandardError = false;
            processinfo.WindowStyle = ProcessWindowStyle.Normal;
            Process ffmpeg = new Process();
            ffmpeg.StartInfo = processinfo;
            ffmpeg.Start();

            //var result = ffmpeg.StandardOutput.ReadToEnd();
            ffmpeg.WaitForExit();
            //ffmpeg.Kill();//等待进程结束
            ffmpeg.Dispose();
        }

        private static void ffmpegpipex264()
        {
            ProcessStartInfo processinfo = new ProcessStartInfo();
            processinfo.FileName = Environment.GetEnvironmentVariable("ComSpec");

            processinfo.Arguments = "/c " + "tools\\ffmpeg  -i \"E:\\BaiduYunDownload\\[13.10.28] 2013 한류드림콘서트 티아라 [ Number 9 + 빛(All the cast) ]-io.ts\" " +
                "-f yuv4mpegpipe -pix_fmt yuv444p -an -v 0 -| tools\\x264_64_tMod-8bit-all.exe --output-csp i444 --demuxer y4m -o output1.mp4 -";
            processinfo.UseShellExecute = false;    //输出信息重定向
            processinfo.CreateNoWindow = false;
            processinfo.RedirectStandardInput = false;
            processinfo.RedirectStandardOutput = false;
            processinfo.RedirectStandardError = false;
            processinfo.WindowStyle = ProcessWindowStyle.Normal;
            Process ffmpeg = new Process();
            ffmpeg.StartInfo = processinfo;
            ffmpeg.Start();

            //var result = ffmpeg.StandardOutput.ReadToEnd();
            ffmpeg.WaitForExit();
            //ffmpeg.Kill();//等待进程结束
            ffmpeg.Dispose();
        }

        private static void VSx265()
        {
            ProcessStartInfo processinfo = new ProcessStartInfo();
            var bat = "\"D:\\Program Files (x86)\\VapourSynth\\core64\\vspipe.exe\" --y4m \"D:\\do.vpy\" - | \"D:\\简单x265批量转码\\tools\\x265-16bit-full.exe\" --y4m --preset medium --merange 25 --no-rect --no-sao --input-depth 10 -o \"somename.mkv\" - ";
            var tempfile = "12345.bat";
            File.WriteAllText(tempfile, bat, Encoding.Default);
            processinfo.FileName = tempfile;
            Process ffmpeg = new Process();
            ffmpeg.StartInfo = processinfo;
            ffmpeg.Start();
        }
    }
}
