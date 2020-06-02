using CommonLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Easyx264CoderGUI.CommandLine
{
    class NvEncCommand
    {
        static string nvencexe;
        static NvEncCommand()
        {
            nvencexe = "tools\\NvEncC\\{0}\\{1}";
            if (Environment.Is64BitOperatingSystem)
            {
                nvencexe = string.Format(nvencexe, "x64", "NVEncC64.exe");
            }
            else
            {
                nvencexe = string.Format(nvencexe, "x86", "NVEncC.exe");
            }
            nvencexe = Path.Combine(Application.StartupPath, nvencexe);
        }

        public static string ffmpegPipeNvEnc(FileConfig fileConfig)
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
            AppendUserArgs(vedioConfig);
            string codec = vedioConfig.Encoder == Encoder.NvEnc_H265 ? "-c hevc" : "-c h264";
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

        public static string NvEncSelf(FileConfig fileConfig)
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
            AppendUserArgs(vedioConfig);
            string codec = vedioConfig.Encoder == Encoder.NvEnc_H265 ? "-c hevc" : "-c h264";
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

        public static string NvEncUseVs(FileConfig fileConfig)
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
            AppendUserArgs(vedioConfig);
            string inputfile = "";
            if (fileConfig.InputType == InputType.AvisynthScriptFile)
            {
                inputfile = fileConfig.AvsFileFullName;
            }
            else if (fileConfig.InputType == InputType.VapoursynthScriptFile)
            {
                inputfile = fileConfig.VapoursynthFileFullName;
            }

            string codec = vedioConfig.Encoder == Encoder.NvEnc_H265 ? "-c hevc" : "-c h264";
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

        public static string VspipeNvEnc(FileConfig fileConfig)
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
            AppendUserArgs(vedioConfig);
            string codec = vedioConfig.Encoder == Encoder.NvEnc_H265 ? "-c hevc" : "-c h264";
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

        static void AppendUserArgs(VedioConfig vedioConfig)
        {
            if (vedioConfig.Resize)
            {
                vedioConfig.UserArgs = $"{vedioConfig.UserArgs} --output-res {vedioConfig.Width}x{vedioConfig.Height} ";
            }
        }
    }
}
