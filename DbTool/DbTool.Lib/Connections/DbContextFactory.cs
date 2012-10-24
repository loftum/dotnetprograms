using System.Data.Common;
using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Communication.DbCommands;
using DbTool.Lib.Communication.DbCommands.CSharp;
using DbTool.Lib.Configuration;

namespace DbTool.Lib.Connections
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IDbToolConfig _config;
        private readonly IAssemblyLoader _assemblyLoader;
        private readonly ICSharpExecutor _cSharpExecutor;

        public DbContextFactory(IDbToolConfig config, IAssemblyLoader assemblyLoader, ICSharpExecutor cSharpExecutor)
        {
            _config = config;
            _assemblyLoader = assemblyLoader;
            _cSharpExecutor = cSharpExecutor;
        }

        public DbContext CreateDbContext(DbToolDatabase database)
        {
            var handler = _assemblyLoader.GetAssemblyFor(database.DatabaseType);
            var connection = handler.CreateInstance<IDbConnectionFactory>().CreateConnection(database.GetConnectionData());
            
            var executorProvider = CreateExecutorProvider(database, connection, handler);

            return new DbContext(connection, executorProvider);
        }

        private IExecutorProvider CreateExecutorProvider(DbToolDatabase database, DbConnection connection,
                                                         AssemblyHandler handler)
        {
            if (handler.HasType<IExecutorProvider>())
            {
                return handler.CreateInstance<IExecutorProvider>(_config, database, connection, _cSharpExecutor);
            }
            return new DefaultExecutorProvider(_config, database, connection, _cSharpExecutor);
        }
    }
}