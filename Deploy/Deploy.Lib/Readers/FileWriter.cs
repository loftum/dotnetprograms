using System.IO;

namespace Deploy.Lib.Readers
{
    public class FileWriter : IFileWriter
    {
        public IFileWriter Write(string text, string filePath)
        {
            using (var writer = new StreamWriter(filePath, false))
            {
                writer.Write(text);
                writer.Flush();
                writer.Close();
            }
            return this;
        }
    }
}
