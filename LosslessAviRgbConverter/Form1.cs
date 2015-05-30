using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LosslessAviRgbConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cblCompeteAction.SelectedIndex = 0;
            cbColorMatrix.SelectedIndex = 0;
        }

        public static string FileExtension = ".avi";
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            textBox1.Text = Path.GetDirectoryName(s[0]);
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
                ListViewItem lvi = new ListViewItem();
                lvi.Text = (listView1.Items.Count + 1).ToString();
                lvi.SubItems.Add(Path.GetFileName(path));//.Name = "FileName";
                lvi.SubItems.Add(Path.GetDirectoryName(path));//.Name = "Path";
                FileConfig fileConfig = new FileConfig();
                fileConfig.FullName = path;
                fileConfig.DirPath = dir;
                lvi.Tag = fileConfig;
                listView1.Items.Add(lvi);
            }
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (File.GetAttributes(s[0]).HasFlag(FileAttributes.Directory))
            {
                textBox1.Text = s[0];
            }
            else
            {
                textBox1.Text = Path.GetDirectoryName(s[0]);
            }
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
        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.Items)
            {
                FileConfig fileConfig = lvi.Tag as FileConfig;
                fileConfig.OutputPath = textBox1.Text;
                fileConfig.CompleteDo = cbCompleteDo.Checked;
                fileConfig.CompleteAction = cblCompeteAction.Text;
                fileConfig.CompleteActionDir = textBox2.Text;
                fileConfig.KeepDirection = cbKeepFileTree.Checked;
                VedioConfig vedioConfig = fileConfig.VedioConfig;
                vedioConfig.crf = float.Parse(textBox3.Text);
                vedioConfig.depth = int.Parse(cbColorDepth.Text);
                vedioConfig.preset = cbpreset.Text;
                vedioConfig.tune = comboBox1.Text;
                vedioConfig.UserArgs = txtUserArgs.Text;
                vedioConfig.Resize = checkBox1.Checked;

                vedioConfig.ColorMatrix = ColorMatrix.Convert(cbColorMatrix.Text);
                vedioConfig.Width = textBox4.Text == "" ? 0 : int.Parse(textBox4.Text);
                vedioConfig.Height = textBox5.Text == "" ? 0 : int.Parse(textBox5.Text);

                AudioConfig audioConfig = fileConfig.AudioConfig;
                audioConfig.Enabled = cbUseAudio.Checked;
                audioConfig.Quality = float.Parse(txtQuality.Text);

                ListViewItem lvi2 = new ListViewItem();
                lvi2.Text = (listView2.Items.Count + 1).ToString();
                lvi2.SubItems.Add(fileConfig.FullName);//.Name = "FileName";
                lvi2.SubItems.Add("待转码").Name = "States";
                lvi2.SubItems.Add(fileConfig.DirPath);//.Name = "Path";
                lvi2.Tag = fileConfig;
                listView2.Items.Add(lvi2);

            }
        }

        private void btnClearList_Click_1(object sender, EventArgs e)
        {
            listView1.Clear();
        }

        private void 删除此任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView2.SelectedItems)
            {
                if (hasHandle > -1)
                {
                    if ((item.Tag as FileConfig).state > -1)
                    {
                        hasHandle--;
                    }
                }
                listView2.Items.Remove(item);
            }
        }


        int threadcount = 4;
        int hasHandle = -1;
        object handledlock = new object();
        private void button3_Click(object sender, EventArgs e)
        {

            threadcount = Convert.ToInt32(txtTaskCount.Text);
            Task.Factory.StartNew(delegate()
            {
                List<Task> tasks = new List<Task>();
                for (int i = 0; i < threadcount; i++)
                {
                    var t = Task.Factory.StartNew(delegate()
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

                ListViewItem item = null;
                FileConfig fileConfig = null;
                lock (handledlock)
                {
                    if (isHandling >= listView2.Items.Count - 1)
                    {
                        return;
                    }
                    hasHandle++;
                    isHandling = hasHandle;

                    this.Invoke((Action)delegate()
                    {
                        item = listView2.Items[isHandling];
                        listView2.Items[isHandling].SubItems["States"].Text = "视频转码中";
                    });
                    fileConfig = item.Tag as FileConfig;
                    fileConfig.state++;
                }
                string outputfile = "";
                string copyto = string.Empty;
                string ralative = string.Empty;
                //仅输出视频部分
                if (fileConfig.KeepDirection)
                {//保持目录树结构
                    ralative = FileUtility.MakeRelativePath(fileConfig.DirPath + "/", Path.GetDirectoryName(fileConfig.FullName));
                    string outpath = Path.Combine(fileConfig.OutputPath, ralative);
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

                //视频转码
                string avsScriptFullName = CommandHelper.GetAvsScriptFileName(fileConfig);

                string vedioOutputFile = CommandHelper.RunX264Command(fileConfig, avsScriptFullName);
                if (fileConfig.AudioConfig.Enabled)
                {
                    this.Invoke((Action)delegate()
                    {
                        item = listView2.Items[isHandling];
                        listView2.Items[isHandling].SubItems["States"].Text = "音频转码中";
                    });
                    string audiofile = CommandHelper.RunFFmpegToAAC(fileConfig);

                    this.Invoke((Action)delegate()
                    {
                        item = listView2.Items[isHandling];
                        listView2.Items[isHandling].SubItems["States"].Text = "mkv封装中";
                    });
                    CommandHelper.MKVmergin(fileConfig, vedioOutputFile, audiofile);

                }

                if (fileConfig.CompleteDo)
                {
                    try
                    {
                        if (cblCompeteAction.SelectedIndex == 0)
                        {
                            this.Invoke((Action)delegate()
                            {
                                listView1.Items[isHandling].SubItems["States"].Text = "拷贝中";
                            });

                            File.Copy(vedioOutputFile, copyto, true);

                        }
                        else if (cblCompeteAction.SelectedIndex == 1)
                        {
                            this.Invoke((Action)delegate()
                            {
                                listView1.Items[isHandling].SubItems["States"].Text = "剪切中";
                            });
                            if (File.Exists(copyto))
                            {
                                File.Delete(copyto);
                            }
                            File.Move(vedioOutputFile, copyto);
                        }
                    }
                    catch { }
                }

                this.Invoke((Action)delegate()
                {
                    listView2.Items[isHandling].SubItems["States"].Text = "完成";
                });

            }

        }


    }
}
