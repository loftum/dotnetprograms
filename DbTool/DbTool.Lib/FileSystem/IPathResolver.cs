namespace DbTool.Lib.FileSystem
{
    public interface IPathResolver
    {
        string GetFullPathOf(string filename);
    }
}