using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CommonLibrary;

namespace Easyx264CoderGUI.CommandLine
{
    public class Eac3toCommand
    {
        public static string Eac3toExecute = "tools" + Path.DirectorySeparatorChar + "eac3to.exe";
        public static string OpusEnc = "tools" + Path.DirectorySeparatorChar + "opusenc";

        public static string ConvertMusic(FileConfig fileConfig)
        {
            AudioConfig audioConfig = fileConfig.AudioConfig;
            string tmp = Config.Temp;
            string audiofile = FileUtility.RandomName(tmp) + ".m4a";
            var eac3to = Path.Combine(Environment.CurrentDirectory, Eac3toExecute);
            string bat = $"{eac3to.Maohao()} {fileConfig.VedioFileFullName.Maohao()} {audioConfig.Tracker}: {audiofile.Maohao()}  -quality={audioConfig.Quality} {audioConfig.CommandLineArgs}";
            ProcessCmd.RunBat(bat, Config.Temp);
            return audiofile;
        }

        public static string ConvertAudioTOpus(FileConfig fileConfig)
        {
            AudioConfig audioConfig = fileConfig.AudioConfig;
            string tmp = Config.Temp;
            string audiofile = FileUtility.RandomName(tmp) + ".opus";
            int bitrat = 0;
            if (audioConfig.Quality < 1)
            {
                bitrat = (int)(audioConfig.Quality * 400);
            }
            else
            {
                bitrat = (int)audioConfig.Quality;
            }
            var eac3to = Path.Combine(Environment.CurrentDirectory, Eac3toExecute);
            var opusenc = Path.Combine(Environment.CurrentDirectory, OpusEnc);
            string bat = $"{eac3to.Maohao()} {fileConfig.VedioFileFullName.Maohao()}  {audioConfig.Tracker}: {audioConfig.CommandLineArgs} stdout.wav | {opusenc.Maohao()} --ignorelength --bitrate {bitrat} --vbr -  {audiofile.Maohao()}";
            ProcessCmd.RunBat(bat, Config.Temp);
            return audiofile;
        }

    }
}
