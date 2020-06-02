﻿using CommonLibrary;
using Easyx264CoderGUI.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Tsanie.FlvBugger;

namespace Easyx264CoderGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //uint _message = NativeWrappers.RegisterWindowMessage("BALL");
            //if (_message != 0)
            //{
            //    NativeWrappers.ChangeWindowMessageFilter(_message, NativeWrappers.ChangeWindowMessageFilterFlags.Add);
            //}
            int p = (int)Environment.OSVersion.Platform;
            if ((p == 4) || (p == 128) || (p == 6))
            {
                Config.IsWindows = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cblCompeteAction.SelectedIndex = 0;
            cbVedioConfigTemplete.SelectedIndex = 0;
            cbMuxer.SelectedIndex = 0;
            //txtAvsScript.Text = Resource1.AvsAlmostFilterTemplete.Replace("$avisynth_plugin$", Path.Combine(Application.StartupPath, "tools\\avsplugin"));
            if (Directory.Exists("Template\\avs"))
            {
                var files = Directory.EnumerateFiles("Template\\avs");
                cbAvsTemplate.Items.AddRange(files.Select(f => Path.GetFileName(f)).ToArray());
            }

            if (Directory.Exists("Template\\vs"))
            {
                var files = Directory.EnumerateFiles("Template\\vs");
                combVSTemplate.Items.AddRange(files.Select(f => Path.GetFileName(f)).ToArray());
            }
#if X265
            //this.toolTip1.SetToolTip(this.cbColorDepth, "x265推荐10bit");
            cbColorDepth.Text = "10";
            txtUserArgs.Text = "  --no-sao --strong-intra-smoothing   --no-open-gop --no-rect --no-amp  --ctu 32 --weightb --qpmax 28  --limit-tu=4 --aq-mode 3 --aq-strength 0.8 --min-keyint 1 --merange 44  --keyint 600 --colorprim bt709  --qcomp 0.65 --range limited --pools 16 --vbv-bufsize 27000 --vbv-maxrate 27000 --psy-rd 3 --psy-rdoq 2 --bframes 6 --rdoq-level 2 --weightb --rd 4";
            cbpreset.Text = "slow";
            this.Text = "简单批量x265转码";
#else

#endif
            var encoders = Enum.GetNames(typeof(Encoder));
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(encoders);
            comboBox2.SelectedIndex = 1;

            cbdecoderMode.SelectedIndex = 0;

            if (File.Exists("config.xml"))
            {
                XElement config = XElement.Load("config.xml");
                var temp = (string)config.Element("Temp");
                if (!string.IsNullOrEmpty(temp))
                {
                    Config.Temp = temp;
                    if (!Directory.Exists(Config.Temp))
                    {
                        Directory.CreateDirectory(Config.Temp);
                    }
                }
                else
                {
                    Config.Temp = Path.GetTempPath();
                }

                var vspipe = (string)config.Element("vspipe");
                Config.VspipePath = vspipe;
                var VsPluginPath = (string)config.Element("VsPluginPath");
                if (!string.IsNullOrEmpty(VsPluginPath))
                {
                    Config.VsPluginPath = VsPluginPath;
                }
                else
                {
                    Config.VsPluginPath = "tools\\vsplugin";
                }

            }
            else
            {
                Config.Temp = Path.GetTempPath();
            }

        }

        public static string FileExtension = ".avi|.mp4|.mkv|.wmv|.avs|.ts|.tp|.m2ts|.mov";
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = Path.GetDirectoryName(s[0]);
            }
            foreach (string path in s)
            {
                if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    foreach (string p in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
                    //foreach (string p in FileUtility.GetFiles(path))
                    {
                        AddFileToList(p, path);
                    }
                }
                else
                {
                    AddFileToList(path, Path.GetDirectoryName(path));
                }
            }
        }

        private void AddFileToList(string path, string dir)
        {
            if (FileExtension.Split('|').Any(f => f.Equals(Path.GetExtension(path), StringComparison.OrdinalIgnoreCase)))
            {
                AddFileToListSkipExtgensionCheck(path, dir);
            }
        }

        private void AddFileToListSkipExtgensionCheck(string path, string dir)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = (listView1.Items.Count + 1).ToString();
            lvi.SubItems.Add(Path.GetFileName(path));//.Name = "FileName";
            lvi.SubItems.Add(Path.GetDirectoryName(path));//.Name = "Path";
            FileConfig fileConfig = new FileConfig();
            fileConfig.FullName = path;
            fileConfig.DirPath = dir;
            //检查是否有字幕
            if (cbloadSub.Checked == true)
            {
                var files = Directory.EnumerateFiles(Path.GetDirectoryName(path));
                foreach (var item in files)
                {
                    if (Path.GetFileNameWithoutExtension(item).StartsWith(Path.GetFileNameWithoutExtension(path)) && ".ass,.ssa,.srt".Split(',').Contains(Path.GetExtension(item)))
                    {
                        fileConfig.SubPath = item;
                        break;
                    }
                }
            }


            if (Path.GetExtension(path).Equals(".avs", StringComparison.OrdinalIgnoreCase))
            {
                fileConfig.InputType = InputType.AvisynthScriptFile;
                fileConfig.AvsFileFullName = path;
            }
            else if (Path.GetExtension(path).Equals(".vpy", StringComparison.OrdinalIgnoreCase))
            {
                fileConfig.InputType = InputType.VapoursynthScriptFile;
                fileConfig.AvsFileFullName = path;
            }
            else
            {
                fileConfig.VedioFileFullName = path;
                fileConfig.AudioInputFile = path;
            }
            lvi.Tag = fileConfig;
            listView1.Items.Add(lvi);
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (File.GetAttributes(s[0]).HasFlag(FileAttributes.Directory))
            {
                ((TextBox)sender).Text = s[0];
            }
            else
            {
                ((TextBox)sender).Text = Path.GetDirectoryName(s[0]);
            }
        }

        private void textBoxFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            ((TextBox)sender).Text = s[0];

        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            FileDragEnter(e);
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            FileDragEnter(e);
        }

        private static void FileDragEnter(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        //添加任务
        private void btnAddtoTasklist_Click(object sender, EventArgs e)
        {
            添加到任务列表();
        }

        private void 添加到任务列表()
        {
            foreach (ListViewItem lvi in listView1.Items)
            {
                FileConfig fileConfig = lvi.Tag as FileConfig;

                FillFileConfig(fileConfig);

            }
        }

        private void FillFileConfig(FileConfig fileConfigSrc)
        {
            FileConfig fileConfig = fileConfigSrc.Clone();
            fileConfig.OutputPath = textBox1.Text;
            fileConfig.CompleteDo = cbCompleteDo.Checked;
            fileConfig.CompleteAction = cblCompeteAction.Text;
            fileConfig.CompleteActionDir = textBox2.Text;
            fileConfig.KeepDirection = cbKeepFileTree.Checked;
            if (cbMuxer.Text == TextManager.zhalangflvmuxer)
            {
                fileConfig.Muxer = "flv";
                fileConfig.sinablack = true;
            }
            else if (cbMuxer.Text == TextManager.zhalangflvpreblack)
            {
                fileConfig.Muxer = "flv";
                fileConfig.sinaPreblack = true;
            }
            else
            {
                fileConfig.Muxer = cbMuxer.Text;
            }
            SetVedioConfigByControl(fileConfig);
            SetAudioConfigByControl(fileConfig);
            AddToTaskList(fileConfig);
        }

        private void SetAudioConfigByControl(FileConfig fileConfig)
        {

            AudioConfig audioConfig = fileConfig.AudioConfig;
            audioConfig.Enabled = cbUseAudio.Checked;
            audioConfig.Quality = float.Parse(txtQuality.Text);
            audioConfig.CopyStream = cbcopuaudio.Checked;
            if (fileConfig.InputType == InputType.AvisynthScriptFile && fileConfig.AudioInputFile == string.Empty)
            {
                fileConfig.AudioConfig.Enabled = false;
            }
            audioConfig.UseEac3to = cbUseEac3to.Checked;
            audioConfig.Tracker = int.Parse(txtAudioTracker.Text);
            audioConfig.CommandLineArgs = txtAudioLine.Text;
            if (cbAudioEncoder.SelectedItem.ToString() == "Opus")
            {
                audioConfig.Encoder = AudioEncoder.opus;
            }
            else if (cbAudioEncoder.SelectedItem.ToString() == "AAC")
            {
                audioConfig.Encoder = AudioEncoder.aac;
            }
            else if (cbAudioEncoder.SelectedItem.ToString() == "FLAC")
            {
                audioConfig.Encoder = AudioEncoder.flac;
            }
        }

        private void SetVedioConfigByControl(FileConfig fileConfig)
        {
            VedioConfig vedioConfig = fileConfig.VedioConfig;
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                vedioConfig.crf = float.Parse(textBox3.Text);
                vedioConfig.BitType = EncoderBitrateType.crf;
            }
            else
            {
                vedioConfig.bitrate = int.Parse(txtbitrate.Text);
                vedioConfig.BitType = EncoderBitrateType.twopass;
            }
            if (cbEnableQpmod.Checked)
            {
                vedioConfig.BitType = EncoderBitrateType.qp;
            }
            //if (cbEnableX265.Checked)

            //vedioConfig.ffmpeg4x265Args = txtffmpeg4x265.Text;
            vedioConfig.Encoder = (Encoder)Enum.Parse(typeof(Encoder), comboBox2.Text);

            vedioConfig.UserArgs = txtUserArgs.Text;
            vedioConfig.depth = int.Parse(cbColorDepth.Text);
            vedioConfig.preset = cbpreset.Text;
            vedioConfig.tune = comboBox1.Text;
            vedioConfig.Resize = checkBox1.Checked;
            vedioConfig.csp = cbcsp.Text;
            vedioConfig.Width = textBox4.Text == "" ? 0 : int.Parse(textBox4.Text);
            vedioConfig.Height = textBox5.Text == "" ? 0 : int.Parse(textBox5.Text);
            if (cbUseAvsTemplete.Checked)
            {//处理自定义avs模板
                fileConfig.InputType = InputType.AvisynthScript;
                string avsscript = txtAvsScript.Text;
                avsscript = avsscript.Replace("$InputVedio$", fileConfig.VedioFileFullName)
                    .Replace("$avisynth_plugin$", Path.Combine(Application.StartupPath, "tools\\avsplugin"));
                vedioConfig.AvsScript = avsscript;
            }
            else if (cbUseVSTemplete.Checked)
            {//处理自定义vs模板
                fileConfig.InputType = InputType.VapoursynthScript;
                string avsscript = txtVsScript.Text;
                avsscript = avsscript.Replace("$InputVedio$", fileConfig.VedioFileFullName)
                    .Replace("$vapoursynth_plugin$", Config.VsPluginPath)
                    .Replace("$InputVedioWithoutExtension$", FileUtility.GetFullNameWithoutExtension(fileConfig.VedioFileFullName));


                var dgipath = Path.ChangeExtension(fileConfig.VedioFileFullName, ".dgi");
                avsscript = avsscript.Replace("$InputDgi$", dgipath);
                vedioConfig.VapoursynthScript = avsscript;
            }
            vedioConfig.decoderMode = cbdecoderMode.SelectedItem.ToString();
        }

        private void AddToTaskList(FileConfig fileConfig)
        {
            ListViewItem lvi2 = new ListViewItem();
            lvi2.Text = (listView2.Items.Count + 1).ToString();
            lvi2.SubItems.Add(Path.GetFileName(fileConfig.FullName));//.Name = "FileName";
            lvi2.SubItems.Add("待转码").Name = "States";
            lvi2.SubItems.Add(fileConfig.DirPath);//.Name = "Path";
            lvi2.Tag = fileConfig;
            listView2.Items.Add(lvi2);
        }

        private void btnClearList_Click_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void 删除此任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView2.SelectedItems)
            {
                if (hasHandle > -1)
                {
                    var fileconfig = (item.Tag as FileConfig);
                    if (fileconfig.state != -1)
                    {

                    }
                    if (item.Index <= hasHandle)
                    {
                        hasHandle--;
                    }
                }
                listView2.Items.Remove(item);
            }
        }

        private void 删除inputfileStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                if (hasHandle > -1)
                {
                    var fileconfig = (item.Tag as FileConfig);
                    if (fileconfig.state != -1)
                    {
                        hasHandle--;
                    }
                }
                listView1.Items.Remove(item);
            }
        }

        int hasHandle = -1;
        object handledlock = new object();
        private void button3_Click(object sender, EventArgs e)
        {
            开始转码();
        }

        private void 开始转码()
        {
            int threadcount = Convert.ToInt32(txtTaskCount.Text);
            Task.Factory.StartNew(delegate ()
            {
                List<Task> tasks = new List<Task>();
                for (int i = 0; i < threadcount; i++)
                {
                    var t = Task.Factory.StartNew(delegate ()
                    {
                        StartOneThread();
                    });
                    tasks.Add(t);
                }
            });
        }
        private void StartOneThread()
        {
            int isHandling = -1;
            while (true)
            {
                int thisstate = -1;
                ListViewItem item = null;
                FileConfig fileConfig = null;
                try
                {

                    lock (handledlock)
                    {
                        if (isHandling >= listView2.Items.Count - 1)
                        {
                            return;
                        }
                        hasHandle++;
                        if (hasHandle > listView2.Items.Count - 1)
                        {
                            return;
                        }
                        isHandling = hasHandle;

                        this.Invoke((Action)delegate ()
                        {
                            item = listView2.Items[isHandling];
                            fileConfig = item.Tag as FileConfig;
                            if (fileConfig.state != -1)
                            {
                                thisstate = -10;
                            }
                            item.SubItems["States"].Text = "视频转码中";

                            fileConfig.state++;

                        });
                        if (thisstate == -10)
                        {
                            continue;
                        }

                        this.Invoke((Action)delegate ()
                         {
                             if (fileConfig.InputType == InputType.AvisynthScriptFile || fileConfig.InputType == InputType.AvisynthScript)
                             {
                                 EncoderTaskInfoForm form = new EncoderTaskInfoForm();
                                 form.fileConfig = fileConfig;
                                 form.lbFile.Text = fileConfig.FullName;
                                 form.Text = fileConfig.FullName;
                                 fileConfig.EncoderTaskInfo.infoForm = form;
                                 form.Show();
                             }
                         });


                    }


                    string outputfile = "";
                    string copyto = string.Empty;
                    string ralative = string.Empty;
                    //仅输出视频部分
                    if (!Directory.Exists(fileConfig.OutputPath))
                    {
                        Directory.CreateDirectory(fileConfig.OutputPath);
                    }

                    if (fileConfig.KeepDirection)
                    {//保持目录树结构
                        ralative = FileUtility.MakeRelativePath(fileConfig.DirPath + "/", Path.GetDirectoryName(fileConfig.FullName));
                        string outpath = Path.Combine(fileConfig.OutputPath, ralative);

                        if (!Directory.Exists(outpath))
                        {
                            Directory.CreateDirectory(outpath);
                        }

                        outputfile = Path.Combine(outpath, Path.GetFileNameWithoutExtension(fileConfig.FullName));
                    }
                    else if (fileConfig.OutputPath != "")
                    {//有输出目录
                        outputfile = Path.Combine(fileConfig.OutputPath, Path.GetFileNameWithoutExtension(fileConfig.FullName));
                    }
                    else
                    {//输出原路径
                        outputfile = Path.Combine(Path.GetDirectoryName(fileConfig.FullName), Path.GetFileNameWithoutExtension(fileConfig.FullName));

                    }
                    if (fileConfig.CompleteDo && !string.IsNullOrEmpty(fileConfig.CompleteActionDir))
                    {
                        if (fileConfig.KeepDirection)
                        {//保持目录树结构 
                            copyto = Path.Combine(fileConfig.CompleteActionDir, ralative, Path.GetFileNameWithoutExtension(fileConfig.FullName));
                        }
                        else
                        {
                            copyto = Path.Combine(fileConfig.CompleteActionDir, Path.GetFileNameWithoutExtension(fileConfig.FullName));
                        }
                    }
                    else
                    {
                        fileConfig.CompleteDo = false;
                    }


                    fileConfig.OutputFile = outputfile;


                    VedioConfig vedioconfig = fileConfig.VedioConfig;
                    string vedioOutputFile = string.Empty;

                    //vedioOutputFile = "D:\\temp\\" + Path.GetFileNameWithoutExtension(fileConfig.VedioFileFullName) + ".h265";

                    try
                    {
                        if (vedioconfig.Encoder == Encoder.x264)
                        {
                            if (fileConfig.InputType == InputType.Vedio)
                            {
                                fileConfig.mediaInfo = new MediaInfo(fileConfig.FullName);
                                vedioOutputFile = X264Command.RunX264Command(fileConfig);
                            }
                            else if (fileConfig.InputType == InputType.AvisynthScriptFile)
                            {
                                vedioOutputFile = X264Command.RunAvsx264mod(fileConfig);
                            }
                            else if (fileConfig.InputType == InputType.AvisynthScript)
                            {
                                fileConfig.mediaInfo = new MediaInfo(fileConfig.FullName);
                                string avsfilename = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetRandomFileName(), ".avs"));
                                File.WriteAllText(avsfilename, vedioconfig.AvsScript, System.Text.Encoding.Default);
                                fileConfig.AvsFileFullName = avsfilename;
                                fileConfig.InputType = InputType.AvisynthScriptFile;
                                vedioOutputFile = X264Command.RunAvsx264mod(fileConfig);
                            }
                            else if (fileConfig.InputType == InputType.VapoursynthScriptFile)
                            {
                                vedioOutputFile = X264Command.RunVSx265(fileConfig);
                            }
                            else if (fileConfig.InputType == InputType.VapoursynthScript)
                            {
                                fileConfig.mediaInfo = new MediaInfo(fileConfig.FullName);
                                string avsfilename = Path.Combine(Config.Temp, Path.ChangeExtension(Path.GetRandomFileName(), ".vpy"));
                                File.WriteAllText(avsfilename, vedioconfig.VapoursynthScript, System.Text.Encoding.UTF8);
                                fileConfig.VapoursynthFileFullName = avsfilename;
                                fileConfig.InputType = InputType.VapoursynthScriptFile;
                                vedioOutputFile = X264Command.RunVSx265(fileConfig);
                            }
                        }
                        else if (vedioconfig.Encoder == Encoder.x265)
                        {
                            if (fileConfig.InputType == InputType.Vedio)
                            {
                                fileConfig.mediaInfo = new MediaInfo(fileConfig.FullName);
                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.defaultStr)
                                {
                                    fileConfig.VedioConfig.decoderMode = DecoderMode.pipe;
                                }
                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.pipe)
                                {
                                    vedioOutputFile = X265Command.ffmpegPipeX265(fileConfig);
                                }
                                else
                                {
                                    vedioOutputFile = X265Command.RunX265Command(fileConfig);
                                }

                            }
                            else if (fileConfig.InputType == InputType.AvisynthScriptFile)
                            {
                                vedioOutputFile = X265Command.RunAvsx264mod(fileConfig);
                            }
                            else if (fileConfig.InputType == InputType.AvisynthScript)
                            {
                                fileConfig.mediaInfo = new MediaInfo(fileConfig.FullName);
                                string avsfilename = Path.Combine(Config.Temp, Path.ChangeExtension(Path.GetRandomFileName(), ".avs"));
                                File.WriteAllText(avsfilename, vedioconfig.AvsScript, System.Text.Encoding.Default);
                                fileConfig.AvsFileFullName = avsfilename;
                                fileConfig.InputType = InputType.AvisynthScriptFile;
                                vedioOutputFile = X265Command.RunAvsx264mod(fileConfig);
                            }
                            else if (fileConfig.InputType == InputType.VapoursynthScriptFile)
                            {
                                vedioOutputFile = X265Command.RunVSx265(fileConfig);
                            }
                            else if (fileConfig.InputType == InputType.VapoursynthScript)
                            {
                                fileConfig.mediaInfo = new MediaInfo(fileConfig.FullName);
                                string avsfilename = Path.Combine(Config.Temp, Path.ChangeExtension(Path.GetRandomFileName(), ".vpy"));
                                File.WriteAllText(avsfilename, vedioconfig.VapoursynthScript, System.Text.Encoding.UTF8);
                                fileConfig.VapoursynthFileFullName = avsfilename;
                                fileConfig.InputType = InputType.VapoursynthScriptFile;
                                vedioOutputFile = X265Command.RunVSx265(fileConfig);
                            }
                        }
                        else if (vedioconfig.Encoder == Encoder.NvEnc_H265 || vedioconfig.Encoder == Encoder.NvEnc_H264)
                        {
                            if (fileConfig.InputType == InputType.Vedio)
                            {
                                fileConfig.mediaInfo = new MediaInfo(fileConfig.FullName);
                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.defaultStr)
                                {
                                    fileConfig.VedioConfig.decoderMode = DecoderMode.self;
                                }

                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.self)
                                {
                                    vedioOutputFile = NvEncCommand.NvEncSelf(fileConfig);
                                }
                                else
                                {
                                    vedioOutputFile = NvEncCommand.ffmpegPipeNvEnc(fileConfig);
                                }

                            }
                            else if (fileConfig.InputType == InputType.VapoursynthScript)
                            {
                                fileConfig.mediaInfo = new MediaInfo(fileConfig.FullName);
                                string avsfilename = Path.Combine(Config.Temp, Path.ChangeExtension(Path.GetRandomFileName(), ".vpy"));
                                File.WriteAllText(avsfilename, vedioconfig.VapoursynthScript, System.Text.Encoding.UTF8);
                                fileConfig.VapoursynthFileFullName = avsfilename;
                                fileConfig.InputType = InputType.VapoursynthScriptFile;
                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.defaultStr)
                                {
                                    fileConfig.VedioConfig.decoderMode = DecoderMode.self;
                                }
                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.self)
                                {
                                    vedioOutputFile = NvEncCommand.NvEncUseVs(fileConfig);
                                }
                                else
                                {
                                    vedioOutputFile = NvEncCommand.VspipeNvEnc(fileConfig);
                                }

                            }
                            else if (fileConfig.InputType == InputType.AvisynthScript)
                            {
                                fileConfig.mediaInfo = new MediaInfo(fileConfig.FullName);
                                string avsfilename = Path.Combine(Config.Temp, Path.ChangeExtension(Path.GetRandomFileName(), ".avs"));
                                File.WriteAllText(avsfilename, vedioconfig.AvsScript, System.Text.Encoding.Default);
                                fileConfig.AvsFileFullName = avsfilename;
                                fileConfig.InputType = InputType.AvisynthScriptFile;
                                vedioOutputFile = NvEncCommand.NvEncUseVs(fileConfig);
                            }
                            else if (fileConfig.InputType == InputType.VapoursynthScriptFile)
                            {
                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.defaultStr)
                                    fileConfig.VedioConfig.decoderMode = DecoderMode.self;

                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.self)
                                {
                                    vedioOutputFile = NvEncCommand.NvEncUseVs(fileConfig);
                                }
                                else
                                {
                                    vedioOutputFile = NvEncCommand.VspipeNvEnc(fileConfig);
                                }
                            }
                            else if (fileConfig.InputType == InputType.AvisynthScriptFile)
                            {
                                vedioOutputFile = NvEncCommand.NvEncUseVs(fileConfig);
                            }

                        }
                        else if (vedioconfig.Encoder == Encoder.QSVEnc_H265 || vedioconfig.Encoder == Encoder.QSVEnc_H264)
                        {
                            if (fileConfig.InputType == InputType.Vedio)
                            {
                                fileConfig.mediaInfo = new MediaInfo(fileConfig.FullName);
                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.defaultStr)
                                    fileConfig.VedioConfig.decoderMode = DecoderMode.self;

                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.self)
                                {
                                    vedioOutputFile = QSVEncCommand.QSVEncSelf(fileConfig);
                                }
                                else
                                {
                                    vedioOutputFile = QSVEncCommand.ffmpegPipeQSVEnc(fileConfig);
                                }

                            }
                            else if (fileConfig.InputType == InputType.VapoursynthScript)
                            {
                                fileConfig.mediaInfo = new MediaInfo(fileConfig.FullName);
                                string avsfilename = Path.Combine(Config.Temp, Path.ChangeExtension(Path.GetRandomFileName(), ".vpy"));
                                File.WriteAllText(avsfilename, vedioconfig.VapoursynthScript, System.Text.Encoding.UTF8);
                                fileConfig.VapoursynthFileFullName = avsfilename;
                                fileConfig.InputType = InputType.VapoursynthScriptFile;
                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.defaultStr)
                                    fileConfig.VedioConfig.decoderMode = DecoderMode.self;

                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.defaultStr || fileConfig.VedioConfig.decoderMode == DecoderMode.self)
                                {
                                    vedioOutputFile = QSVEncCommand.QSVEncUseVs(fileConfig);
                                }
                                else
                                {
                                    vedioOutputFile = QSVEncCommand.VspipeQSVEnc(fileConfig);
                                }

                            }
                            else if (fileConfig.InputType == InputType.AvisynthScript)
                            {
                                fileConfig.mediaInfo = new MediaInfo(fileConfig.FullName);
                                string avsfilename = Path.Combine(Config.Temp, Path.ChangeExtension(Path.GetRandomFileName(), ".avs"));
                                File.WriteAllText(avsfilename, vedioconfig.AvsScript, System.Text.Encoding.Default);
                                fileConfig.AvsFileFullName = avsfilename;
                                fileConfig.InputType = InputType.AvisynthScriptFile;
                                vedioOutputFile = QSVEncCommand.QSVEncUseVs(fileConfig);
                            }
                            else if (fileConfig.InputType == InputType.VapoursynthScriptFile)
                            {
                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.defaultStr)
                                    fileConfig.VedioConfig.decoderMode = DecoderMode.self;

                                if (fileConfig.VedioConfig.decoderMode == DecoderMode.defaultStr || fileConfig.VedioConfig.decoderMode == DecoderMode.self)
                                {
                                    vedioOutputFile = QSVEncCommand.QSVEncUseVs(fileConfig);
                                }
                                else
                                {
                                    vedioOutputFile = QSVEncCommand.VspipeQSVEnc(fileConfig);
                                }
                            }
                            else if (fileConfig.InputType == InputType.AvisynthScriptFile)
                            {
                                vedioOutputFile = QSVEncCommand.QSVEncUseVs(fileConfig);
                            }

                        }
                    }
                    catch (EncoderException e)
                    {
                        this.Invoke((Action)delegate ()
                        {
                            item.SubItems["States"].Text = e.Message;
                        });
                        fileConfig.state = -10;
                        continue;
                    }

                    if (!File.Exists(vedioOutputFile))
                    {
                        this.Invoke((Action)delegate ()
                        {
                            item.SubItems["States"].Text = "视频编码失败";
                        });
                        fileConfig.state = -10;
                        continue;
                    }

                    if (fileConfig.AudioConfig.Enabled && fileConfig.state != -10)
                    {
                        //if (fileConfig.InputType == InputType.Vedio && fileConfig.AudioConfig.CopyStream && fileConfig.VedioConfig.Encoder == Encoder.x264)
                        //{
                        //    //直接由x264处理掉
                        //}
                        //else
                        {
                            if (isHandling >= listView2.Items.Count)
                            {
                                return;
                            }
                            this.Invoke((Action)delegate ()
                            {
                                item = listView2.Items[isHandling];
                                item.SubItems["States"].Text = "音频转码中";
                            });

                            string audiofile = string.Empty;
                            if (!fileConfig.AudioConfig.Enabled)
                            {

                            }
                            else
                            if (fileConfig.AudioConfig.CopyStream)
                            {
                                audiofile = CommandHelper.DemuxAudio(fileConfig);
                            }
                            else
                            {
                                if (fileConfig.AudioConfig.UseEac3to)
                                {
                                    if (fileConfig.AudioConfig.Encoder == AudioEncoder.aac)
                                    {
                                        audiofile = Eac3toCommand.ConvertMusic(fileConfig);
                                    }
                                    else if (fileConfig.AudioConfig.Encoder == AudioEncoder.opus)
                                    {
                                        audiofile = Eac3toCommand.ConvertAudioTOpus(fileConfig);
                                    }
                                    else if (fileConfig.AudioConfig.Encoder == AudioEncoder.flac)
                                    {
                                        audiofile = Eac3toCommand.ConvertAudioToFlac(fileConfig);
                                    }

                                }
                                else
                                {
                                    if (fileConfig.AudioConfig.Encoder == AudioEncoder.aac)
                                    {
                                        audiofile = CommandHelper.RunFFmpegToAAC(fileConfig);
                                    }
                                    else if (fileConfig.AudioConfig.Encoder == AudioEncoder.opus)
                                    {
                                        audiofile = CommandHelper.RunFFmpegToOpus(fileConfig);
                                    }
                                    else if (fileConfig.AudioConfig.Encoder == AudioEncoder.flac)
                                    {
                                        audiofile = FFmpegCommand.RunFFmpegToFlac(fileConfig);
                                    }
                                }
                            }

                            if (isHandling >= listView2.Items.Count)
                            {
                                return;
                            }
                            this.Invoke((Action)delegate ()
                            {
                                item.SubItems["States"].Text = "封装中";
                            });
                            int delay = 0;
                            if (fileConfig.mediaInfo != null)
                            {
                                delay = fileConfig.mediaInfo.DelayRelativeToVideo;
                                //delay = delay - 67;
                            }
                            if (fileConfig.Muxer == "mkv")
                            {
                                vedioOutputFile = CommandHelper.MKVmergin(fileConfig, vedioOutputFile, audiofile, delay);
                            }
                            else if (fileConfig.Muxer == "mp4")
                            {
                                vedioOutputFile = CommandHelper.mp4box(fileConfig, vedioOutputFile, audiofile, delay);
                            }
                            else if (fileConfig.Muxer == "flv")
                            {
                                vedioOutputFile = CommandHelper.ffmpegmux(fileConfig, vedioOutputFile, audiofile, fileConfig.Muxer);
                                if (fileConfig.sinablack)
                                {
                                    FlvMain flvbugger = new FlvMain();
                                    flvbugger.addFile(vedioOutputFile);
                                    flvbugger.ExecuteBlack(999d, -1, Path.ChangeExtension(vedioOutputFile, ".black.flv"));
                                }
                                else if (fileConfig.sinaPreblack)
                                {
                                    FlvMain flvbugger = new FlvMain();
                                    flvbugger.addFile(vedioOutputFile);
                                    flvbugger.ExecuteTime(999d, -1, Path.ChangeExtension(vedioOutputFile, ".speed.flv"));
                                }


                            }
                        }

                    }

                    if (fileConfig.CompleteDo && fileConfig.state != -10)
                    {
                        try
                        {
                            copyto = copyto + Path.GetExtension(vedioOutputFile);
                            copyto = FileUtility.GetNoSameNameFile(copyto);
                            if (fileConfig.CompleteAction == "拷贝到")
                            {
                                this.Invoke((Action)delegate ()
                                {
                                    item.SubItems["States"].Text = "拷贝中";
                                });

                                File.Copy(vedioOutputFile, copyto, true);

                            }
                            else if (fileConfig.CompleteAction == "剪切到")
                            {
                                this.Invoke((Action)delegate ()
                                {
                                    item.SubItems["States"].Text = "剪切中";
                                });

                                File.Move(vedioOutputFile, copyto);
                            }
                        }
                        catch { }
                    }

                    this.Invoke((Action)delegate ()
                    {
                        if (fileConfig.state == -10)
                        {
                            item.SubItems["States"].Text = "失败";
                        }
                        else
                        {
                            item.SubItems["States"].Text = "完成";
                        }

                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((Action)delegate ()
                    {
                        item.SubItems["States"].Text = "失败：" + ex.Message;
                    });
                }

            }

        }

        private void 退出RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOneclickStart_Click(object sender, EventArgs e)
        {
            添加到任务列表();
            tabControl1.SelectedIndex = 3;
            开始转码();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "视频文件|*.mp4;*.mkv;*.avi;*.ts;*.tp;*.ts;*.tp;*.m2ts|所有文件|*";
            var result = ofd.ShowDialog();


            if (result == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string fullname in ofd.FileNames)
                {
                    AddFileToListSkipExtgensionCheck(fullname, Path.GetDirectoryName(fullname));
                }
            }

        }

        private void cbVedioConfigTemplete_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVedioConfigTemplete.Text == "网络视频")
            {
                cbColorDepth.Text = "8";
                cbcsp.SelectedIndex = 0;
                txtUserArgs.Text = txtUserArgs.Text.Replace("--input-depth 10", "");
                if (comboBox2.SelectedItem.ToString().Equals(Encoder.x265.ToString()))
                    cbMuxer.Text = "mkv";
                else
                    txtUserArgs.Text = Resource1.TempleteOnline;

                txtbitrate.Text = "3510";
                textBox3.Text = "";
                cbpreset.Text = "fast";
                cbMuxer.Text = "mp4";
                cbUseAvsTemplete.Checked = true;
            }
            else if (cbVedioConfigTemplete.Text == "高清视频")
            {
                cbColorDepth.Text = "8";
                cbcsp.SelectedIndex = 0;
                if (comboBox2.SelectedItem.ToString().Equals(Encoder.x265.ToString()))
                {
                    cbpreset.Text = "slow";
                    cbMuxer.Text = "mkv";
                    txtUserArgs.Text = "  --no-sao --strong-intra-smoothing  --no-open-gop --no-rect --no-amp --ctu 32  --qpmax 28 --weightb --limit-tu=4 --aq-mode 3 --aq-strength 0.8 --min-keyint 1 --merange 44  --keyint 600 --colorprim bt709  --qcomp 0.7 --range limited --pools 16 --vbv-bufsize 27000 --vbv-maxrate 27000 --psy-rd 3 --psy-rdoq 2 --bframes 6 --rdoq-level 2 --weightb --rd 4";
                }
                else
                {
                    cbpreset.Text = "slow";
                    txtUserArgs.Text = Resource1.TempleteHDi420;

                    cbMuxer.Text = "mp4";
                }
                textBox3.Text = "21";

                txtbitrate.Text = "";
                cbUseAvsTemplete.Checked = false;
            }
            else if (cbVedioConfigTemplete.Text == "高保真游戏视频")
            {
                cbColorDepth.Text = "10";
                cbcsp.SelectedIndex = 2;
                txtUserArgs.AppendText(" --input-depth 10");
                if (comboBox2.SelectedItem.ToString().Equals(Encoder.x265.ToString()))
                {
                    cbpreset.Text = "medium";
                    cbMuxer.Text = "mkv";
                }
                else
                {


                    cbpreset.Text = "slow";
                    txtUserArgs.Text = Resource1.TempleteGamei444;

                    cbMuxer.Text = "mp4";
                }
                textBox3.Text = "24";

                cbUseAvsTemplete.Checked = false;
            }
            else if (cbVedioConfigTemplete.Text == "爱情动作")
            {
                if (comboBox2.SelectedItem.ToString().Equals(Encoder.x265.ToString()))
                {
                    textBox3.Text = "26";
                    cbColorDepth.Text = "10";
                    cbpreset.Text = "medium";
                    txtUserArgs.Text = "  --no-open-gop --no-rect --weightb --aq-mode 3 --merange 40 --min-keyint 1 --qpmax 33 --keyint 1800 --colorprim bt709  --qcomp 0.65 --range limited --pools 16 --vbv-bufsize 12000 --vbv-maxrate 12000 --psy-rd 1.4";
                    cbMuxer.Text = "mkv";
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FileConfig fileConfig = new FileConfig();
            fileConfig.AvsFileFullName = fileConfig.FullName = txtAvsFile.Text;
            fileConfig.DirPath = Path.GetDirectoryName(txtAvsFile.Text);
            if (Path.GetExtension(txtAvsFile.Text).Equals(".avs", StringComparison.OrdinalIgnoreCase))
            {
                fileConfig.InputType = InputType.AvisynthScriptFile;
            }
            fileConfig.AudioInputFile = txtAudioInput.Text;
            FillFileConfig(fileConfig);
        }

        private void cbEnableQpmod_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = cbEnableQpmod.Checked ? "恒定量化" : "恒定质量";
        }

        private void cbEnableX265_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbEnableX265.Checked)
            //    cbUseAvsTemplete.Checked = true;
        }


        private void cbAvsTemplate_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbAvsTemplate.Text))
            {
                var path = "Template\\avs\\" + cbAvsTemplate.Text;
                if (File.Exists(path))
                {
                    var text = File.ReadAllText(path);
                    txtAvsScript.Text = text;
                }
            }
        }

        private void combVSTemplate_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(combVSTemplate.Text))
            {
                var path = "Template\\vs\\" + combVSTemplate.Text;
                if (File.Exists(path))
                {
                    var text = File.ReadAllText(path);
                    txtVsScript.Text = text;
                }
            }
        }

        private void cbColorDepth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbColorDepth.Text == "10")
            {
                txtUserArgs.AppendText(" --input-depth 10");
            }
            else
            {
                txtUserArgs.Text = txtUserArgs.Text.Replace("--input-depth 10", "");
            }

        }

        private void cbUseEac3to_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUseEac3to.Checked)
            {
                txtAudioLine.Text = "+4dB -mixlfe ";
            }
            else
            {
                txtAudioLine.Text = "-af volume=+4dB ";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString().Equals(Encoder.NvEnc_H265.ToString()))
            {
                txtUserArgs.Text = "--max-bitrate 27000 --gop-len 1600 --lookahead 20 --weightp --aq --aq-temporal --ref 5  -b 3";
            }
        }
    }
}
