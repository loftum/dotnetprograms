using System;
using System.Linq;
using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.Logging;

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
            return CreateInstance<IBackupTask>(connection.DatabaseType);
        }

        public IRestoreTask CreateRestoreTask(ConnectionData connection)
        {
            return CreateInstance<IRestoreTask>(connection.DatabaseType);
        }

        private T CreateInstance<T>(string databaseType)
        {
            var expectedType = typeof(T);
            var assembly = _assemblyLoader.GetAssemblyFor(databaseType);
            var type = assembly.GetTypes()
                .Where(t => typeof(IBackupTask).IsAssignableFrom(t) && !t.IsInterface)
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