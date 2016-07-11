using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using ToolSet;

namespace Easyx264CoderGUI.CommandLine
{
    public class Eac3toCommand
    {
        public static string Eac3toExecute = "tools" + Path.DirectorySeparatorChar + "eac3to.exe";

        public static string ConvertMusic(FileConfig fileConfig)
        {
            AudioConfig audioConfig = fileConfig.AudioConfig;
            string tmp = Config.Temp;
            string audiofile = FileUtility.RandomName(tmp) + ".m4a";
            string bat = string.Format("{0} {3}: {1} -q {2}", fileConfig.VedioFileFullName, audioConfig.Quality, audiofile, audioConfig.Tracker);
            ProcessCmd.SilenceRun(Eac3toExecute, bat);
            return audiofile;
        }
    }
}
 