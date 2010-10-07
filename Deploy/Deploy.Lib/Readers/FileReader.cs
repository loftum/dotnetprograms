using System.Collections.Generic;
using System.IO;

namespace Deploy.Lib.Readers
{
    public class FileReader : IFileReader
    {
        public IEnumerable<string> ReadLines(string path)
        {
            var lines = new List<string>();
            using (var reader = new StreamReader(File.OpenRead(path)))
            {
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
            }
            return lines;
        }

        public string ReadAll(string path)
        {
            using(var reader = new StreamReader(File.OpenRead(path)))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
