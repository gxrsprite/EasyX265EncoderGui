using CommonLibrary;
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
using Tsanie.FlvBugger;

namespace ToolSet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //EnableDrag.EnableDragMethod();
            InitializeComponent();
            //NativeWrappers.ChangeWindowMessageFilter(0x0233/*WM_DROPFILES*/ , NativeWrappers.ChangeWindowMessageFilterFlags.Add);
            //NativeWrappers.ChangeWindowMessageFilter(0x004A/*WM_COPYDATA*/ , NativeWrappers.ChangeWindowMessageFilterFlags.Add);
            //NativeWrappers.ChangeWindowMessageFilter(0x0049, NativeWrappers.ChangeWindowMessageFilterFlags.Add);
        }
        #region FFMPEG
        private void listBoxffmpeg_DragEnter(object sender, DragEventArgs e)
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
        public static string FileExtension = ".avi|.mp4|.mkv|.wmv|.avs|.ts|.tp|.m2ts";
        private void listBoxffmpeg_DragDrop(object sender, DragEventArgs e)
        {
            var listbox = sender as ListView;
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string path in s)
            {
                if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    foreach (string p in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
                    {
                        if (FileExtension.Split('|').Any(f => f.Equals(Path.GetExtension(path), StringComparison.OrdinalIgnoreCase)))
                        {
                            listbox.Items.Add(path);
                        }
                    }
                }
                else
                {
                    listbox.Items.Add(path);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> filenames = new List<string>();
            foreach (ListViewItem item in listBoxffmpeg.Items)
            {
                filenames.Add(item.Text);
            }
            Task.Factory.StartNew(() =>
            {
                foreach (string filename in filenames)
                {
                    string dir = Path.GetDirectoryName(filename);
                    string name = Path.GetFileNameWithoutExtension(filename);

                    ffmpegCommand.DemuxVedio(filename, FileUtility.GetNoSameNameFile(Path.Combine(dir, name + "_video" + ".mkv")));
                    ffmpegCommand.DemuxAudio(filename, FileUtility.GetNoSameNameFile(Path.Combine(dir, name + "_audio" + ".mkv")));
                }
            });

        }

        private void btnffmpegmp4_Click(object sender, EventArgs e)
        {
            string extension = ".mp4";
            ChangeMux(extension);
        }

        private void ChangeMux(string extension)
        {
            List<string> filenames = new List<string>();
            foreach (ListViewItem item in listBoxffmpeg.Items)
            {
                filenames.Add(item.Text);
            }
            Task.Factory.StartNew(() =>
            {
                foreach (string filename in filenames)
                {
                    string dir = Path.GetDirectoryName(filename);
                    string name = Path.GetFileNameWithoutExtension(filename);

                    ffmpegCommand.ChangeMux(filename, FileUtility.GetNoSameNameFile(Path.Combine(dir, name + extension)));
                }
            });
        }

        private void btnffmpegflv_Click(object sender, EventArgs e)
        {
            ChangeMux(".flv");
        }

        private void btnffmpegmkv_Click(object sender, EventArgs e)
        {
            ChangeMux(".mkv");
        }

        private void btnAddtoffmpeg_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "视频文件|*.mp4;*.mkv;*.avi;*.ts;*.tp;*.ts;*.tp;*.m2ts|所有文件|*";
            var result = ofd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string fullname in ofd.FileNames)
                {
                    listBoxffmpeg.Items.Add(fullname);
                }
            }
        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region mp4box
        private void btnmp4extract_Click(object sender, EventArgs e)
        {
            Mp4boxCommand.Extract(txtfilemp4.Text, txtmp4trackid.Text);
        }
        #endregion


        #region mediainfo
        private void textboxFile2_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowMediaInfo(e.Text);
        }

        private void ShowMediaInfo(string fullname)
        {
            Task.Factory.StartNew(() =>
            {
                MediaInfo info = new MediaInfo(fullname);
                string text = info.MediaInfoText;
                this.Invoke((Action)delegate()
                {
                    txtMediaInfo.Text = text;
                });
            });
        }

        private void txtMediaInfo_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string path in s)
            {
                if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    ShowMediaInfo(path);
                }
            }
        }
        #endregion

        private void btnTimebug_Click(object sender, EventArgs e)
        {
            string filename = txtflvbuggerfile.Text;
            double bitrate = Double.Parse(txtBuggerBitrate.Text);

            Task.Factory.StartNew(() =>
            {

                string dir = Path.GetDirectoryName(filename);
                string name = Path.GetFileNameWithoutExtension(filename);
                FlvMain flvbugger = new FlvMain();
                flvbugger.addFile(filename);
                flvbugger.ExecuteTime(bitrate, 0, FileUtility.GetNoSameNameFile(Path.Combine(dir, name + "_time" + ".flv")));

            });

        }

        private void btnBlackBug_Click(object sender, EventArgs e)
        {
            string filename = txtflvbuggerfile.Text;
            double bitrate = Double.Parse(txtBuggerBitrate.Text);

            Task.Factory.StartNew(() =>
            {
                string dir = Path.GetDirectoryName(filename);
                string name = Path.GetFileNameWithoutExtension(filename);
                FlvMain flvbugger = new FlvMain();
                flvbugger.addFile(filename);
                flvbugger.ExecuteBlack(bitrate, 0, FileUtility.GetNoSameNameFile(Path.Combine(dir, name + "_black" + ".flv")));

            });
        }

        private void btnMp4Mux_Click(object sender, EventArgs e)
        {
            string filename = txtmp4video.Text;
            string audiofilename = txtmp4audio.Text;
            Task.Factory.StartNew(() =>
            {
                string dir = Path.GetDirectoryName(filename);
                string name = Path.GetFileNameWithoutExtension(filename);
                Mp4boxCommand.Mp4boxMux(filename, audiofilename, FileUtility.GetNoSameNameFile(Path.Combine(dir, name + ".mp4")));
            });
        }




    }
}
