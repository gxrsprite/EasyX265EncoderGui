using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommonLibrary;

namespace ToolSet
{
    public class ffmpegCommand
    {
        public static string FFmpegExecute = "tools" + Path.DirectorySeparatorChar + "ffmpeg.exe";

        //分离视频文件
        public static void DemuxVedio(string vedio, string output)
        {
            ProcessCmd.Run(FFmpegExecute, string.Format(" -i {0} -vcodec copy -an {1} ", vedio.Maohao(), output.Maohao()));
        }

        //分离音频文件
        public static void DemuxAudio(string vedio, string output)
        {
            ProcessCmd.Run(FFmpegExecute, string.Format(" -i {0} -acodec copy -vn {1} ", vedio.Maohao(), output.Maohao()));
        }

        //转封装
        public static void ChangeMux(string vedio, string output)
        {
            ProcessCmd.Run(FFmpegExecute, string.Format("-i {0} -vcodec copy -acodec copy  {1} ", vedio.Maohao(), output.Maohao()));
        }

        //混流
        public static void Mux(string vedio, string audio, string output)
        {
            ProcessCmd.Run(FFmpegExecute, string.Format(" -i {0} -vcodec copy -i {1} -acodec copy {3}", vedio.Maohao(), audio.Maohao(), output.Maohao()));
        }
    }
}
