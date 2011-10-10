using System.Reflection;

namespace DbTool.Lib.AssemblyLoading
{
    public interface IAssemblyLoader
    {
        Assembly GetAssemblyFor(string databaseType);
    }
}