using System.Data.Common;
using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Communication.Commands;
using DbTool.Lib.Configuration;

namespace DbTool.Lib.Connections
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IDbToolConfig _config;
        private readonly IAssemblyLoader _assemblyLoader;

        public DbContextFactory(IDbToolConfig config, IAssemblyLoader assemblyLoader)
        {
            _config = config;
            _assemblyLoader = assemblyLoader;
        }

        public DbContext CreateDbContext(ConnectionData connectionData)
        {
            var handler = _assemblyLoader.GetAssemblyFor(connectionData.DatabaseType);
            var connection = handler.CreateInstance<IDbConnectionFactory>().CreateConnection(connectionData);
            
            var executorProvider = CreateExecutorProvider(connectionData, connection, handler);

            return new DbContext(connection, executorProvider);
        }

        private IExecutorProvider CreateExecutorProvider(ConnectionData connectionData, DbConnection connection,
                                                         AssemblyHandler handler)
        {
            if (handler.HasType<IExecutorProvider>())
            {
                return handler.CreateInstance<IExecutorProvider>(_config, connectionData, connection);
            }
            return new DefaultExecutorProvider(_config, connectionData, connection);
        }
    }
}