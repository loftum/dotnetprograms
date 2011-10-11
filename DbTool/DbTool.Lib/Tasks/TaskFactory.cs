using System;
using System.Linq;
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

        public IMigrateDbTask CreateMigrateDbTask(ConnectionData connection)
        {
            connection.ShouldNotBeNull("connection");
            if (!connection.HasConnectionString)
            {
                throw new DbToolException("No connection for {0} is defined.", connection.Name);
            }
            return new MigrationRunner(connection, _logger);
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
            var expectedType = typeof(T);
            var assembly = _assemblyLoader.GetAssemblyFor(databaseType);
            var type = assembly.GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface)
                .FirstOrDefault();
            if (type == null)
            {
                throw new DbToolException("Could not find any {0} for databasetype {1} in assembly {2}",
                    expectedType.Name, databaseType, assembly.GetName());
            }

            return (T) Activator.CreateInstance(type, new object[] {_logger, _config.Settings});
        }
    }
}