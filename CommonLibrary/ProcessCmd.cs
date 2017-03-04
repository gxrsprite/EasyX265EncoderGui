using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    public class ProcessCmd
    {
        public static void SilenceRun(string process, string args)
        {
            ProcessStartInfo processinfo = new ProcessStartInfo();
            processinfo.UseShellExecute = false;
            processinfo.CreateNoWindow = true;
            processinfo.RedirectStandardInput = true;
            processinfo.RedirectStandardOutput = true;
            processinfo.RedirectStandardError = true;
            processinfo.WindowStyle = ProcessWindowStyle.Hidden;
            processinfo.FileName = process;
            if (!File.Exists(process))
            {
                throw new ArgumentException("找不到指定程序：" + process, process);
            }
            processinfo.Arguments = args;
            Process p = new Process();
            p.StartInfo = processinfo;
            p.Start();
            p.WaitForExit();
        }

        public static void Run(string process, string args = "")
        {
            ProcessStartInfo processinfo = new ProcessStartInfo();
            processinfo.FileName = process;
            if (!File.Exists(process))
            {
                throw new ArgumentException("找不到指定程序：" + process, process);
            }
            processinfo.Arguments = args;
            Process p = new Process();
            p.StartInfo = processinfo;
            p.Start();
            p.WaitForExit();
        }

        public static void RunBat(string bat, string temp)
        {
            var batfile = FileUtility.RandomName(temp) + ".bat";
            File.WriteAllText(batfile, bat, Encoding.Default);
            ProcessCmd.Run(batfile);
        }

    }
}
