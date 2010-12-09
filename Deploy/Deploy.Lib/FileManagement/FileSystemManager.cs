using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public IEnumerable<string> FileAndFolderNamesIn(string path, string searchPattern = null)
        {
            var directoryInfo = new DirectoryInfo(path);
            if (string.IsNullOrEmpty(searchPattern))
            {
                return directoryInfo.GetFileSystemInfos().Select(info => info.Name).ToList();
            }
            return directoryInfo.GetFileSystemInfos(searchPattern).Select(info => info.Name).ToList();
        }

        public IEnumerable<string> FilenamesIn(string path, string searchPattern = null)
        {
            var directoryInfo = new DirectoryInfo(path);
            if (string.IsNullOrEmpty(searchPattern))
            {
                return directoryInfo.GetFiles().Select(info => info.Name);
            }
            return directoryInfo.GetFiles(searchPattern).Select(info => info.Name);
        }

        public IEnumerable<string> FoldernamesIn(string path, string searchPattern = null)
        {
            var directoryInfo = new DirectoryInfo(path);
            if (string.IsNullOrEmpty(searchPattern))
            {
                return directoryInfo.GetDirectories().Select(info => info.Name);
            }
            return directoryInfo.GetDirectories(searchPattern).Select(info => info.Name);
        }

        public DirectoryInfo CreateNewDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        public FileInfo CreateNewFile(string path)
        {
            using (File.Create(path))
            {
            }
            return new FileInfo(path);
        }

        public FileInfo SearchForFile(string filename, string rootDir)
        {
            return SearchForFile(filename, new DirectoryInfo(rootDir));
        }

        private static FileInfo SearchForFile(string filename, DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles())
            {
                if (file.Name.Equals(filename))
                {
                    return file;
                }
            }
            foreach (var subDir in directory.GetDirectories())
            {
                var file = SearchForFile(filename, subDir);
                if (file != null)
                {
                    return file;
                }
            }
            return null;
        }

        public DirectoryInfo CreateTempDirectory()
        {
            var tempPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            return CreateNewDirectory(tempPath);
        }

        public DirectoryMover MoveContentsOf(DirectoryInfo directory)
        {
            return DirectoryMover.MoveContentsOf(directory);
        }

        public DirectoryCopier CopyContentsOf(DirectoryInfo directory)
        {
            return DirectoryCopier.CopyContentsOf(directory);
        }

        public void DeleteDirectory(DirectoryInfo directory)
        {
            directory.Delete(true);
        }

        public void DeleteFile(string filePath)
        {
            DeleteFile(new FileInfo(filePath));
        }

        public void DeleteFile(FileInfo fileInfo)
        {
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }
    }
}