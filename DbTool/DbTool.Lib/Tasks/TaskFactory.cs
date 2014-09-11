using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.Logging;
using DotNetPrograms.Common.Validation;

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
            Guard.NotNull(() => connection);
            return CreateInstance<IBackupTask>(connection.DatabaseType);
        }

        public IRestoreTask CreateRestoreTask(ConnectionData connection)
        {
            Guard.NotNull(() => connection);
            return CreateInstance<IRestoreTask>(connection.DatabaseType);
        }

        public ICreateDbTask CreateCreateDbTask(ConnectionData connection)
        {
            Guard.NotNull(() => connection);
            return CreateInstance<ICreateDbTask>(connection.DatabaseType);
        }

        public IDeleteDbTask CreateDeleteDbTask(ConnectionData connection)
        {
            Guard.NotNull(() => connection);
            return CreateInstance<IDeleteDbTask>(connection.DatabaseType);
        }

        public IListDbTask CreateListDbTask(ConnectionData connection)
        {
            Guard.NotNull(() => connection);
            return CreateInstance<IListDbTask>(connection.DatabaseType);
        }

        public IPopulateContextTask CreatePopulateContextTask(ConnectionData connection)
        {
            Guard.NotNull(() => connection);
            return CreateInstance<IPopulateContextTask>(connection.DatabaseType);
        }

        private T CreateInstance<T>(string databaseType)
        {
            return _assemblyLoader.GetAssemblyFor(databaseType).CreateInstance<T>(_logger, _config.Settings);
        }
    }
}