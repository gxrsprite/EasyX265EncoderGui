using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using ToolSet;


public class Eac3toCommand
{
    public static string Eac3toExecute = "tools" + Path.DirectorySeparatorChar + "eac3to.exe";

    public static void ConvertMusic(string input, string output, string q, string tracker)
    {
        string bat = string.Format("{0} {3}: {1} -q {2}", input, q, output, tracker);
        ProcessCmd.Run(Eac3toExecute, bat);
    } 
}


