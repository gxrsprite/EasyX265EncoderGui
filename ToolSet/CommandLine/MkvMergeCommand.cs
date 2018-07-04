using CommonLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ToolSet
{
    public class MkvMergeCommand
    {
        public static string MkvMergeExcute = "tools" + Path.DirectorySeparatorChar + "mkvmerge.exe";

        public static void MkvMux(string vedio, string outfile)
        {

            string Arguments = $"--output {outfile.Maohao()}  ( { vedio.Maohao()} )";
            ProcessCmd.Run(MkvMergeExcute, Arguments);
        }
    }
}
