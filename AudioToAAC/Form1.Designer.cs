namespace AudioToAAC
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.AudioPage = new System.Windows.Forms.TabPage();
            this.btnClearList = new System.Windows.Forms.Button();
            this.cbCopyID3 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTaskCount = new System.Windows.Forms.TextBox();
            this.cbKeepFileTree = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtQuality = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FullName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.States = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cbCompleteDo = new System.Windows.Forms.CheckBox();
            this.cblCompeteAction = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.AudioPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(532, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出RToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件ToolStripMenuItem.Text = "文件(&F)";
            this.文件ToolStripMenuItem.Click += new System.EventHandler(this.文件ToolStripMenuItem_Click);
            // 
            // 退出RToolStripMenuItem
            // 
            this.退出RToolStripMenuItem.Name = "退出RToolStripMenuItem";
            this.退出RToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.退出RToolStripMenuItem.Text = "退出(&X)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 475);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(532, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Controls.Add(this.AudioPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(532, 450);
            this.tabControl1.TabIndex = 1;
            // 
            // AudioPage
            // 
            this.AudioPage.Controls.Add(this.button2);
            this.AudioPage.Controls.Add(this.textBox2);
            this.AudioPage.Controls.Add(this.cblCompeteAction);
            this.AudioPage.Controls.Add(this.cbCompleteDo);
            this.AudioPage.Controls.Add(this.btnClearList);
            this.AudioPage.Controls.Add(this.cbCopyID3);
            this.AudioPage.Controls.Add(this.label3);
            this.AudioPage.Controls.Add(this.txtTaskCount);
            this.AudioPage.Controls.Add(this.cbKeepFileTree);
            this.AudioPage.Controls.Add(this.label2);
            this.AudioPage.Controls.Add(this.txtQuality);
            this.AudioPage.Controls.Add(this.label1);
            this.AudioPage.Controls.Add(this.btnOutputPath);
            this.AudioPage.Controls.Add(this.textBox1);
            this.AudioPage.Controls.Add(this.button1);
            this.AudioPage.Controls.Add(this.listView1);
            this.AudioPage.Location = new System.Drawing.Point(4, 22);
            this.AudioPage.Name = "AudioPage";
            this.AudioPage.Padding = new System.Windows.Forms.Padding(3);
            this.AudioPage.Size = new System.Drawing.Size(524, 424);
            this.AudioPage.TabIndex = 1;
            this.AudioPage.Text = "音频转码";
            this.AudioPage.UseVisualStyleBackColor = true;
            // 
            // btnClearList
            // 
            this.btnClearList.Location = new System.Drawing.Point(439, 185);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(75, 23);
            this.btnClearList.TabIndex = 11;
            this.btnClearList.Text = "清空列表";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // cbCopyID3
            // 
            this.cbCopyID3.AutoSize = true;
            this.cbCopyID3.Checked = true;
            this.cbCopyID3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCopyID3.Location = new System.Drawing.Point(10, 303);
            this.cbCopyID3.Name = "cbCopyID3";
            this.cbCopyID3.Size = new System.Drawing.Size(66, 16);
            this.cbCopyID3.TabIndex = 10;
            this.cbCopyID3.Text = "复制ID3";
            this.cbCopyID3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "任务数：";
            // 
            // txtTaskCount
            // 
            this.txtTaskCount.Location = new System.Drawing.Point(312, 261);
            this.txtTaskCount.Name = "txtTaskCount";
            this.txtTaskCount.Size = new System.Drawing.Size(27, 21);
            this.txtTaskCount.TabIndex = 8;
            this.txtTaskCount.Text = "4";
            // 
            // cbKeepFileTree
            // 
            this.cbKeepFileTree.AutoSize = true;
            this.cbKeepFileTree.Location = new System.Drawing.Point(10, 280);
            this.cbKeepFileTree.Name = "cbKeepFileTree";
            this.cbKeepFileTree.Size = new System.Drawing.Size(96, 16);
            this.cbKeepFileTree.TabIndex = 7;
            this.cbKeepFileTree.Text = "保持目录结构";
            this.cbKeepFileTree.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "(0~1,0.65 250K)";
            // 
            // txtQuality
            // 
            this.txtQuality.Location = new System.Drawing.Point(54, 261);
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.Size = new System.Drawing.Size(40, 21);
            this.txtQuality.TabIndex = 5;
            this.txtQuality.Text = "0.65";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "质量：";
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Location = new System.Drawing.Point(439, 225);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(75, 23);
            this.btnOutputPath.TabIndex = 3;
            this.btnOutputPath.Text = "输出";
            this.btnOutputPath.UseVisualStyleBackColor = true;
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputPath_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 227);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(425, 21);
            this.textBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(425, 381);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "开始转码";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.FullName,
            this.States,
            this.FilePath});
            this.listView1.Location = new System.Drawing.Point(8, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(508, 172);
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
            // States
            // 
            this.States.Text = "状态";
            // 
            // FilePath
            // 
            this.FilePath.Text = "路径";
            this.FilePath.Width = 268;
            // 
            // cbCompleteDo
            // 
            this.cbCompleteDo.AutoSize = true;
            this.cbCompleteDo.Location = new System.Drawing.Point(34, 351);
            this.cbCompleteDo.Name = "cbCompleteDo";
            this.cbCompleteDo.Size = new System.Drawing.Size(60, 16);
            this.cbCompleteDo.TabIndex = 13;
            this.cbCompleteDo.Text = "完成后";
            this.cbCompleteDo.UseVisualStyleBackColor = true;
            // 
            // cblCompeteAction
            // 
            this.cblCompeteAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cblCompeteAction.FormattingEnabled = true;
            this.cblCompeteAction.Items.AddRange(new object[] {
            "拷贝到",
            "剪切到"});
            this.cblCompeteAction.Location = new System.Drawing.Point(102, 346);
            this.cblCompeteAction.Name = "cblCompeteAction";
            this.cblCompeteAction.Size = new System.Drawing.Size(71, 20);
            this.cblCompeteAction.TabIndex = 14;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(195, 344);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(239, 21);
            this.textBox2.TabIndex = 15;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(439, 342);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "浏览";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 497);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "音频批量转码";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.AudioPage.ResumeLayout(false);
            this.AudioPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出RToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage AudioPage;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader FullName;
        private System.Windows.Forms.ColumnHeader FilePath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOutputPath;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ColumnHeader States;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQuality;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbKeepFileTree;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTaskCount;
        private System.Windows.Forms.CheckBox cbCopyID3;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.ComboBox cblCompeteAction;
        private System.Windows.Forms.CheckBox cbCompleteDo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
    }
}

