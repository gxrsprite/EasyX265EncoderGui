using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;

namespace Easyx264CoderGUI.CommandLine
{
    public class X264Command
    {

        public static string RunAvsx264mod(FileConfig fileConfig)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            ProcessStartInfo processinfo = new ProcessStartInfo();
            processinfo.UseShellExecute = false;    //输出信息重定向
            processinfo.CreateNoWindow = true;
            processinfo.RedirectStandardInput = true;
            processinfo.RedirectStandardOutput = true;
            processinfo.RedirectStandardError = true;
            processinfo.WindowStyle = ProcessWindowStyle.Hidden;

            processinfo.FileName = Path.Combine(Application.StartupPath, "tools\\avs4x264mod.exe");
            if (!File.Exists(processinfo.FileName))
            {
                throw new EncoderException("找不到指定程序：" + processinfo.FileName);
            }

            string x264exe = GetX264exeFullName(vedioConfig);
            if (!File.Exists(x264exe))
            {
                throw new EncoderException("找不到指定程序：" + x264exe);
            }
            string x264Line;
            string outputpath = "";
            Process avsx264mod = new Process();
            if (vedioConfig.BitType == EncoderBitrateType.crf)
            {
                Getx264Line(fileConfig, 0, out x264Line, out outputpath);
                string avsx264modarg = string.Format("--x264-binary \"{0}\" ", Path.GetFileName(x264exe));
                processinfo.Arguments = avsx264modarg + x264Line;
                avsx264mod.StartInfo = processinfo;
                OutputToText(fileConfig, avsx264mod);
                avsx264mod.Start();
                avsx264mod.BeginOutputReadLine();
                avsx264mod.BeginErrorReadLine();
                avsx264mod.WaitForExit();
            }
            else if (vedioConfig.BitType == EncoderBitrateType.twopass)
            {
                Getx264Line(fileConfig, 1, out x264Line, out outputpath);
                string avsx264modarg = string.Format("--x264-binary \"{0}\" ", x264exe);
                processinfo.Arguments = avsx264modarg + x264Line;
                avsx264mod.StartInfo = processinfo;
                OutputToText(fileConfig, avsx264mod);
                avsx264mod.Start();
                avsx264mod.BeginOutputReadLine();
                avsx264mod.BeginErrorReadLine();
                avsx264mod.WaitForExit();

                {
                    Process avsx264mod2 = new Process();
                    Getx264Line(fileConfig, 2, out x264Line, out outputpath);
                    avsx264modarg = string.Format("--x264-binary \"{0}\" ", x264exe);
                    processinfo.Arguments = avsx264modarg + x264Line;
                    avsx264mod2.StartInfo = processinfo;
                    OutputToText(fileConfig, avsx264mod2);
                    avsx264mod2.Start();
                    avsx264mod2.BeginOutputReadLine();
                    avsx264mod2.BeginErrorReadLine();
                    avsx264mod2.WaitForExit();
                }
            }

            avsx264mod.Dispose();
            return outputpath;

        }
        private static void OutputToText(FileConfig fileConfig, Process avsx264mod)
        {
            avsx264mod.OutputDataReceived += new DataReceivedEventHandler(delegate(object sender, DataReceivedEventArgs e)
            {
                fileConfig.EncoderTaskInfo.AppendOutput(e.Data);
            });
            avsx264mod.ErrorDataReceived += new DataReceivedEventHandler(delegate(object sender, DataReceivedEventArgs e)
            {
                //fileConfig.state = -10;
                //fileConfig.EncoderTaskInfo.AppendOutput("[简单批量x264编码]avs转码错误");
                fileConfig.EncoderTaskInfo.AppendOutput(e.Data);
            });
        }

        public static string RunX264Command(FileConfig fileConfig)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            ProcessStartInfo processinfo = new ProcessStartInfo();

            string x264exe = GetX264exeFullName(vedioConfig);
            if (!File.Exists(x264exe))
            {
                throw new EncoderException("找不到指定程序：" + x264exe);
            }

            processinfo.FileName = x264exe;
            //processinfo.FileName = Environment.GetEnvironmentVariable("ComSpec");
            string x264Line;
            string outputpath = ""; ;

