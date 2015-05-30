using Id3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AudioToAAC
{
    public class TagConsole
    {
        public static void ReadTagToID3(Id3Tag id3, string filename)
        {
            ProcessStartInfo processinfo = new ProcessStartInfo();
            string tagfile = Path.Combine(Application.StartupPath, "tools\\tag.exe");
            processinfo.FileName = tagfile;
            processinfo.Arguments = "\"" + filename + "\" --stdout";
            processinfo.UseShellExecute = false;    //输出信息重定向
            processinfo.CreateNoWindow = true;
            processinfo.RedirectStandardInput = true;
            processinfo.RedirectStandardOutput = true;
            processinfo.RedirectStandardError = false;
            processinfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process tag = new Process();
            tag.StartInfo = processinfo;
            tag.Start();

            var result = tag.StandardOutput.ReadToEnd();

            id3.Title.Value = GetValueByText(result, "Title");
            id3.Artists.Value = GetValueByText(result, "Artist");
            id3.Track.Value = GetValueByText(result, "Track");
            id3.Genre.Value = GetValueByText(result, "Genre");
            id3.Year.Value = GetValueByText(result, "Year");
            Id3.Frames.CommentFrame c = new Id3.Frames.CommentFrame();
            c.Comment = GetValueByText(result, "Comment");
            id3.Comments.Add(c);

        }

        public static string GetValueByText(string text, string key)
        {
            return Regex.Match(text, string.Format(@"{0}:\s*(?<{0}>.*?)\r", key)).Groups[key].Value;
        }

    }
}
