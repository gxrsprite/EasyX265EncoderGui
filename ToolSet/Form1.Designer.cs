namespace ToolSet
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnAddtoffmpeg = new System.Windows.Forms.Button();
            this.listBoxffmpeg = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnffmpegmkv = new System.Windows.Forms.Button();
            this.btnffmpegflv = new System.Windows.Forms.Button();
            this.btnffmpegmp4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnMp4Mux = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtmp4delay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtmp4audio = new ToolSet.TextboxFile();
            this.txtmp4video = new ToolSet.TextboxFile();
            this.label2 = new System.Windows.Forms.Label();
            this.txtmp4trackid = new System.Windows.Forms.TextBox();
            this.btnmp4extract = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtfilemp4 = new ToolSet.TextboxFile();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textboxFile2 = new ToolSet.TextboxFile();
            this.txtMediaInfo = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBlackBug = new System.Windows.Forms.Button();
            this.btnTimebug = new System.Windows.Forms.Button();
            this.txtBuggerBitrate = new System.Windows.Forms.TextBox();
            this.txtflvbuggerfile = new ToolSet.TextboxFile();
            this.tabEac3to = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEacTracker = new System.Windows.Forms.TextBox();
            this.btnEac3toAAC = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFileEac = new ToolSet.TextboxFile();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEac3toQuality = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabEac3to.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 659);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(711, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(711, 30);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出XToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // 退出XToolStripMenuItem
            // 
            this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
            this.退出XToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.退出XToolStripMenuItem.Text = "退出(&X)";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabEac3to);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 30);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(711, 629);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnAddtoffmpeg);
            this.tabPage1.Controls.Add(this.listBoxffmpeg);
            this.tabPage1.Controls.Add(this.btnffmpegmkv);
            this.tabPage1.Controls.Add(this.btnffmpegflv);
            this.tabPage1.Controls.Add(this.btnffmpegmp4);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(703, 600);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ffmpeg";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnAddtoffmpeg
            // 
            this.btnAddtoffmpeg.Location = new System.Drawing.Point(32, 261);
            this.btnAddtoffmpeg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddtoffmpeg.Name = "btnAddtoffmpeg";
            this.btnAddtoffmpeg.Size = new System.Drawing.Size(100, 31);
            this.btnAddtoffmpeg.TabIndex = 6;
            this.btnAddtoffmpeg.Text = "添加文件";
            this.btnAddtoffmpeg.UseVisualStyleBackColor = true;
            this.btnAddtoffmpeg.Click += new System.EventHandler(this.btnAddtoffmpeg_Click);
            // 
            // listBoxffmpeg
            // 
            this.listBoxffmpeg.AllowDrop = true;
            this.listBoxffmpeg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listBoxffmpeg.Location = new System.Drawing.Point(12, 24);
            this.listBoxffmpeg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBoxffmpeg.Name = "listBoxffmpeg";
            this.listBoxffmpeg.Size = new System.Drawing.Size(676, 227);
            this.listBoxffmpeg.TabIndex = 5;
            this.listBoxffmpeg.UseCompatibleStateImageBehavior = false;
            this.listBoxffmpeg.View = System.Windows.Forms.View.Details;
            this.listBoxffmpeg.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBoxffmpeg_DragDrop);
            this.listBoxffmpeg.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBoxffmpeg_DragEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "文件名";
            this.columnHeader1.Width = 500;
            // 
            // btnffmpegmkv
            // 
            this.btnffmpegmkv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnffmpegmkv.Location = new System.Drawing.Point(556, 270);
            this.btnffmpegmkv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnffmpegmkv.Name = "btnffmpegmkv";
            this.btnffmpegmkv.Size = new System.Drawing.Size(105, 31);
            this.btnffmpegmkv.TabIndex = 4;
            this.btnffmpegmkv.Text = "转为mkv封装";
            this.btnffmpegmkv.UseVisualStyleBackColor = true;
            this.btnffmpegmkv.Click += new System.EventHandler(this.btnffmpegmkv_Click);
            // 
            // btnffmpegflv
            // 
            this.btnffmpegflv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnffmpegflv.Location = new System.Drawing.Point(443, 270);
            this.btnffmpegflv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnffmpegflv.Name = "btnffmpegflv";
            this.btnffmpegflv.Size = new System.Drawing.Size(105, 31);
            this.btnffmpegflv.TabIndex = 3;
            this.btnffmpegflv.Text = "转为flv封装";
            this.btnffmpegflv.UseVisualStyleBackColor = true;
            this.btnffmpegflv.Click += new System.EventHandler(this.btnffmpegflv_Click);
            // 
            // btnffmpegmp4
            // 
            this.btnffmpegmp4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnffmpegmp4.Location = new System.Drawing.Point(329, 270);
            this.btnffmpegmp4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnffmpegmp4.Name = "btnffmpegmp4";
            this.btnffmpegmp4.Size = new System.Drawing.Size(105, 31);
            this.btnffmpegmp4.TabIndex = 2;
            this.btnffmpegmp4.Text = "转为mp4封装";
            this.btnffmpegmp4.UseVisualStyleBackColor = true;
            this.btnffmpegmp4.Click += new System.EventHandler(this.btnffmpegmp4_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(221, 270);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "解封装";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnMp4Mux);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.txtmp4delay);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtmp4audio);
            this.tabPage2.Controls.Add(this.txtmp4video);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.txtmp4trackid);
            this.tabPage2.Controls.Add(this.btnmp4extract);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtfilemp4);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(703, 600);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "mp4box";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnMp4Mux
            // 
            this.btnMp4Mux.Location = new System.Drawing.Point(517, 279);
            this.btnMp4Mux.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMp4Mux.Name = "btnMp4Mux";
            this.btnMp4Mux.Size = new System.Drawing.Size(100, 31);
            this.btnMp4Mux.TabIndex = 13;
            this.btnMp4Mux.Text = "封装";
            this.btnMp4Mux.UseVisualStyleBackColor = true;
            this.btnMp4Mux.Click += new System.EventHandler(this.btnMp4Mux_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(467, 284);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "ms";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(289, 283);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "音频延时：";
            // 
            // txtmp4delay
            // 
            this.txtmp4delay.Location = new System.Drawing.Point(384, 279);
            this.txtmp4delay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtmp4delay.Name = "txtmp4delay";
            this.txtmp4delay.Size = new System.Drawing.Size(79, 22);
            this.txtmp4delay.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 247);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "音频：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 211);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "视频：";
            // 
            // txtmp4audio
            // 
            this.txtmp4audio.Filter = "所有文件|*";
            this.txtmp4audio.Location = new System.Drawing.Point(89, 239);
            this.txtmp4audio.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtmp4audio.Name = "txtmp4audio";
            this.txtmp4audio.Size = new System.Drawing.Size(571, 31);
            this.txtmp4audio.TabIndex = 7;
            // 
            // txtmp4video
            // 
            this.txtmp4video.Filter = "所有文件|*";
            this.txtmp4video.Location = new System.Drawing.Point(89, 200);
            this.txtmp4video.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtmp4video.Name = "txtmp4video";
            this.txtmp4video.Size = new System.Drawing.Size(571, 31);
            this.txtmp4video.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(409, 77);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "轨道：";
            // 
            // txtmp4trackid
            // 
            this.txtmp4trackid.Location = new System.Drawing.Point(475, 67);
            this.txtmp4trackid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtmp4trackid.Name = "txtmp4trackid";
            this.txtmp4trackid.Size = new System.Drawing.Size(76, 22);
            this.txtmp4trackid.TabIndex = 4;
            this.txtmp4trackid.Text = "1";
            // 
            // btnmp4extract
            // 
            this.btnmp4extract.Location = new System.Drawing.Point(560, 64);
            this.btnmp4extract.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnmp4extract.Name = "btnmp4extract";
            this.btnmp4extract.Size = new System.Drawing.Size(79, 31);
            this.btnmp4extract.TabIndex = 3;
            this.btnmp4extract.Text = "抽取";
            this.btnmp4extract.UseVisualStyleBackColor = true;
            this.btnmp4extract.Click += new System.EventHandler(this.btnmp4extract_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "mp4文件：";
            // 
            // txtfilemp4
            // 
            this.txtfilemp4.Filter = "所有文件|*";
            this.txtfilemp4.Location = new System.Drawing.Point(85, 28);
            this.txtfilemp4.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtfilemp4.Name = "txtfilemp4";
            this.txtfilemp4.Size = new System.Drawing.Size(575, 28);
            this.txtfilemp4.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textboxFile2);
            this.tabPage3.Controls.Add(this.txtMediaInfo);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(703, 600);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "MediaInfo";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textboxFile2
            // 
            this.textboxFile2.Filter = "所有文件|*";
            this.textboxFile2.Location = new System.Drawing.Point(11, 23);
            this.textboxFile2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.textboxFile2.Name = "textboxFile2";
            this.textboxFile2.Size = new System.Drawing.Size(572, 43);
            this.textboxFile2.TabIndex = 1;
            this.textboxFile2.TextChanged += new ToolSet.TextboxFile.TextChangedEventHandler(this.textboxFile2_TextChanged);
            // 
            // txtMediaInfo
            // 
            this.txtMediaInfo.AllowDrop = true;
            this.txtMediaInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMediaInfo.Location = new System.Drawing.Point(11, 73);
            this.txtMediaInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMediaInfo.Multiline = true;
            this.txtMediaInfo.Name = "txtMediaInfo";
            this.txtMediaInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMediaInfo.Size = new System.Drawing.Size(677, 515);
            this.txtMediaInfo.TabIndex = 0;
            this.txtMediaInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtMediaInfo_DragDrop);
            this.txtMediaInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBoxffmpeg_DragEnter);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.btnBlackBug);
            this.tabPage4.Controls.Add(this.btnTimebug);
            this.tabPage4.Controls.Add(this.txtBuggerBitrate);
            this.tabPage4.Controls.Add(this.txtflvbuggerfile);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(703, 600);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "flvbugger";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(312, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kbps";
            // 
            // btnBlackBug
            // 
            this.btnBlackBug.Location = new System.Drawing.Point(507, 96);
            this.btnBlackBug.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBlackBug.Name = "btnBlackBug";
            this.btnBlackBug.Size = new System.Drawing.Size(100, 31);
            this.btnBlackBug.TabIndex = 3;
            this.btnBlackBug.Text = "后黑";
            this.btnBlackBug.UseVisualStyleBackColor = true;
            this.btnBlackBug.Click += new System.EventHandler(this.btnBlackBug_Click);
            // 
            // btnTimebug
            // 
            this.btnTimebug.Location = new System.Drawing.Point(381, 96);
            this.btnTimebug.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTimebug.Name = "btnTimebug";
            this.btnTimebug.Size = new System.Drawing.Size(100, 31);
            this.btnTimebug.TabIndex = 2;
            this.btnTimebug.Text = "前黑";
            this.btnTimebug.UseVisualStyleBackColor = true;
            this.btnTimebug.Click += new System.EventHandler(this.btnTimebug_Click);
            // 
            // txtBuggerBitrate
            // 
            this.txtBuggerBitrate.Location = new System.Drawing.Point(233, 96);
            this.txtBuggerBitrate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuggerBitrate.Name = "txtBuggerBitrate";
            this.txtBuggerBitrate.Size = new System.Drawing.Size(69, 22);
            this.txtBuggerBitrate.TabIndex = 1;
            this.txtBuggerBitrate.Text = "999";
            // 
            // txtflvbuggerfile
            // 
            this.txtflvbuggerfile.Filter = "flv文件|*.flv|所有文件|*";
            this.txtflvbuggerfile.Location = new System.Drawing.Point(36, 44);
            this.txtflvbuggerfile.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtflvbuggerfile.Name = "txtflvbuggerfile";
            this.txtflvbuggerfile.Size = new System.Drawing.Size(571, 31);
            this.txtflvbuggerfile.TabIndex = 0;
            // 
            // tabEac3to
            // 
            this.tabEac3to.Controls.Add(this.txtEac3toQuality);
            this.tabEac3to.Controls.Add(this.label10);
            this.tabEac3to.Controls.Add(this.label8);
            this.tabEac3to.Controls.Add(this.txtEacTracker);
            this.tabEac3to.Controls.Add(this.btnEac3toAAC);
            this.tabEac3to.Controls.Add(this.label9);
            this.tabEac3to.Controls.Add(this.txtFileEac);
            this.tabEac3to.Location = new System.Drawing.Point(4, 25);
            this.tabEac3to.Name = "tabEac3to";
            this.tabEac3to.Padding = new System.Windows.Forms.Padding(3);
            this.tabEac3to.Size = new System.Drawing.Size(703, 600);
            this.tabEac3to.TabIndex = 4;
            this.tabEac3to.Text = "Eac3to";
            this.tabEac3to.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(155, 81);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "轨道：";
            // 
            // txtEacTracker
            // 
            this.txtEacTracker.Location = new System.Drawing.Point(213, 78);
            this.txtEacTracker.Margin = new System.Windows.Forms.Padding(4);
            this.txtEacTracker.Name = "txtEacTracker";
            this.txtEacTracker.Size = new System.Drawing.Size(76, 22);
            this.txtEacTracker.TabIndex = 9;
            this.txtEacTracker.Text = "2";
            // 
            // btnEac3toAAC
            // 
            this.btnEac3toAAC.Location = new System.Drawing.Point(558, 69);
            this.btnEac3toAAC.Margin = new System.Windows.Forms.Padding(4);
            this.btnEac3toAAC.Name = "btnEac3toAAC";
            this.btnEac3toAAC.Size = new System.Drawing.Size(79, 31);
            this.btnEac3toAAC.TabIndex = 8;
            this.btnEac3toAAC.Text = "转AAC";
            this.btnEac3toAAC.UseVisualStyleBackColor = true;
            this.btnEac3toAAC.Click += new System.EventHandler(this.btnEac3toAAC_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 43);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "视频：";
            // 
            // txtFileEac
            // 
            this.txtFileEac.Filter = "所有文件|*";
            this.txtFileEac.Location = new System.Drawing.Point(98, 31);
            this.txtFileEac.Margin = new System.Windows.Forms.Padding(5);
            this.txtFileEac.Name = "txtFileEac";
            this.txtFileEac.Size = new System.Drawing.Size(575, 28);
            this.txtFileEac.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(339, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 17);
            this.label10.TabIndex = 11;
            this.label10.Text = "质量：";
            // 
            // txtEac3toQuality
            // 
            this.txtEac3toQuality.Location = new System.Drawing.Point(395, 78);
            this.txtEac3toQuality.Name = "txtEac3toQuality";
            this.txtEac3toQuality.Size = new System.Drawing.Size(100, 22);
            this.txtEac3toQuality.TabIndex = 12;
            this.txtEac3toQuality.Text = "0.6";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 681);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "工具集";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabEac3to.ResumeLayout(false);
            this.tabEac3to.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出XToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnffmpegmkv;
        private System.Windows.Forms.Button btnffmpegflv;
        private System.Windows.Forms.Button btnffmpegmp4;
        private System.Windows.Forms.ListView listBoxffmpeg;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnAddtoffmpeg;
        private System.Windows.Forms.Label label1;
        private TextboxFile txtfilemp4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtmp4trackid;
        private System.Windows.Forms.Button btnmp4extract;
        private System.Windows.Forms.TabPage tabPage3;
        private TextboxFile textboxFile2;
        private System.Windows.Forms.TextBox txtMediaInfo;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnBlackBug;
        private System.Windows.Forms.Button btnTimebug;
        private System.Windows.Forms.TextBox txtBuggerBitrate;
        private TextboxFile txtflvbuggerfile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private TextboxFile txtmp4audio;
        private TextboxFile txtmp4video;
        private System.Windows.Forms.Button btnMp4Mux;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtmp4delay;
        private System.Windows.Forms.TabPage tabEac3to;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEacTracker;
        private System.Windows.Forms.Button btnEac3toAAC;
        private System.Windows.Forms.Label label9;
        private TextboxFile txtFileEac;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtEac3toQuality;
    }
}

