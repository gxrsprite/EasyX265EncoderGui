using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LosslessAviRgbConverter
{
    public class FileUtility
    {
        /// <summary>
        /// 递归枚举所有文件
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static IEnumerable<String> GetFiles(string dir)
        {
            if (File.GetAttributes(dir).HasFlag(FileAttributes.Directory))
            {
                foreach (String file in System.IO.Directory.GetFiles(dir))
                {
                    yield return file;
                }
                foreach (string dir1 in System.IO.Directory.GetDirectories(dir))
                {
                    foreach (string ff in GetFiles(dir1))
                    {
                        yield return ff;
                    }
                }
            }
            yield return dir;

        }


        public static String MakeRelativePath(String fromPath, String toPath)
        {
            if (String.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
            if (String.IsNullOrEmpty(toPath)) throw new ArgumentNullException("toPath");

            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            String relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }
    }
}
