﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;

namespace AudioToAAC
{
    public class CommandHelper
    {
        public static void RunFFmpegToOpus(AudioConfig audioConfig)
        {
            string tmp = Path.GetTempPath();

            string bat = getAudioOpus(audioConfig);
            ProcessCmd.RunBat(bat, tmp);
        }

        public static string getAudioOpus(AudioConfig audioconfig)
        {
            string ffmpegfile = "";

            ffmpegfile = Path.Combine(Application.StartupPath, "tools\\ffmpeg.exe");


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
            return $"{ffmpegfile.Maohao()} -i {audioconfig.Input.Maohao()} {audioargs} -f  wav pipe:| {neroAacEncfile.Maohao()} --quiet --ignorelength --vbr --bitrate {bitrat}  -  {audioconfig.Output.Maohao()}";
            //return $"{ffmpegfile.Maohao()} -i {input.Maohao()}  {audioargs} -c:a libopus -vn -vbr on { output.Maohao()}";
        }

        public static void RunFFmpegToAAC(AudioConfig audioConfig)
        {
            string bat = getAudiobat(audioConfig);


            ProcessCmd.RunBat(bat, Path.GetTempPath());
        }

        private static string getAudiobat(AudioConfig audioconfig)
        {
            string ffmpegfile = Path.Combine(Application.StartupPath, "tools\\ffmpeg.exe");
            var audioargs = audioconfig.CommandLineArgs;
            if (audioconfig.Channel > 0)
            {
                audioargs += "-ac " + audioconfig.Channel.ToString();
            }
            string neroAacEncfile = Path.Combine(Application.StartupPath, "tools\\neroAacEnc.exe");
            return string.Format("tools\\ffmpeg.exe -vn -i \"{0}\" {3} -f  wav pipe:| tools\\neroAacEnc -ignorelength -q {2} -lc -if - -of \"{1}\"",
                audioconfig.Input, audioconfig.Output, audioconfig.Quality, audioargs);
        }


        public static void RunFFmpegToFlac(AudioConfig audioconfig)
        {
            ProcessStartInfo processinfo = new ProcessStartInfo();
            string bat = getAudioFlacBat(audioconfig.Input, audioconfig.Output, audioconfig);
            ProcessCmd.RunBat(bat, Path.GetTempPath());
        }

        private static string getAudioFlacBat(string input, string output, AudioConfig audioconfig)
        {
            string ffmpegfile = Path.Combine(Application.StartupPath, "tools\\ffmpeg.exe");
            return $"{ffmpegfile.Maohao()} -i {input.Maohao()} -c:a flac -compression_level 9  {output.Maohao()}";
            //return $"{ffmpegfile.Maohao()} -i {input.Maohao()}  {audioargs} -c:a libopus -vn -vbr on { output.Maohao()}";
        }
    }
}