            //processinfo.Arguments = "/c \"" + Path.Combine(Application.StartupPath, "tools\\avs4x264mod.exe") + "\" " + x264Line;
            //processinfo.UseShellExecute = false;    //输出信息重定向
            //processinfo.CreateNoWindow = true;
            //processinfo.RedirectStandardInput = true;
            //processinfo.RedirectStandardOutput = true;
            //processinfo.RedirectStandardError = false;
            //processinfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process avsx264mod = new Process();
            fileConfig.FillMediaInfo();
            if (vedioConfig.BitType == EncoderBitrateType.crf)
            {
                Getx264Line(fileConfig, 0, out x264Line, out outputpath);
                processinfo.Arguments = x264Line;
                avsx264mod.StartInfo = processinfo;
                avsx264mod.Start();
                avsx264mod.WaitForExit();
            }
            else if (vedioConfig.BitType == EncoderBitrateType.twopass)
            {
                Getx264Line(fileConfig, 1, out x264Line, out outputpath);
                processinfo.Arguments = x264Line;
                avsx264mod.StartInfo = processinfo;
                avsx264mod.Start();
                avsx264mod.WaitForExit();

                Getx264Line(fileConfig, 2, out x264Line, out outputpath);
                processinfo.Arguments = x264Line;
                avsx264mod.StartInfo = processinfo;
                avsx264mod.Start();
                avsx264mod.WaitForExit();
            }

