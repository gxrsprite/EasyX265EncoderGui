using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Easyx264CoderGUI
{
    public partial class EncoderTaskInfoForm : Form
    {
        public EncoderTaskInfoForm()
        {
            InitializeComponent();
        }
        public FileConfig fileConfig = null;
        private void EncoderTaskInfoForm_Load(object sender, EventArgs e)
        {

        }

        public void AppendText(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke((Action)delegate()
                {
                    txtInfo.AppendText(text + "\r\n");
                });
            }
            else
            {
                txtInfo.AppendText(text + "\r\n");
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

    }
}
