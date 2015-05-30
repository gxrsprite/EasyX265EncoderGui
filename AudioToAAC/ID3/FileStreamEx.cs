using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ID3;

/*
 * The goal of this name space is just
 */
namespace System.IO
{
    internal class FileStreamEx : FileStream
    {
        public FileStreamEx(string path, FileMode mode)
            : base(path, mode) { }

        /// <summary>
        /// Read string from current FileStream
        /// </summary>
        /// <param name="MaxLength">Maximum length that can read from stream</param>
        /// <param name="TEncoding">TextEcoding to read from Stream</param>
        /// <param name="DetectEncoding">Can method recognize encoding of text from Encoding inicators</param>
        /// <returns>string readed from current FileStream</returns>
        public string ReadText(int MaxLength, TextEncodings TEncoding, ref int ReadedLength, bool DetectEncoding)
        {
            if (MaxLength <= 0)
                return "";
            long Pos = base.Position;

            MemoryStream MStream = new MemoryStream();
            if (DetectEncoding && MaxLength >= 3)
            {
                byte[] Buffer = new byte[3];
                base.Read(Buffer, 0, Buffer.Length);
                if (Buffer[0] == 0xFF && Buffer[1] == 0xFE)
                {   // FF FE
                    TEncoding = TextEncodings.UTF_16;// UTF-16 (LE)
                    base.Position--;
                    MaxLength -= 2;
                }
                else if (Buffer[0] == 0xFE && Buffer[1] == 0xFF)
                {   // FE FF
                    TEncoding = TextEncodings.UTF_16BE;
                    base.Position--;
                    MaxLength -= 2;
                }
                else if (Buffer[0] == 0xEF && Buffer[1] == 0xBB && Buffer[2] == 0xBF)
                {
                    // EF BB BF
                    TEncoding = TextEncodings.UTF8;
                    MaxLength -= 3;
                }
                else
                    base.Position -= 3;
            }
            bool Is2ByteSeprator = (TEncoding == TextEncodings.UTF_16 || TEncoding == TextEncodings.UTF_16BE);

            byte Buf;
            while (MaxLength > 0)
            {
                Buf = ReadByte(); // Read First/Next byte from stream

                if (Buf != 0) // if it's data byte
                    MStream.WriteByte(Buf);
                else // if Buf == 0
                {
                    if (Is2ByteSeprator)
                    {
                        byte Temp = ReadByte();
                        if (Temp == 0)
                            break;
                        else
                        {
                            MStream.WriteByte(Buf);
                            MStream.WriteByte(Temp);
                            MaxLength--;
                        }
                    }
                    else
                        break;
                }
                MaxLength--;
            }

            if (MaxLength < 0)
                base.Position += MaxLength;

            ReadedLength -= Convert.ToInt32(base.Position - Pos);

            return GetEncoding(TEncoding).GetString(MStream.ToArray());
        }

        public string ReadText(int MaxLength, TextEncodings TEncoding)
        {
            int i = 0;
            return ReadText(MaxLength, TEncoding, ref i, true);
        }

        public string ReadText(int MaxLength, TextEncodings TEncoding, bool DetectEncoding)
        { 
            int i = 0;
            return ReadText(MaxLength, TEncoding, ref i, DetectEncoding);
        }

        /// <summary>
        /// Read a byte from current FileStream
        /// </summary>
        /// <returns>Readed byte</returns>
        public new byte ReadByte()
        {
            byte[] RByte = new byte[1];

            // Use read method of FileStream instead of ReadByte
            // Becuase ReadByte return a SIGNED byte as integer
            // But what we want here is unsigned byte
            base.Read(RByte, 0, 1);

            return RByte[0];
        }

        /// <summary>
        /// Read some bytes from FileStream and return it as unsigned integer
        /// </summary>
        /// <param name="Length">length of number in bytes</param>
        /// <returns>uint represent number readed from stream</returns>
        public uint ReadUInt(int Length)
        {
            if (Length > 4 || Length < 1)
                throw (new ArgumentOutOfRangeException("ReadUInt method can read 1-4 byte(s)"));

            byte[] Buf = new byte[Length];
            byte[] RBuf = new byte[4];
            base.Read(Buf, 0, Length);
            Buf.CopyTo(RBuf, 4 - Buf.Length);
            Array.Reverse(RBuf);
            return BitConverter.ToUInt32(RBuf, 0);
        }        

        /// <summary>
        /// Read data from specific FileStream and return it as MemoryStream
        /// </summary>
        /// <param name="Length">Length that must read</param>
        /// <returns>MemoryStream readed from FileStream</returns>
        public MemoryStream ReadData(int Length)
        {
            MemoryStream ms;
            byte[] Buf = new byte[Length];
            base.Read(Buf, 0, Length);
            ms = new MemoryStream();
            ms.Write(Buf, 0, Length);

            return ms;
        }

        /// <summary>
        /// Indicate if file contain ID3v2 information
        /// </summary>
        /// <returns>true if contain otherwise false</returns>
        public bool HaveID3v2()
        {
            /* if the first three characters in begining of a file
             * be "ID3". that mpeg file contain ID3v2 information
             */
            string Iden = ReadText(3, TextEncodings.Ascii);
            if (Iden == "ID3")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Indicate if current File have ID3v1
        /// </summary>
        public bool HaveID3v1()
        {
            base.Seek(-128, SeekOrigin.End);
            string Tag = ReadText(3, TextEncodings.Ascii);
            if (Tag == "TAG")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Read ID3 version from current file
        /// </summary>
        /// <returns>Version contain ID3v2 version</returns>
        public Version ReadVersion()
        {
            return new Version("2." + ReadByte().ToString() + "." +
                ReadByte().ToString());
        }

        /// <summary>
        /// Read ID3 Size
        /// </summary>
        /// <returns>ID3 Length size</returns>
        public int ReadSize()
        {
            /* ID3 Size is like:
             * 0XXXXXXXb 0XXXXXXXb 0XXXXXXXb 0XXXXXXXb (b means binary)
             * the zero bytes must ignore, so we have 28 bits number = 0x1000 0000 (maximum)
             * it's equal to 256MB
             */
            int RInt;
            RInt = ReadByte() * 0x200000;
            RInt += ReadByte() * 0x4000;
            RInt += ReadByte() * 0x80;
            RInt += ReadByte();
            return RInt;
        }

        public static Encoding GetEncoding(TextEncodings TEncoding)
        {
            switch (TEncoding)
            {
                case TextEncodings.UTF_16:
                    return Encoding.Unicode;
                case TextEncodings.UTF_16BE:
                    return Encoding.GetEncoding("UTF-16BE");
                case TextEncodings.UTF8:
                    return Encoding.UTF8;
                default:
                    return Encoding.Default;
            }
        }
    }
}
