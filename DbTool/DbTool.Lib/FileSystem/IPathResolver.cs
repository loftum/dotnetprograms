namespace DbTool.Lib.FileSystem
{
    public interface IPathResolver
    {
        string GetFullPathOfExisting(string filename);
        string GetFullPathOf(string filename);
    }
}