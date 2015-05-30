using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Easyx264CoderGUI
{

    public class EncoderTaskInfo
    {
        public Process process = null;
        public EncoderTaskInfoForm infoForm = null;
        public void AppendOutput(string text)
        {
            if (infoForm != null)
            {
                infoForm.AppendText(text);
            }
        }
    }
}
