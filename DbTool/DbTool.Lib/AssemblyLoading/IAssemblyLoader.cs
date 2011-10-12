namespace DbTool.Lib.AssemblyLoading
{
    public interface IAssemblyLoader
    {
        AssemblyHandler GetAssemblyFor(string databaseType);
    }
}