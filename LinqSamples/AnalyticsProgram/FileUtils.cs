using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AnalyticsProgram
{
    public static class FileUtils
    {
        public static void WriteToFile(string path,string text)
        {
            if (!File.Exists(path))
            {
                using var stream = File.Create(path);
                byte[] info = new UTF8Encoding(true).GetBytes($"{text}\n");
                stream.Write(info, 0, info.Length);
            }
            else
            {
                File.AppendAllText(path, text);
                File.AppendAllText(path, Environment.NewLine);
            }
        }
    }
}
