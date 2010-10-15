using System.Collections.Generic;

namespace Deploy.Lib.FileManagement
{
    public interface IFileSystemManager
    {
        bool FileExists(string path);
        bool DirectoryExists(string path);
        IEnumerable<string> FileAndFolderNamesIn(string path, string searchPattern = null);
        IEnumerable<string> FilenamesIn(string path, string searchPattern = null);
        IEnumerable<string> FoldernamesIn(string path, string searchPattern = null);
    }
}