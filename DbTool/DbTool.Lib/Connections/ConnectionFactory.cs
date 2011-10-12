using System.Data.Common;
using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Configuration;

namespace DbTool.Lib.Connections
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IAssemblyLoader _assemblyLoader;

        public ConnectionFactory(IAssemblyLoader assemblyLoader)
        {
            _assemblyLoader = assemblyLoader;
        }

        public DbConnection CreateConnection(ConnectionData connectionData)
        {
            return _assemblyLoader
                .GetAssemblyFor(connectionData.DatabaseType)
                .CreateInstance<IConnectionFactory>()
                .CreateConnection(connectionData);
        }
    }
}