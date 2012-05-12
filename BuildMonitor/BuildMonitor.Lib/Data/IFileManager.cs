namespace BuildMonitor.Lib.Data
{
    public interface IFileManager
    {
        bool Exists(string filePath);
        string Read(string filePath);
        void Write(string filePath, string content);
    }
}