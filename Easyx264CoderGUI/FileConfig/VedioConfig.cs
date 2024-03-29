﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easyx264CoderGUI
{
    [Serializable]
    public class VedioConfig
    {
        public EncoderBitrateType BitType = EncoderBitrateType.crf;
        public int bitrate = 3500;
        public float crf = 25f;
        public int depth = 10;
        public string preset = "slow";
        public string tune = "";
        public string UserArgs = "";
        public bool Resize = false;
        public int Width = 1920;
        public int Height = 1080;
        public string csp = "i420";
        public string AvsScript = "";//if inputtype == AvisynthScript
        public string VapoursynthScript = "";
        public Encoder Encoder = Encoder.x264;
        public bool deinterlace = false;
        public bool scanorder = true;
        public string ffmpeg4x265Args = "";
        public string decoderMode = DecoderMode.defaultStr;
        public bool Is_x265_GHFLY_MOD;
    }

    public enum EncoderBitrateType
    {
        crf,
        twopass,
        qp
    }
    public enum Encoder
    {
        x264,
        x265,
        x265_GHFLY_MOD,
        NvEnc_H265,
        NvEnc_H264,
        QSVEnc_H265,
        QSVEnc_H264,
    }

    public class EncoderHelper
    {
        public static bool IsHevc(Encoder encoder)
        {
            switch (encoder)
            {
                case Encoder.x265:
                case Encoder.x265_GHFLY_MOD:
                case Encoder.NvEnc_H265:
                case Encoder.QSVEnc_H265:
                    return true;
            }

            return false;
        }
    }
}
