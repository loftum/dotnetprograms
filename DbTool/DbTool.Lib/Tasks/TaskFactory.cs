using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Logging;
using DbTool.Lib.Migrating;

namespace DbTool.Lib.Tasks
{
    public class TaskFactory : ITaskFactory
    {
        private readonly IDbToolLogger _logger;
        private readonly IDbToolConfig _config;
        private readonly IAssemblyLoader _assemblyLoader;

        public TaskFactory(IDbToolConfig config, IDbToolLogger logger, IAssemblyLoader assemblyLoader)
        {
            _config = config;
            _logger = logger;
            _assemblyLoader = assemblyLoader;
        }

        public IBackupTask CreateBackupTask(ConnectionData connection)
        {
            connection.ShouldNotBeNull("connection");
            return CreateInstance<IBackupTask>(connection.DatabaseType);
        }

        public IRestoreTask CreateRestoreTask(ConnectionData connection)
        {
            connection.ShouldNotBeNull("connection");
            return CreateInstance<IRestoreTask>(connection.DatabaseType);
        }

        public ICreateDbTask CreateCreateDbTask(ConnectionData connection)
        {
            connection.ShouldNotBeNull("connection");
            return CreateInstance<ICreateDbTask>(connection.DatabaseType);
        }

        public IDeleteDbTask CreateDeleteDbTask(ConnectionData connection)
        {
            connection.ShouldNotBeNull("connection");
            return CreateInstance<IDeleteDbTask>(connection.DatabaseType);
        }

        public IListDbTask CreateListDbTask(ConnectionData connection)
        {
            connection.ShouldNotBeNull("connection");
            return CreateInstance<IListDbTask>(connection.DatabaseType);
        }

        public IPopulateContextTask CreatePopulateContextTask(ConnectionData connection)
        {
            connection.ShouldNotBeNull("connection");
            return CreateInstance<IPopulateContextTask>(connection.DatabaseType);
        }

        public IMigrateDbTask CreateMigrateDbTask(DbToolDatabase database)
        {
            database.ShouldNotBeNull("database");
            return new MigrationRunner(database, _logger);
        }

        public IViewDbVersionTask CreateViewDbVersionTask(ConnectionData connection)
        {
            connection.ShouldNotBeNull("connection");
            if (!connection.HasConnectionString)
            {
                throw new DbToolException("No connection for {0} is defined.", connection.Name);
            }
            return new ViewDbVersionTask(connection, _logger);
        }

        private T CreateInstance<T>(string databaseType)
        {
            return _assemblyLoader.GetAssemblyFor(databaseType).CreateInstance<T>(_logger, _config.Settings);
        }
    }
}