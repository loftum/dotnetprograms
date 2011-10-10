using System.Reflection;
using DbTool.Lib.Configuration;

namespace DbTool.Lib.AssemblyLoading
{
    public class AssemblyLoader : IAssemblyLoader
    {
        private readonly IDbToolConfig _config;

        public AssemblyLoader(IDbToolConfig config)
        {
            _config = config;
        }

        public Assembly GetAssemblyFor(string databaseType)
        {
            var assemblyPath = _config.AssemblyMap[databaseType];
            var assembly = Assembly.LoadFile(assemblyPath);
            return assembly;
        }
    }
}