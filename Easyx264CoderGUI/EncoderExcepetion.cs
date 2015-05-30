using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easyx264CoderGUI
{
    [Serializable]
    public class EncoderException : Exception
    {
        public EncoderException() { }
        public EncoderException(string message) : base(message) { }
        public EncoderException(string message, Exception inner) : base(message, inner) { }
        protected EncoderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
