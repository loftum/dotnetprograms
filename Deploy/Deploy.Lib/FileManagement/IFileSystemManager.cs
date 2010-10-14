namespace Deploy.Lib.FileManagement
{
    public interface IFileSystemManager
    {
        bool FileExists(string path);
        bool DirectoryExists(string path);
    }
}