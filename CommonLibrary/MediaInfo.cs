using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CommonLibrary
{
    [Serializable]
    public class MediaInfo
    {
        public string MediaInfoText { set; get; }
        public MediaInfo(string filename)
        {
            ProcessStartInfo processinfo = new ProcessStartInfo();
            string mediainfofile = Path.Combine(Application.StartupPath, "tools\\mediainfo");
            processinfo.FileName = mediainfofile;
            processinfo.Arguments = "\"" + filename + "\"";
            processinfo.UseShellExecute = false;    //输出信息重定向
            processinfo.CreateNoWindow = true;
            processinfo.RedirectStandardInput = true;
            processinfo.RedirectStandardOutput = true;
            processinfo.RedirectStandardError = false;
            processinfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process mediainfo = new Process();
            mediainfo.StartInfo = processinfo;
            mediainfo.Start();
            MediaInfoText = mediainfo.StandardOutput.ReadToEnd();
            mediainfo.Dispose();
        }


        public int DelayRelativeToVideo
        {
            get
            {
                string delayStr = GetValueByText("Delay relative to video").Replace("ms", "");
                int delay = 0;
                int.TryParse(delayStr, out delay);
                return delay;
            }
        }

        public ScanType ScanType
        {
            get
            {
                string value = GetValueByText("Scan type").Replace("ms", "");
                if (value.Equals("Interlaced", StringComparison.OrdinalIgnoreCase))
                {
                    return ScanType.Interlaced;
                }
                else
                {
                    return ScanType.Progressive;
                }
            }
        }

        public ScanOrder ScanOrder
        {
            get
            {
                string value = GetValueByText("Scan order").Replace("ms", "");
                if (value.Contains("Bottom"))
                    return ScanOrder.BottomFieldFirst;
                else
                    return ScanOrder.TopFieldFirst;
            }
        }

        public string GetValueByText(string key)
        {
            return Regex.Match(MediaInfoText, string.Format(@"{0}\s*:\s(?<Value>.*?)\r", key)).Groups["Value"].Value;
        }
    }

    public enum ScanType
    {
        Progressive,
        Interlaced
    }
    public enum ScanOrder
    {
        TopFieldFirst,
        BottomFieldFirst
    }

}
