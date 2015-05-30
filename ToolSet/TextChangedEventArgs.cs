using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolSet
{
    public class TextChangedEventArgs : EventArgs
    {
        private string test;

        public string Text
        {
            get { return test; }
            set { test = value; }
        }
    }
}
