using CommonLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Easyx264CoderGUI
{
    [Serializable]
    public class FileConfig
    {
        public FileConfig()
        {
            lockobj = new object();
            EncoderTaskInfo = new EncoderTaskInfo();
        }

        object lockobj = null;
        public int state = -1;
        public string FullName = "";
        public string AvsFileFullName = "";
        public string VapoursynthFileFullName = "";
        public string VedioFileFullName = "";
        public string DirPath = "";
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
        public string Muxer = "mkv";
        public bool sinablack = false;
        public bool sinaPreblack = false;
        public InputType InputType = InputType.Vedio;
        public AudioConfig AudioConfig = new AudioConfig();
        public VedioConfig VedioConfig = new VedioConfig();
        public MediaInfo mediaInfo = null;

        public string SubPath = null;
        [NonSerialized]
        public EncoderTaskInfo EncoderTaskInfo = null;

        /// <summary>
        /// 使用MediaInfo填充FileConfig里需要填充的数据
        /// </summary>
        public void FillMediaInfo()
        {
            if (this.mediaInfo != null)
            {
                this.mediaInfo.FillFileConfig(this);
            }
        }

        public FileConfig Clone()
        {
            var cloneti = DeepClone.Clone(this);
            cloneti.EncoderTaskInfo = new EncoderTaskInfo();
            return cloneti;
        }
    }


    public enum InputType
    {
        Vedio,
        AvisynthScriptFile,
        AvisynthScript,
        VapoursynthScript,
        VapoursynthScriptFile
    }
}
