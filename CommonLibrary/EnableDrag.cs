using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CommonLibrary
{
    public class EnableDrag
    {
        public enum ChangeWindowMessageFilterFlags : uint
        {
            Add = 1, Remove = 2
        };
        [DllImport("user32")]
        public static extern bool ChangeWindowMessageFilter(uint msg, ChangeWindowMessageFilterFlags flags);

        public static void EnableDragMethod()
        {
            ChangeWindowMessageFilter(0x233/*_WM_DROPFILES*/, ChangeWindowMessageFilterFlags.Add/*_MSGFLT_ADD*/);
            ChangeWindowMessageFilter(0x0049/*_WM_COPYGLOBALDATA*/, ChangeWindowMessageFilterFlags.Add/*_MSGFLT_ADD*/);

        }
    }
}
