using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ToolSet
{
    public partial class TextboxFile : UserControl
    {
        public TextboxFile()
        {
            InitializeComponent();
        }
        public override string Text
        {
            get
            {
                return this.txtFileName.Text;
            }
            set
            {
                this.txtFileName.Text = value;
            }
        }

        private void txtFileName_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string path in s)
            {
                if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    txtFileName.Text = path;
                }

            }
        }

        private void txtFileName_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        private string filter = "所有文件|*";
        public string Filter
        {
            set { filter = value; }
            get { return filter; }
        }
        private void btnbowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filter;
            var result = ofd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string fullname in ofd.FileNames)
                {
                    txtFileName.Text = fullname;
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public new event TextChangedEventHandler TextChanged;
        public delegate void TextChangedEventHandler(object sender, TextChangedEventArgs e);
        private void txtFileName_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
            {
                TextChanged.Invoke(txtFileName, new TextChangedEventArgs() { Text = txtFileName.Text });
            }
        }
    }
}
