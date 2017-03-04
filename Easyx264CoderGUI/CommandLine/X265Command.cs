using CommonLibrary;
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
    public class X265Command
    {
        public static string x265Excute8 = "tools" + Path.DirectorySeparatorChar + "x265-8bit-full.exe";
        public static string x265Excute8lite = "tools" + Path.DirectorySeparatorChar + "x265.exe";
        public static string x265Excute10 = "tools" + Path.DirectorySeparatorChar + "x265-10bit-full.exe";
        public static string x265Excute10lite = "tools" + Path.DirectorySeparatorChar + "x265-10b.exe";
        public static string avs4x265 = "tools" + Path.DirectorySeparatorChar + "avs4x265.exe";
        public static string x265Args = " $crf$ $profile$  --preset $preset$  $tune$  $userargs$   -o  \"$outputfile$\"  $input$";

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

            processinfo.FileName = Path.Combine(Application.StartupPath, avs4x265);
            var finalX265Path = "";
            if (!File.Exists(processinfo.FileName))
            {
                throw new EncoderException("找不到指定程序：" + processinfo.FileName);
            }
            if (vedioConfig.depth == 8)
            {
                finalX265Path = x265Excute8lite;
            }
            else if (vedioConfig.depth == 10)
            {
                finalX265Path = x265Excute10lite;
            }

            if (!File.Exists(finalX265Path))
            {
                throw new EncoderException("找不到指定程序：" + finalX265Path);
            }
            string x264Line;
            string outputpath = "";
            Process avsx264mod = new Process();
            if (vedioConfig.BitType == EncoderBitrateType.crf)
            {
                Getx265Line(fileConfig, 0, out x264Line, out outputpath);
                string avsx264modarg = string.Format("--x265-binary \"{0}\" ", Path.GetFileName(finalX265Path));
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
                Getx265Line(fileConfig, 1, out x264Line, out outputpath);
                string avsx264modarg = string.Format("--x265-binary \"{0}\" ", finalX265Path);
                processinfo.Arguments = avsx264modarg + x264Line;
                avsx264mod.StartInfo = processinfo;
                OutputToText(fileConfig, avsx264mod);
                avsx264mod.Start();
                avsx264mod.BeginOutputReadLine();
                avsx264mod.BeginErrorReadLine();
                avsx264mod.WaitForExit();

                Getx265Line(fileConfig, 2, out x264Line, out outputpath);
                avsx264modarg = string.Format("--x265-binary \"{0}\" ", finalX265Path);
                processinfo.Arguments = avsx264modarg + x264Line;
                avsx264mod.StartInfo = processinfo;
                OutputToText(fileConfig, avsx264mod);
                avsx264mod.Start();
                avsx264mod.BeginOutputReadLine();
                avsx264mod.BeginErrorReadLine();
                avsx264mod.WaitForExit();
            }

            avsx264mod.Dispose();
            return outputpath;

        }

        private static string GetX265exeFullName(VedioConfig vedioConfig)
        {
            string x265exe = "";
            if (vedioConfig.depth == 10)
            {
                x265exe = x265Excute10;
            }
            else
            {
                x265exe = x265Excute8;
            }
            return x265exe;
        }

        public static string RunX265Command(FileConfig fileConfig)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            ProcessStartInfo processinfo = new ProcessStartInfo();

            string x264exe = GetX265exeFullName(vedioConfig);
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
                Getx265Line(fileConfig, 0, out x264Line, out outputpath);
                processinfo.Arguments = x264Line;
                avsx264mod.StartInfo = processinfo;
                avsx264mod.Start();
                avsx264mod.WaitForExit();
            }
            else if (vedioConfig.BitType == EncoderBitrateType.twopass)
            {
                Getx265Line(fileConfig, 1, out x264Line, out outputpath);
                processinfo.Arguments = x264Line;
                avsx264mod.StartInfo = processinfo;
                avsx264mod.Start();
                avsx264mod.WaitForExit();

                Getx265Line(fileConfig, 2, out x264Line, out outputpath);
                processinfo.Arguments = x264Line;
                avsx264mod.StartInfo = processinfo;
                avsx264mod.Start();
                avsx264mod.WaitForExit();
            }

            avsx264mod.Dispose();
            return outputpath;

        }

        public static string ffmpegPipeX265(FileConfig fileConfig)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            ProcessStartInfo processinfo = new ProcessStartInfo();
            string finalX265Path = "";
            if (vedioConfig.depth == 8)
            {
                finalX265Path = x265Excute8lite;
            }
            else if (vedioConfig.depth == 10)
            {
                finalX265Path = x265Excute10lite;
            }
            if (!File.Exists(finalX265Path))
            {
                throw new EncoderException("找不到指定程序：" + finalX265Path);
            }
            processinfo.FileName = Environment.GetEnvironmentVariable("ComSpec");
            string x264Line;
            string outputpath = "";
            Getx265Line(fileConfig, 1, out x264Line, out outputpath);
            string ffmpegline = TextManager.Mh + FFmpegCommand.FFmpegExecute + TextManager.Mh + FFmpegCommand.GetFfmpegArgs(fileConfig);
            var bat = ffmpegline + finalX265Path + " " + x264Line + " -";
            //processinfo.UseShellExecute = false;    //输出信息重定向
            //processinfo.CreateNoWindow = true;
            //processinfo.RedirectStandardInput = true;
            //processinfo.RedirectStandardOutput = true;
            //processinfo.RedirectStandardError = false;
            //processinfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process avsx264mod = new Process();

            if (vedioConfig.BitType == EncoderBitrateType.crf)
            {
                ProcessCmd.RunBat(bat, Config.Temp);
            }
            else if (vedioConfig.BitType == EncoderBitrateType.twopass)
            {
                Getx265Line(fileConfig, 1, out x264Line, out outputpath);
                var bat1 = ffmpegline + finalX265Path + " " + x264Line + " -";
                ProcessCmd.RunBat(bat1, Config.Temp);

                Getx265Line(fileConfig, 2, out x264Line, out outputpath);
                var bat2 = ffmpegline + finalX265Path + " " + x264Line + " -";
                ProcessCmd.RunBat(bat2, Config.Temp);
            }

            avsx264mod.Dispose();
            return outputpath;
        }


        private static void OutputToText(FileConfig fileConfig, Process avsx264mod)
        {
            avsx264mod.OutputDataReceived += new DataReceivedEventHandler(delegate (object sender, DataReceivedEventArgs e)
            {
                fileConfig.EncoderTaskInfo.AppendOutput(e.Data);
            });
            avsx264mod.ErrorDataReceived += new DataReceivedEventHandler(delegate (object sender, DataReceivedEventArgs e)
            {
                //fileConfig.state = -10;
                //fileConfig.EncoderTaskInfo.AppendOutput("[简单批量x264编码]avs转码错误");
                fileConfig.EncoderTaskInfo.AppendOutput(e.Data);
            });
        }

        private static void Getx265Line(FileConfig fileConfig, int pass, out string x264Line, out string outputpath)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            x264Line = x265Args;
            x264Line = x264Line.Replace("$preset$", vedioConfig.preset);
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
            else
            {
                string twopassstr = "--pass " + pass + " --bitrate " + vedioConfig.bitrate.ToString();
                x264ArgsManager manager = new x264ArgsManager(x264Line);

                x264Line = x264Line.Replace("$crf$", twopassstr);
            }


            x264Line = x264Line.Replace("$profile$", "");

            outputpath = string.Empty;

            string fileExtension = "." + fileConfig.Muxer;

            string inputArg = "";
            if (fileConfig.AudioConfig.CopyStream || !fileConfig.AudioConfig.Enabled)
            {
                outputpath = fileConfig.OutputFile + fileExtension;
            }
            else
            {//临时目录
                outputpath = FileUtility.RandomName(Config.Temp) + ".h265";

            }
            if (fileConfig.InputType == InputType.AvisynthScriptFile)
            {
                x264Line = x264Line.Replace("$input$", fileConfig.AvsFileFullName.Maohao());
            }
            else if (fileConfig.InputType == InputType.AvisynthScript || fileConfig.InputType == InputType.VapoursynthScriptFile)
            {
                x264Line = x264Line.Replace("$input$", "");
            }
            else
            {
                if (fileConfig.UseBat)
                {
                    x264Line = x264Line.Replace("$input$", "");
                    inputArg = " --input - --y4m ";
                }
                else
                {
                    x264Line = x264Line.Replace("$input$", "--input " + fileConfig.VedioFileFullName.Maohao());
                }

            }

            x264Line = x264Line.Replace("$outputfile$", FileUtility.GetNoSameNameFile(outputpath));
            if (fileConfig.UseBat)
            {
                x264Line = x264Line.Replace("$userargs$", vedioConfig.UserArgs + inputArg);
            }
            else
            {
                x264Line = x264Line.Replace("$userargs$", vedioConfig.UserArgs);
            }   
        }



        internal static string RunVSx265(FileConfig fileConfig)
        {
            var vedioConfig = fileConfig.VedioConfig;
            var finalX265Path = "";
            if (vedioConfig.depth == 8)
            {
                finalX265Path = x265Excute8lite;
            }
            else if (vedioConfig.depth == 10)
            {
                finalX265Path = x265Excute10lite;
            }

            finalX265Path = Path.Combine(Environment.CurrentDirectory, finalX265Path);
            if (!File.Exists(finalX265Path))
            {
                throw new EncoderException("找不到指定程序：" + finalX265Path);
            }

            //if (!File.Exists(Config.VspipePath) && !File.Exists(Config.VspipePath + ".exe"))
            //{
            //    throw new EncoderException("找不到指定程序：" + Config.VspipePath);
            //}
            string fileExtension = "." + fileConfig.Muxer;
            var outputpath = string.Empty;
            if (fileConfig.AudioConfig.CopyStream || !fileConfig.AudioConfig.Enabled)
            {
                outputpath = fileConfig.OutputFile + fileExtension;
            }
            else
            {//临时目录
                outputpath = FileUtility.RandomName(Config.Temp) + ".mkv";
            }

            var x265args = "";
            Getx265Line(fileConfig, 0, out x265args, out outputpath);
            ProcessStartInfo processinfo = new ProcessStartInfo();
            var bat = string.Format("\"{0}\" --y4m \"{1}\" - | \"{2}\" --y4m {3} - ", Config.VspipePath, fileConfig.VapoursynthFileFullName, finalX265Path, x265args);
            var batfile = FileUtility.RandomName(Config.Temp) + ".bat";
            File.WriteAllText(batfile, bat, Encoding.Default);
            processinfo.FileName = batfile;
            Process run = new Process();
            run.StartInfo = processinfo;
            run.Start();
            run.WaitForExit();
            return outputpath;
        }
    }
}
