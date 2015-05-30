using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Easyx264CoderGUI
{
    public class x264ArgsManager
    {
        public x264ArgsManager() { }
        public x264ArgsManager(string args)
        {
            x264ArgsStr = args;
        }
        public string x264ArgsStr { set; get; }

        public x264ArgsManager RemoveArg(string name)
        {
            Regex regex = new Regex(string.Format("(--{0}\\s+.+?)\\s+?", name), RegexOptions.IgnoreCase);
            x264ArgsStr = regex.Replace(x264ArgsStr, " ");
            return this;
        }

        public x264ArgsManager ChangeArgValue(string name, string value)
        {
            Regex regex = new Regex(string.Format("(?<=--{0}\\s+).+?(?=\\s+)", name), RegexOptions.IgnoreCase);
            x264ArgsStr = regex.Replace(x264ArgsStr, value);
            return this;
        }

        public string GetArgValue(string name)
        {
            Regex regex = new Regex(string.Format("(?<=--{0}\\s+).+?(?=\\s+)", name), RegexOptions.IgnoreCase);
            Match match = regex.Match(x264ArgsStr);
            if (match.Success)
            {
                return match.Value;
            }
            return "";
        }

    }
}