            avsx264mod.Dispose();
            return outputpath;

        }

        private static string GetX264exeFullName(VedioConfig vedioConfig)
        {
            string x264exe = "";
            if (vedioConfig.depth == 10)
            {
                x264exe = Path.Combine(Application.StartupPath, "tools\\x264-10b_64.exe");
            }
            else
            {
                x264exe = Path.Combine(Application.StartupPath, "tools\\x264_64.exe");
            }
            if (!Environment.Is64BitOperatingSystem || !File.Exists(x264exe))
            {
                if (vedioConfig.depth == 10)
                {
                    x264exe = Path.Combine(Application.StartupPath, "tools\\x264.exe");
                }
                else
                {
                    x264exe = Path.Combine(Application.StartupPath, "tools\\x264_10b.exe");
                }
            }
            if (!File.Exists(x264exe))
            {
                throw new Exception("程序" + x264exe + "不存在，请下载：www.msystem.waw.pl/x265");
            }
            return x264exe;
        }

        private static void Getx264Line(FileConfig fileConfig, int pass, out string x264Line, out string outputpath, bool ffmpegpipe = false)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            x264Line = Resource1.x264Line;
            x264Line = x264Line.Replace("$preset$", vedioConfig.preset);

            if (vedioConfig.depth == 10)
            {
                if (fileConfig.InputType == InputType.VapoursynthScriptFile)
                {
                    x264Line = x264Line.Replace("$profile$", "--input-depth 10");
                }
                else
                {
                    x264Line = x264Line.Replace("$profile$", "");
                }
            }
            else
            {
                x264Line = x264Line.Replace("$profile$", "");
            }

            if (string.IsNullOrEmpty(vedioConfig.tune))
            {
                x264Line = x264Line.Replace("$tune$", "");
            }
            else
            {
                x264Line = x264Line.Replace("$tune$", "--tune " + vedioConfig.tune);
            }
            if (vedioConfig.BitType == EncoderBitrateType.crf)
            {
                x264Line = x264Line.Replace("$crf$", "--crf " + vedioConfig.crf.ToString());
            }
            else if (vedioConfig.BitType == EncoderBitrateType.twopass)
            {
                string twopassstr = "--pass " + pass + " --bitrate " + vedioConfig.bitrate.ToString();
                x264ArgsManager manager = new x264ArgsManager(x264Line);
                //提供索引
                //if (!string.IsNullOrEmpty(fileConfig.VedioFileFullName))
                //    if (File.Exists(fileConfig.VedioFileFullName + ".lwi") && manager.GetArgValue("demuxer") == "lavf")
                //    {
                //        twopassstr += " --index \"" + fileConfig.VedioFileFullName + ".lwi\" ";
                //    }
                //    else if (File.Exists(fileConfig.VedioFileFullName + ".ffindex") && manager.GetArgValue("demuxer") == "ffms")
                //    {
                //        twopassstr += " --index \"" + fileConfig.VedioFileFullName + ".ffindex\" ";
                //    }

                x264Line = x264Line.Replace("$crf$", twopassstr);
            }
            else if (vedioConfig.BitType == EncoderBitrateType.qp)
            {
                x264Line = x264Line.Replace("$crf$", "--qp " + vedioConfig.crf.ToString());
            }

            if (fileConfig.AudioConfig.Enabled && fileConfig.AudioConfig.CopyStream)
            {
                x264Line = x264Line.Replace("$acodec$", "copy");
            }
            else
            {
                x264Line = x264Line.Replace("$acodec$", "none");
            }
            x264Line = x264Line.Replace("$csp$", vedioConfig.csp);

            if (ffmpegpipe || !vedioConfig.deinterlace || !vedioConfig.Resize)
            {
                x264Line = x264Line.Replace("$resize$", "");
            }
            else
            {
                string vf = "--vf ";
                List<string> vflist = new List<string>();
                if (vedioConfig.deinterlace)
                {
                    string deinterlace = "yadif:mode=2,order=" + (vedioConfig.scanorder ? "tff" : "bff");
                    vflist.Add(deinterlace);
                }
                if (vedioConfig.Resize)
                {
                    string resize = string.Format("resize:width={0},height={1},method=lanczos", vedioConfig.Width, vedioConfig.Height);
                    vflist.Add(resize);
                }
                x264Line = x264Line.Replace("$resize$", vf + string.Join("/", vflist));
            }

            outputpath = string.Empty;

            string fileExtension = "." + fileConfig.Muxer;

            if (fileConfig.AudioConfig.CopyStream || !fileConfig.AudioConfig.Enabled)
            {
                outputpath = fileConfig.OutputFile + fileExtension;
                outputpath = FileUtility.GetNoSameNameFile(outputpath);
            }
            else
            {//临时目录
                outputpath = FileUtility.RandomName(Config.Temp) + ".mp4";

            }
            if (fileConfig.InputType == InputType.AvisynthScriptFile)
            {
                x264Line = x264Line.Replace("$input$", fileConfig.AvsFileFullName);
            }
            else if (fileConfig.InputType == InputType.AvisynthScript)
            {
                x264Line = x264Line.Replace("\"$input$\"", "");
            }
            else
            {
                x264Line = x264Line.Replace("$input$", fileConfig.VedioFileFullName);
            }
            x264Line = x264Line.Replace("$outputfile$", outputpath);

            string log = "--log-file \"" + Path.GetFileNameWithoutExtension(fileConfig.FullName) + "_x264.log\" --log-file-level info ";
            if (fileConfig.InputType == InputType.AvisynthScriptFile || fileConfig.InputType == InputType.AvisynthScript ||
               fileConfig.VedioConfig.BitType == EncoderBitrateType.twopass)
            {
                vedioConfig.UserArgs = vedioConfig.UserArgs.Replace("--demuxer lavf", "");
            }
            x264Line = x264Line.Replace("$userargs$", log + vedioConfig.UserArgs);
        }



        public static string ffmpegPipeX264(FileConfig fileConfig)
        {
            fileConfig.FillMediaInfo();
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            ProcessStartInfo processinfo = new ProcessStartInfo();
            string x264exe = GetX264exeFullName(vedioConfig);
            processinfo.FileName = Environment.GetEnvironmentVariable("ComSpec");
            string x264Line;
            string outputpath = "";
            Getx264Line(fileConfig, 1, out x264Line, out outputpath);
            string ffmpegline = TextManager.Mh + FFmpegCommand.FFmpegExecute + TextManager.Mh + string.Format(FFmpegCommand.ffmpegPipex265Args, fileConfig.VedioFileFullName.Maohao(), vedioConfig.ffmpeg4x265Args);
            processinfo.Arguments = "/c " + ffmpegline + x264exe + " " + x264Line + " -";
            //processinfo.UseShellExecute = false;    //输出信息重定向
            //processinfo.CreateNoWindow = true;
            //processinfo.RedirectStandardInput = true;
            //processinfo.RedirectStandardOutput = true;
            //processinfo.RedirectStandardError = false;
            //processinfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process avsx264mod = new Process();

            if (vedioConfig.BitType == EncoderBitrateType.crf)
            {
                Getx264Line(fileConfig, 0, out x264Line, out outputpath);
                processinfo.Arguments = x264Line;
                avsx264mod.StartInfo = processinfo;
                avsx264mod.Start();
                avsx264mod.WaitForExit();
            }
            else if (vedioConfig.BitType == EncoderBitrateType.twopass)
            {
                Getx264Line(fileConfig, 1, out x264Line, out outputpath);
                processinfo.Arguments = x264Line;
                avsx264mod.StartInfo = processinfo;
                avsx264mod.Start();
                avsx264mod.WaitForExit();

                Getx264Line(fileConfig, 2, out x264Line, out outputpath);
                processinfo.Arguments = x264Line;
                avsx264mod.StartInfo = processinfo;
                avsx264mod.Start();
                avsx264mod.WaitForExit();
            }

            avsx264mod.Dispose();
            return outputpath;
        }
    }
}
