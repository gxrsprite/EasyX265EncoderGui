using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Easyx264CoderGUI.CommandLine
{
    class QSVEncCommand
    {
        static string nvencexe;
        static QSVEncCommand()
        {
            nvencexe = "tools\\QSVEncC\\{0}\\{1}";
            if (Environment.Is64BitOperatingSystem)
            {
                nvencexe = string.Format(nvencexe, "x64", "QSVEncC64.exe");
            }
            else
            {
                nvencexe = string.Format(nvencexe, "x86", "QSVEncC.exe");
            }
            nvencexe = Path.Combine(Application.StartupPath, nvencexe);
        }

        public static string ffmpegPipeQSVEnc(FileConfig fileConfig)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            ProcessStartInfo processinfo = new ProcessStartInfo();
            string outfile = FileUtility.GetNoSameNameFile(fileConfig.OutputFile + ".h265");
            string ffmpegline = Path.Combine(Application.StartupPath, FFmpegCommand.FFmpegExecute).Maohao();
            string cqp = $" --cqp {vedioConfig.crf}";
            if (vedioConfig.UserArgs.Contains("--cqp") || vedioConfig.UserArgs.Contains("--cbr") || vedioConfig.UserArgs.Contains("--vbr"))
            {
                cqp = "";
            }
            string codec = vedioConfig.Encoder == Encoder.QSVEnc_H265 ? "-c hevc" : "-c h264";
            var bat = $"{ffmpegline} -y -i {fileConfig.VedioFileFullName.Maohao()} -an -pix_fmt yuv420p -f yuv4mpegpipe - | {nvencexe.Maohao()} --y4m {cqp} {codec} --output-depth {vedioConfig.depth} {vedioConfig.UserArgs} -i - -o {outfile.Maohao()}";

            if (vedioConfig.BitType == EncoderBitrateType.crf || vedioConfig.BitType == EncoderBitrateType.qp)
            {
                ProcessCmd.RunBat(bat, Config.Temp);
            }
            else if (vedioConfig.BitType == EncoderBitrateType.twopass)
            {
                throw new NotSupportedException();
            }

            return outfile;
        }

        public static string QSVEncSelf(FileConfig fileConfig)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            ProcessStartInfo processinfo = new ProcessStartInfo();
            string outfile = FileUtility.GetNoSameNameFile(fileConfig.OutputFile + ".h265");
            string ffmpegline = Path.Combine(Application.StartupPath, FFmpegCommand.FFmpegExecute).Maohao();
            string cqp = $" --cqp {vedioConfig.crf}";
            if (vedioConfig.UserArgs.Contains("--cqp") || vedioConfig.UserArgs.Contains("--cbr") || vedioConfig.UserArgs.Contains("--vbr"))
            {
                cqp = "";
            }
            string codec = vedioConfig.Encoder == Encoder.QSVEnc_H265 ? "-c hevc" : "-c h264";
            var bat = $"{nvencexe.Maohao()} {cqp} {codec} --output-depth {vedioConfig.depth} {vedioConfig.UserArgs} -i {fileConfig.VedioFileFullName.Maohao()} -o {outfile.Maohao()}";

            if (vedioConfig.BitType == EncoderBitrateType.crf || vedioConfig.BitType == EncoderBitrateType.qp)
            {
                ProcessCmd.RunBat(bat, Config.Temp);
            }
            else if (vedioConfig.BitType == EncoderBitrateType.twopass)
            {
                throw new NotSupportedException();
            }

            return outfile;
        }

        public static string QSVEncUseVs(FileConfig fileConfig)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            ProcessStartInfo processinfo = new ProcessStartInfo();
            string outfile = FileUtility.GetNoSameNameFile(fileConfig.OutputFile + ".h265");
            string ffmpegline = Path.Combine(Application.StartupPath, FFmpegCommand.FFmpegExecute).Maohao();
            string cqp = $" --cqp {vedioConfig.crf}";
            if (vedioConfig.UserArgs.Contains("--cqp") || vedioConfig.UserArgs.Contains("--cbr") || vedioConfig.UserArgs.Contains("--vbr"))
            {
                cqp = "";
            }

            string inputfile = "";
            if (fileConfig.InputType == InputType.AvisynthScriptFile)
            {
                inputfile = fileConfig.AvsFileFullName;
            }
            else if (fileConfig.InputType == InputType.VapoursynthScriptFile)
            {
                inputfile = fileConfig.VapoursynthFileFullName;
            }

            string codec = vedioConfig.Encoder == Encoder.QSVEnc_H265 ? "-c hevc" : "-c h264";
            var bat = $"{nvencexe.Maohao()} -i {inputfile} {cqp} {codec} --output-depth {vedioConfig.depth} {vedioConfig.UserArgs} -o {outfile.Maohao()}";

            if (vedioConfig.BitType == EncoderBitrateType.crf || vedioConfig.BitType == EncoderBitrateType.qp)
            {
                ProcessCmd.RunBat(bat, Config.Temp);
            }
            else if (vedioConfig.BitType == EncoderBitrateType.twopass)
            {
                throw new NotSupportedException();
            }

            return outfile;
        }

        public static string VspipeQSVEnc(FileConfig fileConfig)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            ProcessStartInfo processinfo = new ProcessStartInfo();
            string outfile = FileUtility.GetNoSameNameFile(fileConfig.OutputFile + ".h265");
            string ffmpegline = Path.Combine(Application.StartupPath, FFmpegCommand.FFmpegExecute).Maohao();
            string cqp = $" --cqp {vedioConfig.crf}";
            if (vedioConfig.UserArgs.Contains("--cqp") || vedioConfig.UserArgs.Contains("--cbr") || vedioConfig.UserArgs.Contains("--vbr"))
            {
                cqp = "";
            }

            string inputfile = "";
            if (fileConfig.InputType == InputType.AvisynthScriptFile)
            {
                inputfile = fileConfig.AvsFileFullName;
            }
            else if (fileConfig.InputType == InputType.VapoursynthScriptFile)
            {
                inputfile = fileConfig.VapoursynthFileFullName;
            }

            string codec = vedioConfig.Encoder == Encoder.QSVEnc_H265 ? "-c hevc" : "-c h264";
            var bat = $"{ Config.VspipePath.Maohao()} --y4m { fileConfig.VapoursynthFileFullName.Maohao()} - | {nvencexe.Maohao()} --y4m {cqp} {codec} --output-depth {vedioConfig.depth} {vedioConfig.UserArgs} -i - -o {outfile.Maohao()}";

            if (vedioConfig.BitType == EncoderBitrateType.crf || vedioConfig.BitType == EncoderBitrateType.qp)
            {
                ProcessCmd.RunBat(bat, Config.Temp);
            }
            else if (vedioConfig.BitType == EncoderBitrateType.twopass)
            {
                throw new NotSupportedException();
            }

            return outfile;
        }
    }
}
