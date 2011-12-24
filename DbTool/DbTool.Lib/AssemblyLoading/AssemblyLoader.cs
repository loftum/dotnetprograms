using System.Reflection;
using DbTool.Lib.Configuration;
using DbTool.Lib.FileSystem;

namespace DbTool.Lib.AssemblyLoading
{
    public class AssemblyLoader : IAssemblyLoader
    {
        private readonly IDbToolConfig _config;
        private readonly IPathResolver _pathResolver;

        public AssemblyLoader(IDbToolConfig config, IPathResolver pathResolver)
        {
            _config = config;
            _pathResolver = pathResolver;
        }

        public AssemblyHandler GetAssemblyFor(string databaseType)
        {
            var assemblyName = _config.Settings.AssemblyMap[databaseType];
            var assemblyPath = _pathResolver.GetFullPathOfExisting(assemblyName);
            var assembly = Assembly.LoadFile(assemblyPath);
            return new AssemblyHandler(databaseType, assembly);
        }
    }
}