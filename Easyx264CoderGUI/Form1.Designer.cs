﻿namespace Easyx264CoderGUI
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.AddTask = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.cbMuxer = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbVedioConfigTemplete = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.btnOneclickStart = new System.Windows.Forms.Button();
            this.gbVedio = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.cbEnableQpmod = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtbitrate = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbcsp = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUserArgs = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbpreset = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbColorDepth = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAudioEncoder = new System.Windows.Forms.ComboBox();
            this.txtAudioLine = new System.Windows.Forms.TextBox();
            this.txtAudioTracker = new System.Windows.Forms.TextBox();
            this.cbUseEac3to = new System.Windows.Forms.CheckBox();
            this.cbcopuaudio = new System.Windows.Forms.CheckBox();
            this.cbUseAudio = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuality = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cbKeepFileTree = new System.Windows.Forms.CheckBox();
            this.cblCompeteAction = new System.Windows.Forms.ComboBox();
            this.cbCompleteDo = new System.Windows.Forms.CheckBox();
            this.btnClearList = new System.Windows.Forms.Button();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnAddtoTasklist = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FullName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.inputfileMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAvs = new System.Windows.Forms.TabPage();
            this.cbloadSub = new System.Windows.Forms.CheckBox();
            this.cbAvsTemplate = new System.Windows.Forms.ComboBox();
            this.txtAvsScript = new System.Windows.Forms.TextBox();
            this.cbUseAvsTemplete = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtAudioInput = new System.Windows.Forms.TextBox();
            this.txtAvsFile = new System.Windows.Forms.TextBox();
            this.tabVS = new System.Windows.Forms.TabPage();
            this.combVSTemplate = new System.Windows.Forms.ComboBox();
            this.txtVsScript = new System.Windows.Forms.TextBox();
            this.cbUseVSTemplete = new System.Windows.Forms.CheckBox();
            this.TaskList = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTaskCount = new System.Windows.Forms.TextBox();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除此任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.MyFolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label18 = new System.Windows.Forms.Label();
            this.cbdecoderMode = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.AddTask.SuspendLayout();
            this.gbVedio.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.inputfileMenuStrip.SuspendLayout();
            this.tbAvs.SuspendLayout();
            this.tabVS.SuspendLayout();
            this.TaskList.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(617, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出RToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件ToolStripMenuItem.Text = "文件(&F)";
            // 
            // 退出RToolStripMenuItem
            // 
            this.退出RToolStripMenuItem.Name = "退出RToolStripMenuItem";
            this.退出RToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.退出RToolStripMenuItem.Text = "退出(&X)";
            this.退出RToolStripMenuItem.Click += new System.EventHandler(this.退出RToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 707);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(617, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Controls.Add(this.AddTask);
            this.tabControl1.Controls.Add(this.tbAvs);
            this.tabControl1.Controls.Add(this.tabVS);
            this.tabControl1.Controls.Add(this.TaskList);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(617, 682);
            this.tabControl1.TabIndex = 5;
            // 
            // AddTask
            // 
            this.AddTask.Controls.Add(this.label16);
            this.AddTask.Controls.Add(this.cbMuxer);
            this.AddTask.Controls.Add(this.label10);
            this.AddTask.Controls.Add(this.cbVedioConfigTemplete);
            this.AddTask.Controls.Add(this.button4);
            this.AddTask.Controls.Add(this.btnOneclickStart);
            this.AddTask.Controls.Add(this.gbVedio);
            this.AddTask.Controls.Add(this.groupBox1);
            this.AddTask.Controls.Add(this.button2);
            this.AddTask.Controls.Add(this.textBox2);
            this.AddTask.Controls.Add(this.cbKeepFileTree);
            this.AddTask.Controls.Add(this.cblCompeteAction);
            this.AddTask.Controls.Add(this.cbCompleteDo);
            this.AddTask.Controls.Add(this.btnClearList);
            this.AddTask.Controls.Add(this.btnOutputPath);
            this.AddTask.Controls.Add(this.textBox1);
            this.AddTask.Controls.Add(this.btnAddtoTasklist);
            this.AddTask.Controls.Add(this.listView1);
            this.AddTask.Location = new System.Drawing.Point(4, 22);
            this.AddTask.Name = "AddTask";
            this.AddTask.Padding = new System.Windows.Forms.Padding(3);
            this.AddTask.Size = new System.Drawing.Size(609, 656);
            this.AddTask.TabIndex = 1;
            this.AddTask.Text = "添加视频";
            this.AddTask.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 626);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 24;
            this.label16.Text = "封装容器：";
            // 
            // cbMuxer
            // 
            this.cbMuxer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbMuxer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMuxer.FormattingEnabled = true;
            this.cbMuxer.Items.AddRange(new object[] {
            "mkv",
            "mp4",
            "flv",
            "战渣浪后黑",
            "战渣浪前黑"});
            this.cbMuxer.Location = new System.Drawing.Point(87, 623);
            this.cbMuxer.Name = "cbMuxer";
            this.cbMuxer.Size = new System.Drawing.Size(99, 20);
            this.cbMuxer.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(155, 330);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 22;
            this.label10.Text = "视频参数模板：";
            // 
            // cbVedioConfigTemplete
            // 
            this.cbVedioConfigTemplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbVedioConfigTemplete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVedioConfigTemplete.FormattingEnabled = true;
            this.cbVedioConfigTemplete.Items.AddRange(new object[] {
            "高清视频",
            "爱情动作",
            "网络视频",
            "战渣浪",
            "高保真游戏视频"});
            this.cbVedioConfigTemplete.Location = new System.Drawing.Point(248, 326);
            this.cbVedioConfigTemplete.Name = "cbVedioConfigTemplete";
            this.cbVedioConfigTemplete.Size = new System.Drawing.Size(121, 20);
            this.cbVedioConfigTemplete.TabIndex = 21;
            this.cbVedioConfigTemplete.SelectedIndexChanged += new System.EventHandler(this.cbVedioConfigTemplete_SelectedIndexChanged);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Location = new System.Drawing.Point(392, 216);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 20;
            this.button4.Text = "添加视频";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnOneclickStart
            // 
            this.btnOneclickStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOneclickStart.Location = new System.Drawing.Point(496, 623);
            this.btnOneclickStart.Name = "btnOneclickStart";
            this.btnOneclickStart.Size = new System.Drawing.Size(75, 23);
            this.btnOneclickStart.TabIndex = 19;
            this.btnOneclickStart.Text = "一键开始";
            this.btnOneclickStart.UseVisualStyleBackColor = true;
            this.btnOneclickStart.Click += new System.EventHandler(this.btnOneclickStart_Click);
            // 
            // gbVedio
            // 
            this.gbVedio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbVedio.Controls.Add(this.label18);
            this.gbVedio.Controls.Add(this.cbdecoderMode);
            this.gbVedio.Controls.Add(this.label17);
            this.gbVedio.Controls.Add(this.comboBox2);
            this.gbVedio.Controls.Add(this.cbEnableQpmod);
            this.gbVedio.Controls.Add(this.label13);
            this.gbVedio.Controls.Add(this.txtbitrate);
            this.gbVedio.Controls.Add(this.label12);
            this.gbVedio.Controls.Add(this.cbcsp);
            this.gbVedio.Controls.Add(this.label11);
            this.gbVedio.Controls.Add(this.label9);
            this.gbVedio.Controls.Add(this.txtUserArgs);
            this.gbVedio.Controls.Add(this.textBox5);
            this.gbVedio.Controls.Add(this.textBox4);
            this.gbVedio.Controls.Add(this.checkBox1);
            this.gbVedio.Controls.Add(this.label8);
            this.gbVedio.Controls.Add(this.comboBox1);
            this.gbVedio.Controls.Add(this.label7);
            this.gbVedio.Controls.Add(this.cbpreset);
            this.gbVedio.Controls.Add(this.label6);
            this.gbVedio.Controls.Add(this.cbColorDepth);
            this.gbVedio.Controls.Add(this.label5);
            this.gbVedio.Controls.Add(this.textBox3);
            this.gbVedio.Controls.Add(this.label4);
            this.gbVedio.Location = new System.Drawing.Point(23, 352);
            this.gbVedio.Name = "gbVedio";
            this.gbVedio.Size = new System.Drawing.Size(555, 172);
            this.gbVedio.TabIndex = 18;
            this.gbVedio.TabStop = false;
            this.gbVedio.Text = "视频参数";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(25, 19);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 23;
            this.label17.Text = "编码器";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "x264",
            "x265",
            "NVEnc_H265",
            "NVEnc_H264"});
            this.comboBox2.Location = new System.Drawing.Point(82, 12);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 22;
            this.comboBox2.Text = "x265";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // cbEnableQpmod
            // 
            this.cbEnableQpmod.AutoSize = true;
            this.cbEnableQpmod.Location = new System.Drawing.Point(129, 45);
            this.cbEnableQpmod.Name = "cbEnableQpmod";
            this.cbEnableQpmod.Size = new System.Drawing.Size(15, 14);
            this.cbEnableQpmod.TabIndex = 21;
            this.toolTip1.SetToolTip(this.cbEnableQpmod, "使用恒定量化");
            this.cbEnableQpmod.UseVisualStyleBackColor = true;
            this.cbEnableQpmod.CheckedChanged += new System.EventHandler(this.cbEnableQpmod_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(151, 79);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 12);
            this.label13.TabIndex = 20;
            this.label13.Text = "Kb";
            // 
            // txtbitrate
            // 
            this.txtbitrate.Location = new System.Drawing.Point(82, 74);
            this.txtbitrate.Name = "txtbitrate";
            this.txtbitrate.Size = new System.Drawing.Size(63, 21);
            this.txtbitrate.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 79);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 18;
            this.label12.Text = "二次编码";
            // 
            // cbcsp
            // 
            this.cbcsp.FormattingEnabled = true;
            this.cbcsp.Items.AddRange(new object[] {
            "i420",
            "i422",
            "i444",
            "rgb"});
            this.cbcsp.Location = new System.Drawing.Point(346, 43);
            this.cbcsp.Name = "cbcsp";
            this.cbcsp.Size = new System.Drawing.Size(51, 20);
            this.cbcsp.TabIndex = 17;
            this.cbcsp.Text = "i420";
            this.toolTip1.SetToolTip(this.cbcsp, "RGB24的视频这里要设置成i444才是高保真色彩");
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(288, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = "色彩空间：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "自定义：";
            // 
            // txtUserArgs
            // 
            this.txtUserArgs.Location = new System.Drawing.Point(70, 107);
            this.txtUserArgs.Multiline = true;
            this.txtUserArgs.Name = "txtUserArgs";
            this.txtUserArgs.Size = new System.Drawing.Size(448, 56);
            this.txtUserArgs.TabIndex = 12;
            this.txtUserArgs.Text = "--input-range pc --range pc ";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(482, 77);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(51, 21);
            this.textBox5.TabIndex = 11;
            this.textBox5.Text = "720";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(420, 77);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(56, 21);
            this.textBox4.TabIndex = 10;
            this.textBox4.Text = "1280";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(330, 79);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "调整分辨率";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(203, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "调校";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "",
            "film",
            "grain",
            "animation",
            "lp",
            "lp++"});
            this.comboBox1.Location = new System.Drawing.Point(246, 76);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(56, 20);
            this.comboBox1.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(436, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "预设";
            // 
            // cbpreset
            // 
            this.cbpreset.FormattingEnabled = true;
            this.cbpreset.Items.AddRange(new object[] {
            "veryfast",
            "faster",
            "fast",
            "medium",
            "slow",
            "slower",
            "veryslow",
            "placebo"});
            this.cbpreset.Location = new System.Drawing.Point(471, 46);
            this.cbpreset.Name = "cbpreset";
            this.cbpreset.Size = new System.Drawing.Size(71, 20);
            this.cbpreset.TabIndex = 3;
            this.cbpreset.Text = "medium";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(258, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "bit";
            // 
            // cbColorDepth
            // 
            this.cbColorDepth.FormattingEnabled = true;
            this.cbColorDepth.Items.AddRange(new object[] {
            "8",
            "10"});
            this.cbColorDepth.Location = new System.Drawing.Point(212, 44);
            this.cbColorDepth.Name = "cbColorDepth";
            this.cbColorDepth.Size = new System.Drawing.Size(40, 20);
            this.cbColorDepth.TabIndex = 3;
            this.cbColorDepth.Text = "8";
            this.toolTip1.SetToolTip(this.cbColorDepth, "8bit兼容性好，10bit压缩率好");
            this.cbColorDepth.SelectedIndexChanged += new System.EventHandler(this.cbColorDepth_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "色深";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(82, 44);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(41, 21);
            this.textBox3.TabIndex = 1;
            this.textBox3.Text = "21";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "恒定质量";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.cbAudioEncoder);
            this.groupBox1.Controls.Add(this.txtAudioLine);
            this.groupBox1.Controls.Add(this.txtAudioTracker);
            this.groupBox1.Controls.Add(this.cbUseEac3to);
            this.groupBox1.Controls.Add(this.cbcopuaudio);
            this.groupBox1.Controls.Add(this.cbUseAudio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtQuality);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(24, 530);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(554, 75);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "音频参数";
            // 
            // cbAudioEncoder
            // 
            this.cbAudioEncoder.FormattingEnabled = true;
            this.cbAudioEncoder.Items.AddRange(new object[] {
            "Opus",
            "AAC",
            "FLAC"});
            this.cbAudioEncoder.Location = new System.Drawing.Point(24, 43);
            this.cbAudioEncoder.Name = "cbAudioEncoder";
            this.cbAudioEncoder.Size = new System.Drawing.Size(55, 20);
            this.cbAudioEncoder.TabIndex = 17;
            this.cbAudioEncoder.Text = "Opus";
            // 
            // txtAudioLine
            // 
            this.txtAudioLine.Location = new System.Drawing.Point(368, 43);
            this.txtAudioLine.Name = "txtAudioLine";
            this.txtAudioLine.Size = new System.Drawing.Size(151, 21);
            this.txtAudioLine.TabIndex = 16;
            this.txtAudioLine.Text = "-af volume=+4dB";
            // 
            // txtAudioTracker
            // 
            this.txtAudioTracker.Location = new System.Drawing.Point(430, 18);
            this.txtAudioTracker.Margin = new System.Windows.Forms.Padding(2);
            this.txtAudioTracker.Name = "txtAudioTracker";
            this.txtAudioTracker.Size = new System.Drawing.Size(19, 21);
            this.txtAudioTracker.TabIndex = 14;
            this.txtAudioTracker.Text = "2";
            // 
            // cbUseEac3to
            // 
            this.cbUseEac3to.AutoSize = true;
            this.cbUseEac3to.Location = new System.Drawing.Point(368, 20);
            this.cbUseEac3to.Margin = new System.Windows.Forms.Padding(2);
            this.cbUseEac3to.Name = "cbUseEac3to";
            this.cbUseEac3to.Size = new System.Drawing.Size(60, 16);
            this.cbUseEac3to.TabIndex = 13;
            this.cbUseEac3to.Text = "Eac3to";
            this.cbUseEac3to.UseVisualStyleBackColor = true;
            this.cbUseEac3to.CheckedChanged += new System.EventHandler(this.cbUseEac3to_CheckedChanged);
            // 
            // cbcopuaudio
            // 
            this.cbcopuaudio.AutoSize = true;
            this.cbcopuaudio.Location = new System.Drawing.Point(285, 21);
            this.cbcopuaudio.Name = "cbcopuaudio";
            this.cbcopuaudio.Size = new System.Drawing.Size(84, 16);
            this.cbcopuaudio.TabIndex = 12;
            this.cbcopuaudio.Text = "复制音频流";
            this.cbcopuaudio.UseVisualStyleBackColor = true;
            // 
            // cbUseAudio
            // 
            this.cbUseAudio.AutoSize = true;
            this.cbUseAudio.Checked = true;
            this.cbUseAudio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseAudio.Location = new System.Drawing.Point(7, 21);
            this.cbUseAudio.Name = "cbUseAudio";
            this.cbUseAudio.Size = new System.Drawing.Size(72, 16);
            this.cbUseAudio.TabIndex = 11;
            this.cbUseAudio.Text = "启用音频";
            this.cbUseAudio.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "质量：";
            // 
            // txtQuality
            // 
            this.txtQuality.Location = new System.Drawing.Point(138, 19);
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.Size = new System.Drawing.Size(40, 21);
            this.txtQuality.TabIndex = 5;
            this.txtQuality.Text = "0.7";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "(0~1,0.65 250K)";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(486, 284);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "浏览";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.AllowDrop = true;
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox2.Location = new System.Drawing.Point(206, 285);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(239, 21);
            this.textBox2.TabIndex = 15;
            this.toolTip1.SetToolTip(this.textBox2, "完成后拷贝到360云盘目录就自动上传了呗");
            // 
            // cbKeepFileTree
            // 
            this.cbKeepFileTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbKeepFileTree.AutoSize = true;
            this.cbKeepFileTree.Location = new System.Drawing.Point(54, 311);
            this.cbKeepFileTree.Name = "cbKeepFileTree";
            this.cbKeepFileTree.Size = new System.Drawing.Size(96, 16);
            this.cbKeepFileTree.TabIndex = 7;
            this.cbKeepFileTree.Text = "保持目录结构";
            this.cbKeepFileTree.UseVisualStyleBackColor = true;
            // 
            // cblCompeteAction
            // 
            this.cblCompeteAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cblCompeteAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cblCompeteAction.FormattingEnabled = true;
            this.cblCompeteAction.Items.AddRange(new object[] {
            "拷贝到",
            "剪切到"});
            this.cblCompeteAction.Location = new System.Drawing.Point(120, 285);
            this.cblCompeteAction.Name = "cblCompeteAction";
            this.cblCompeteAction.Size = new System.Drawing.Size(71, 20);
            this.cblCompeteAction.TabIndex = 14;
            // 
            // cbCompleteDo
            // 
            this.cbCompleteDo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbCompleteDo.AutoSize = true;
            this.cbCompleteDo.Location = new System.Drawing.Point(54, 287);
            this.cbCompleteDo.Name = "cbCompleteDo";
            this.cbCompleteDo.Size = new System.Drawing.Size(60, 16);
            this.cbCompleteDo.TabIndex = 13;
            this.cbCompleteDo.Text = "完成后";
            this.cbCompleteDo.UseVisualStyleBackColor = true;
            // 
            // btnClearList
            // 
            this.btnClearList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearList.Location = new System.Drawing.Point(486, 216);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(75, 23);
            this.btnClearList.TabIndex = 11;
            this.btnClearList.Text = "清空列表";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click_1);
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOutputPath.Location = new System.Drawing.Point(486, 246);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(75, 23);
            this.btnOutputPath.TabIndex = 3;
            this.btnOutputPath.Text = "输出";
            this.btnOutputPath.UseVisualStyleBackColor = true;
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputPath_Click);
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(26, 248);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(425, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // btnAddtoTasklist
            // 
            this.btnAddtoTasklist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddtoTasklist.Location = new System.Drawing.Point(415, 623);
            this.btnAddtoTasklist.Name = "btnAddtoTasklist";
            this.btnAddtoTasklist.Size = new System.Drawing.Size(75, 23);
            this.btnAddtoTasklist.TabIndex = 1;
            this.btnAddtoTasklist.Text = "添加任务";
            this.btnAddtoTasklist.UseVisualStyleBackColor = true;
            this.btnAddtoTasklist.Click += new System.EventHandler(this.btnAddtoTasklist_Click);
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.FullName,
            this.FilePath});
            this.listView1.ContextMenuStrip = this.inputfileMenuStrip;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(8, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(595, 191);
            this.listView1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.listView1, "可以把文件拖进来哦");
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            // 
            // FullName
            // 
            this.FullName.Text = "文件名";
            this.FullName.Width = 162;
            // 
            // FilePath
            // 
            this.FilePath.Text = "路径";
            this.FilePath.Width = 268;
            // 
            // inputfileMenuStrip
            // 
            this.inputfileMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除toolStripMenuItem});
            this.inputfileMenuStrip.Name = "inputfileMenuStrip";
            this.inputfileMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // 删除toolStripMenuItem
            // 
            this.删除toolStripMenuItem.Name = "删除toolStripMenuItem";
            this.删除toolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除toolStripMenuItem.Text = "删除";
            this.删除toolStripMenuItem.Click += new System.EventHandler(this.删除inputfileStripMenuItem_Click);
            // 
            // tbAvs
            // 
            this.tbAvs.Controls.Add(this.cbloadSub);
            this.tbAvs.Controls.Add(this.cbAvsTemplate);
            this.tbAvs.Controls.Add(this.txtAvsScript);
            this.tbAvs.Controls.Add(this.cbUseAvsTemplete);
            this.tbAvs.Controls.Add(this.button5);
            this.tbAvs.Controls.Add(this.label15);
            this.tbAvs.Controls.Add(this.label14);
            this.tbAvs.Controls.Add(this.txtAudioInput);
            this.tbAvs.Controls.Add(this.txtAvsFile);
            this.tbAvs.Location = new System.Drawing.Point(4, 22);
            this.tbAvs.Name = "tbAvs";
            this.tbAvs.Size = new System.Drawing.Size(609, 656);
            this.tbAvs.TabIndex = 3;
            this.tbAvs.Text = "AVS添加";
            this.tbAvs.UseVisualStyleBackColor = true;
            // 
            // cbloadSub
            // 
            this.cbloadSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbloadSub.AutoSize = true;
            this.cbloadSub.Location = new System.Drawing.Point(10, 538);
            this.cbloadSub.Margin = new System.Windows.Forms.Padding(2);
            this.cbloadSub.Name = "cbloadSub";
            this.cbloadSub.Size = new System.Drawing.Size(96, 16);
            this.cbloadSub.TabIndex = 26;
            this.cbloadSub.Text = "自动加载字幕";
            this.cbloadSub.UseVisualStyleBackColor = true;
            // 
            // cbAvsTemplate
            // 
            this.cbAvsTemplate.FormattingEnabled = true;
            this.cbAvsTemplate.Location = new System.Drawing.Point(25, 186);
            this.cbAvsTemplate.Name = "cbAvsTemplate";
            this.cbAvsTemplate.Size = new System.Drawing.Size(369, 20);
            this.cbAvsTemplate.TabIndex = 9;
            this.cbAvsTemplate.SelectedValueChanged += new System.EventHandler(this.cbAvsTemplate_SelectedValueChanged);
            // 
            // txtAvsScript
            // 
            this.txtAvsScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAvsScript.Location = new System.Drawing.Point(25, 214);
            this.txtAvsScript.Multiline = true;
            this.txtAvsScript.Name = "txtAvsScript";
            this.txtAvsScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAvsScript.Size = new System.Drawing.Size(534, 312);
            this.txtAvsScript.TabIndex = 6;
            // 
            // cbUseAvsTemplete
            // 
            this.cbUseAvsTemplete.AutoSize = true;
            this.cbUseAvsTemplete.Location = new System.Drawing.Point(10, 164);
            this.cbUseAvsTemplete.Name = "cbUseAvsTemplete";
            this.cbUseAvsTemplete.Size = new System.Drawing.Size(342, 16);
            this.cbUseAvsTemplete.TabIndex = 5;
            this.cbUseAvsTemplete.Text = "使用AVS模版添加任务(视频文件在第一个“添加视频”添加)";
            this.cbUseAvsTemplete.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(466, 101);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "添加任务";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 66);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 12);
            this.label15.TabIndex = 3;
            this.label15.Text = "音频输入文件：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 21);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 12);
            this.label14.TabIndex = 2;
            this.label14.Text = "AVS文件：";
            // 
            // txtAudioInput
            // 
            this.txtAudioInput.AllowDrop = true;
            this.txtAudioInput.Location = new System.Drawing.Point(95, 63);
            this.txtAudioInput.Name = "txtAudioInput";
            this.txtAudioInput.Size = new System.Drawing.Size(446, 21);
            this.txtAudioInput.TabIndex = 1;
            this.txtAudioInput.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxFile_DragDrop);
            this.txtAudioInput.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // txtAvsFile
            // 
            this.txtAvsFile.AllowDrop = true;
            this.txtAvsFile.Location = new System.Drawing.Point(95, 18);
            this.txtAvsFile.Name = "txtAvsFile";
            this.txtAvsFile.Size = new System.Drawing.Size(446, 21);
            this.txtAvsFile.TabIndex = 0;
            this.txtAvsFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxFile_DragDrop);
            this.txtAvsFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // tabVS
            // 
            this.tabVS.Controls.Add(this.combVSTemplate);
            this.tabVS.Controls.Add(this.txtVsScript);
            this.tabVS.Controls.Add(this.cbUseVSTemplete);
            this.tabVS.Location = new System.Drawing.Point(4, 22);
            this.tabVS.Margin = new System.Windows.Forms.Padding(2);
            this.tabVS.Name = "tabVS";
            this.tabVS.Padding = new System.Windows.Forms.Padding(2);
            this.tabVS.Size = new System.Drawing.Size(609, 656);
            this.tabVS.TabIndex = 4;
            this.tabVS.Text = "Vapoursynth";
            this.tabVS.UseVisualStyleBackColor = true;
            // 
            // combVSTemplate
            // 
            this.combVSTemplate.FormattingEnabled = true;
            this.combVSTemplate.Location = new System.Drawing.Point(31, 40);
            this.combVSTemplate.Name = "combVSTemplate";
            this.combVSTemplate.Size = new System.Drawing.Size(385, 20);
            this.combVSTemplate.TabIndex = 12;
            this.combVSTemplate.SelectedValueChanged += new System.EventHandler(this.combVSTemplate_SelectedValueChanged);
            // 
            // txtVsScript
            // 
            this.txtVsScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVsScript.Location = new System.Drawing.Point(31, 68);
            this.txtVsScript.Multiline = true;
            this.txtVsScript.Name = "txtVsScript";
            this.txtVsScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtVsScript.Size = new System.Drawing.Size(550, 487);
            this.txtVsScript.TabIndex = 11;
            // 
            // cbUseVSTemplete
            // 
            this.cbUseVSTemplete.AutoSize = true;
            this.cbUseVSTemplete.Location = new System.Drawing.Point(16, 18);
            this.cbUseVSTemplete.Name = "cbUseVSTemplete";
            this.cbUseVSTemplete.Size = new System.Drawing.Size(336, 16);
            this.cbUseVSTemplete.TabIndex = 10;
            this.cbUseVSTemplete.Text = "使用VS模版添加任务(视频文件在第一个“添加视频”添加)";
            this.cbUseVSTemplete.UseVisualStyleBackColor = true;
            // 
            // TaskList
            // 
            this.TaskList.Controls.Add(this.button3);
            this.TaskList.Controls.Add(this.label3);
            this.TaskList.Controls.Add(this.txtTaskCount);
            this.TaskList.Controls.Add(this.listView2);
            this.TaskList.Location = new System.Drawing.Point(4, 22);
            this.TaskList.Name = "TaskList";
            this.TaskList.Size = new System.Drawing.Size(609, 656);
            this.TaskList.TabIndex = 2;
            this.TaskList.Text = "任务列表";
            this.TaskList.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(373, 509);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "开始转码";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 515);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "任务数：";
            // 
            // txtTaskCount
            // 
            this.txtTaskCount.Location = new System.Drawing.Point(221, 512);
            this.txtTaskCount.Name = "txtTaskCount";
            this.txtTaskCount.Size = new System.Drawing.Size(27, 21);
            this.txtTaskCount.TabIndex = 10;
            this.txtTaskCount.Text = "1";
            // 
            // listView2
            // 
            this.listView2.AllowDrop = true;
            this.listView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView2.ContextMenuStrip = this.contextMenuStrip1;
            this.listView2.FullRowSelect = true;
            this.listView2.Location = new System.Drawing.Point(3, 3);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(577, 490);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "序号";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "文件名";
            this.columnHeader3.Width = 162;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "状态";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "路径";
            this.columnHeader5.Width = 268;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除此任务ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 26);
            // 
            // 删除此任务ToolStripMenuItem
            // 
            this.删除此任务ToolStripMenuItem.Name = "删除此任务ToolStripMenuItem";
            this.删除此任务ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除此任务ToolStripMenuItem.Text = "删除此任务";
            this.删除此任务ToolStripMenuItem.Click += new System.EventHandler(this.删除此任务ToolStripMenuItem_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = null;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(239, 17);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 25;
            this.label18.Text = "解码设定";
            // 
            // cbdecoderMode
            // 
            this.cbdecoderMode.FormattingEnabled = true;
            this.cbdecoderMode.Items.AddRange(new object[] {
            "default",
            "pipe",
            "self"});
            this.cbdecoderMode.Location = new System.Drawing.Point(294, 13);
            this.cbdecoderMode.Name = "cbdecoderMode";
            this.cbdecoderMode.Size = new System.Drawing.Size(121, 20);
            this.cbdecoderMode.TabIndex = 24;
            this.cbdecoderMode.Text = "default";
            this.toolTip1.SetToolTip(this.cbdecoderMode, "倾向于使用pipe传送还是编码器本体直接处理");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 729);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "简单批量x264转码";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.AddTask.ResumeLayout(false);
            this.AddTask.PerformLayout();
            this.gbVedio.ResumeLayout(false);
            this.gbVedio.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.inputfileMenuStrip.ResumeLayout(false);
            this.tbAvs.ResumeLayout(false);
            this.tbAvs.PerformLayout();
            this.tabVS.ResumeLayout(false);
            this.tabVS.PerformLayout();
            this.TaskList.ResumeLayout(false);
            this.TaskList.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出RToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage AddTask;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox cblCompeteAction;
        private System.Windows.Forms.CheckBox cbCompleteDo;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.CheckBox cbKeepFileTree;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQuality;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOutputPath;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnAddtoTasklist;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader FullName;
        private System.Windows.Forms.ColumnHeader FilePath;
        private System.Windows.Forms.TabPage TaskList;
        private System.Windows.Forms.GroupBox gbVedio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbUseAudio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTaskCount;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbColorDepth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MyFolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbpreset;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除此任务ToolStripMenuItem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUserArgs;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbcsp;
        private System.Windows.Forms.Button btnOneclickStart;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbVedioConfigTemplete;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtbitrate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tbAvs;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtAudioInput;
        private System.Windows.Forms.CheckBox cbloadSub;
        private System.Windows.Forms.TextBox txtAvsFile;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox cbUseAvsTemplete;
        private System.Windows.Forms.TextBox txtAvsScript;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbMuxer;
        private System.Windows.Forms.CheckBox cbcopuaudio;
        private System.Windows.Forms.CheckBox cbEnableQpmod;
        private System.Windows.Forms.ComboBox cbAvsTemplate;
        private System.Windows.Forms.TabPage tabVS;
        private System.Windows.Forms.ComboBox combVSTemplate;
        private System.Windows.Forms.TextBox txtVsScript;
        private System.Windows.Forms.CheckBox cbUseVSTemplete;
        private System.Windows.Forms.CheckBox cbUseEac3to;
        private System.Windows.Forms.TextBox txtAudioTracker;
        private System.Windows.Forms.ContextMenuStrip inputfileMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 删除toolStripMenuItem;
        private System.Windows.Forms.TextBox txtAudioLine;
        private System.Windows.Forms.ComboBox cbAudioEncoder;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cbdecoderMode;
    }
}

