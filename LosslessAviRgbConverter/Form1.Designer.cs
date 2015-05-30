namespace LosslessAviRgbConverter
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.AddTask = new System.Windows.Forms.TabPage();
            this.gbVedio = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbColorMatrix = new System.Windows.Forms.ComboBox();
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
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FullName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label11 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.AddTask.SuspendLayout();
            this.gbVedio.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TaskList.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(596, 25);
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
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 605);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(596, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Controls.Add(this.AddTask);
            this.tabControl1.Controls.Add(this.TaskList);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(596, 580);
            this.tabControl1.TabIndex = 5;
            // 
            // AddTask
            // 
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
            this.AddTask.Controls.Add(this.button1);
            this.AddTask.Controls.Add(this.listView1);
            this.AddTask.Location = new System.Drawing.Point(4, 22);
            this.AddTask.Name = "AddTask";
            this.AddTask.Padding = new System.Windows.Forms.Padding(3);
            this.AddTask.Size = new System.Drawing.Size(588, 554);
            this.AddTask.TabIndex = 1;
            this.AddTask.Text = "添加任务";
            this.AddTask.UseVisualStyleBackColor = true;
            // 
            // gbVedio
            // 
            this.gbVedio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbVedio.Controls.Add(this.label11);
            this.gbVedio.Controls.Add(this.label10);
            this.gbVedio.Controls.Add(this.cbColorMatrix);
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
            this.gbVedio.Location = new System.Drawing.Point(16, 336);
            this.gbVedio.Name = "gbVedio";
            this.gbVedio.Size = new System.Drawing.Size(535, 139);
            this.gbVedio.TabIndex = 18;
            this.gbVedio.TabStop = false;
            this.gbVedio.Text = "视频参数";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(393, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 15;
            this.label10.Text = "矩阵：";
            // 
            // cbColorMatrix
            // 
            this.cbColorMatrix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColorMatrix.FormattingEnabled = true;
            this.cbColorMatrix.Items.AddRange(new object[] {
            "YCbCr.BT709",
            "YCgCo"});
            this.cbColorMatrix.Location = new System.Drawing.Point(440, 42);
            this.cbColorMatrix.Name = "cbColorMatrix";
            this.cbColorMatrix.Size = new System.Drawing.Size(89, 20);
            this.cbColorMatrix.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "自定义：";
            // 
            // txtUserArgs
            // 
            this.txtUserArgs.Location = new System.Drawing.Point(72, 77);
            this.txtUserArgs.Multiline = true;
            this.txtUserArgs.Name = "txtUserArgs";
            this.txtUserArgs.Size = new System.Drawing.Size(448, 56);
            this.txtUserArgs.TabIndex = 12;
            this.txtUserArgs.Text = "--chroma-qp-offset -3 --fade-compensate 0.2 --min-keyint 1  --bframes 9 --qcomp 0" +
    ".60 --rc-lookahead 96 --partitions all --direct auto  --me umh   --psy-rd 0.40:0" +
    ".00 --aq-mode 3 --aq-strength 0.8";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(309, 42);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(51, 21);
            this.textBox5.TabIndex = 11;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(247, 42);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(56, 21);
            this.textBox4.TabIndex = 10;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(156, 47);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "调整分辨率";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 47);
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
            "psnr",
            "ssim"});
            this.comboBox1.Location = new System.Drawing.Point(72, 44);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(56, 20);
            this.comboBox1.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(405, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "预设";
            // 
            // cbpreset
            // 
            this.cbpreset.FormattingEnabled = true;
            this.cbpreset.Items.AddRange(new object[] {
            "medium",
            "slow",
            "slower",
            "veryslow"});
            this.cbpreset.Location = new System.Drawing.Point(440, 16);
            this.cbpreset.Name = "cbpreset";
            this.cbpreset.Size = new System.Drawing.Size(71, 20);
            this.cbpreset.TabIndex = 5;
            this.cbpreset.Text = "slow";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(227, 17);
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
            this.cbColorDepth.Location = new System.Drawing.Point(181, 14);
            this.cbColorDepth.Name = "cbColorDepth";
            this.cbColorDepth.Size = new System.Drawing.Size(40, 20);
            this.cbColorDepth.TabIndex = 3;
            this.cbColorDepth.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(151, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "色深";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(84, 14);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(41, 21);
            this.textBox3.TabIndex = 1;
            this.textBox3.Text = "25";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "恒定质量";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.cbUseAudio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtQuality);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 481);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 58);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "音频参数";
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
            this.label1.Location = new System.Drawing.Point(148, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "质量：";
            // 
            // txtQuality
            // 
            this.txtQuality.Location = new System.Drawing.Point(195, 14);
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.Size = new System.Drawing.Size(40, 21);
            this.txtQuality.TabIndex = 5;
            this.txtQuality.Text = "0.65";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "(0~1,0.65 250K)";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(476, 286);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "浏览";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox2.Location = new System.Drawing.Point(196, 288);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(239, 21);
            this.textBox2.TabIndex = 15;
            this.textBox2.Text = "完成后拷贝到360云盘目录就自动上传了呗";
            // 
            // cbKeepFileTree
            // 
            this.cbKeepFileTree.AutoSize = true;
            this.cbKeepFileTree.Location = new System.Drawing.Point(44, 314);
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
            this.cblCompeteAction.Location = new System.Drawing.Point(110, 288);
            this.cblCompeteAction.Name = "cblCompeteAction";
            this.cblCompeteAction.Size = new System.Drawing.Size(71, 20);
            this.cblCompeteAction.TabIndex = 14;
            // 
            // cbCompleteDo
            // 
            this.cbCompleteDo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbCompleteDo.AutoSize = true;
            this.cbCompleteDo.Location = new System.Drawing.Point(44, 290);
            this.cbCompleteDo.Name = "cbCompleteDo";
            this.cbCompleteDo.Size = new System.Drawing.Size(60, 16);
            this.cbCompleteDo.TabIndex = 13;
            this.cbCompleteDo.Text = "完成后";
            this.cbCompleteDo.UseVisualStyleBackColor = true;
            // 
            // btnClearList
            // 
            this.btnClearList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearList.Location = new System.Drawing.Point(476, 219);
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
            this.btnOutputPath.Location = new System.Drawing.Point(476, 248);
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
            this.textBox1.Location = new System.Drawing.Point(16, 250);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(425, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(476, 525);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "添加任务";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
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
            this.listView1.Location = new System.Drawing.Point(8, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(572, 197);
            this.listView1.TabIndex = 0;
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
            // TaskList
            // 
            this.TaskList.Controls.Add(this.button3);
            this.TaskList.Controls.Add(this.label3);
            this.TaskList.Controls.Add(this.txtTaskCount);
            this.TaskList.Controls.Add(this.listView2);
            this.TaskList.Location = new System.Drawing.Point(4, 22);
            this.TaskList.Name = "TaskList";
            this.TaskList.Size = new System.Drawing.Size(588, 554);
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(257, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = "色彩空间：i444";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 627);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "无损色彩视频转码";
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
        private System.Windows.Forms.Button button1;
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
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
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
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.ComboBox cbColorMatrix;
        private System.Windows.Forms.Label label11;
    }
}

