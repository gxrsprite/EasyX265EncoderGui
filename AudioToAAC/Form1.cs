using Id3;
using Id3.Id3v1;
using Id3.Id3v2;
using Id3.Id3v2.v23;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioToAAC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Process.EnterDebugMode();
        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        public static string FileExtension = ".flac|.ape|.wav|.mp3|.aac|.mp4|.flv|.f4v|.m4a|.avi|.pcm|.ts|.tp|.amr|.mkv|.dts|.ac3";
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

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
                    AddFileToList(path, "");
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
                lvi.SubItems.Add("待转码").Name = "States";
                lvi.SubItems.Add(Path.GetDirectoryName(path));//.Name = "Path";
                lvi.Tag = new AudioInfo(path, dir);
                listView1.Items.Add(lvi);
            }
        }

        int threadcount = 4;
        int hasHandle = -1;
        object handledlock = new object();
        private void button1_Click(object sender, EventArgs e)
        {
            btnClearList.Enabled = false;
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
                  Task.WaitAll(tasks.ToArray());
                  hasHandle = -1;

                  Invoke((Action)delegate()
                  {
                      btnClearList.Enabled = true;
                  });
              });
        }

        private void StartOneThread()
        {
            int isHandling = -1;
            while (true)
            {

                lock (handledlock)
                {
                    hasHandle++;
                    isHandling = hasHandle;
                }
                if (isHandling >= listView1.Items.Count)
                {
                    return;
                }
                ListViewItem item = null;

                this.Invoke((Action)delegate()
                {
                    item = listView1.Items[isHandling];
                    listView1.Items[isHandling].SubItems["States"].Text = "转码中";
                });
                AudioInfo info = item.Tag as AudioInfo;

                string output = string.Empty;
                string copyto = string.Empty;
                if (cbKeepFileTree.Checked)
                {
                    string ralative = FileUtility.MakeRelativePath(info.DirPath + "/", Path.GetDirectoryName(info.FullName));
                    string outpath = Path.Combine(textBox1.Text, ralative);
                    output = Path.Combine(outpath, Path.GetFileNameWithoutExtension(info.FullName) + ".m4a");
                    string copytopath = Path.Combine(textBox2.Text, ralative);
                    copyto = Path.Combine(copytopath, Path.GetFileNameWithoutExtension(info.FullName) + ".m4a");
                    if (!Directory.Exists(outpath))
                    {
                        Directory.CreateDirectory(outpath);
                    }
                    if (!Directory.Exists(copyto))
                    {
                        Directory.CreateDirectory(copyto);
                    }

                }
                else
                {
                    output = Path.Combine(textBox1.Text, Path.GetFileNameWithoutExtension(info.FullName) + ".m4a");
                    copyto = Path.Combine(textBox2.Text, Path.GetFileNameWithoutExtension(info.FullName) + ".m4a");
                }

                string bat = getbat(info.FullName, output);
                ProcessStartInfo processinfo = new ProcessStartInfo();
                processinfo.FileName = Environment.GetEnvironmentVariable("ComSpec");
                processinfo.Arguments = "/c " + bat;
                processinfo.UseShellExecute = false;    //输出信息重定向
                processinfo.CreateNoWindow = true;
                processinfo.RedirectStandardInput = true;
                processinfo.RedirectStandardOutput = true;
                processinfo.RedirectStandardError = false;
                processinfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process ffmpeg = new Process();
                ffmpeg.StartInfo = processinfo;
                ffmpeg.Start();

                var result = ffmpeg.StandardOutput.ReadToEnd();
                ffmpeg.WaitForExit();
                //ffmpeg.Kill();//等待进程结束
                ffmpeg.Dispose();

                if (cbCopyID3.Checked == true)
                {
                    try
                    {
                        using (var mp3 = new Mp3File(info.FullName))
                        {
                            if (mp3.HasTagOfFamily(Id3TagFamily.FileStartTag))
                            { }
                            else
                            {
                                using (var outputmp3 = new Mp3File(output, Mp3Permissions.ReadWrite))
                                {
                                    Id3v1Tag id3tag = Id3Tag.Create<Id3v1Tag>();
                                    TagConsole.ReadTagToID3(id3tag, info.FullName);
                                    outputmp3.WriteTag(id3tag, id3tag.MajorVersion, id3tag.MinorVersion, WriteConflictAction.Replace);
                                }
                            }
                        }
                    }
                    catch { }
                }
                if (cbCompleteDo.Checked)
                {
                    try
                    {
                        if (cblCompeteAction.SelectedIndex == 0)
                        {
                            this.Invoke((Action)delegate()
                            {
                                listView1.Items[isHandling].SubItems["States"].Text = "拷贝中";
                            });

                            File.Copy(output, copyto, true);

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
                            File.Move(output, copyto);
                        }
                    }
                    catch { }
                }

                this.Invoke((Action)delegate()
                {
                    listView1.Items[isHandling].SubItems["States"].Text = "完成";
                });

            }

        }



        private string getbat(string input)
        {
            return getbat(input, "");
        }

        private string getbat(string input, string output)
        {
            if (string.IsNullOrEmpty(output))
            {
                output = Path.GetFileNameWithoutExtension(input) + ".m4a";
            }
            return string.Format("tools\\ffmpeg.exe -vn -i \"{0}\" -f  wav pipe:| tools\\neroAacEnc -ignorelength -q {2} -lc -if - -of \"{1}\"",
                input, output, txtQuality.Text);
        }

        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }

        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cblCompeteAction.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }


    }
}
