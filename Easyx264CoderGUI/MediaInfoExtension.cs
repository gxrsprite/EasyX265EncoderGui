using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easyx264CoderGUI
{
    public static class MediaInfoExtension
    {
        public static void FillFileConfig(this MediaInfo info, FileConfig fileConfig)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            vedioConfig.deinterlace = fileConfig.mediaInfo.ScanType == ScanType.Interlaced ? true : false;
            vedioConfig.scanorder = fileConfig.mediaInfo.ScanOrder == ScanOrder.TopFieldFirst ? true : false;
        }
    }
}
