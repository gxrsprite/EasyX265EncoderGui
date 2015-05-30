using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LosslessAviRgbConverter
{
    public class FileConfig
    {
        public int state = -1;
        public string FullName { set; get; }
        public string DirPath { set; get; }
        public bool CompleteDo = false;
        public string CompleteAction = "复制到";
        public string CompleteActionDir = string.Empty;
        public bool KeepDirection = false;
        public string OutputPath = "";
        /// <summary>
        /// 输出视频文件，不包含后缀
        /// </summary>
        public string OutputFile = "";
        public string AudioInputFile = "";
        public AudioConfig AudioConfig = new AudioConfig();
        public VedioConfig VedioConfig = new VedioConfig();

    }
}
