using System.IO;

namespace BuildMonitor.Lib.Data
{
    public class FileManager : IFileManager
    {
        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public string Read(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public void Write(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }
    }
}