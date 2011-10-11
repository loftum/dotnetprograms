using System.IO;
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
            var assemblyPath = GetAssemblyPathFor(databaseType);
            var assembly = Assembly.LoadFile(assemblyPath);
            return assembly;
        }

        private string GetAssemblyPathFor(string databaseType)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), _config.AssemblyMap[databaseType]);
        }
    }
}