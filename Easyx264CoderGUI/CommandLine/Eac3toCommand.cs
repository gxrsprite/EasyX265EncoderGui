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
            string bat = $"{Eac3toExecute.Maohao()} {fileConfig.VedioFileFullName.Maohao()} {audioConfig.Tracker}: {audiofile.Maohao()}  -quality={audioConfig.Quality} {audioConfig.CommandLineArgs}";
            ProcessCmd.RunBat(bat, Config.Temp);
            return audiofile;
        }

        public static string ConvertAudioTOpus(FileConfig fileConfig)
        {
            AudioConfig audioConfig = fileConfig.AudioConfig;
            string tmp = Config.Temp;
            string audiofile = FileUtility.RandomName(tmp) + ".opus";
            string bat = $"{Eac3toExecute.Maohao()} {fileConfig.VedioFileFullName.Maohao()}  {audioConfig.Tracker}: stdout.wav | {OpusEnc.Maohao()} --ignorelength -  {audiofile.Maohao()}";
            ProcessCmd.RunBat(bat, Config.Temp);
            return audiofile;
        }

    }
}
