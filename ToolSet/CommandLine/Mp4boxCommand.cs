using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommonLibrary;

namespace ToolSet
{
    public class Mp4boxCommand
    {
        public static string Mp4boxExcute = "tools" + Path.DirectorySeparatorChar + "mp4box.exe";

        public static void Extract(string filename, string ID)
        {
            ProcessCmd.Run(Mp4boxExcute, string.Format("{0 } -raw {1}", filename, ID));
        }

        public static void Mp4boxMux(string vedio, string audio, string outfile, int audiodelay = 0)
        {

            string Arguments = string.Format("-add \"{1}\" -add \"{2}\" {3} \"{0}\"",
                 outfile, vedio, audio, audiodelay == 0 ? "" : ("-delay 2=" + audiodelay)
                 );
            ProcessCmd.Run(Mp4boxExcute, Arguments);
        }


    }
}
