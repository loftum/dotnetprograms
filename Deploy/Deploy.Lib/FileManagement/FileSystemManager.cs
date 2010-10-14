using System.IO;

namespace Deploy.Lib.FileManagement
{
    public class FileSystemManager : IFileSystemManager
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}