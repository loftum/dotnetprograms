using System;
using System.Linq;
using System.Reflection;
using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.Logging;

namespace DbTool.Lib.Tasks
{
    public class TaskFactory : ITaskFactory
    {
        private IDbToolLogger _logger;
        private IDbToolConfig _config;
        private readonly IAssemblyLoader _assemblyLoader;

        public TaskFactory(IDbToolConfig config, IDbToolLogger logger, IAssemblyLoader assemblyLoader)
        {
            _config = config;
            _logger = logger;
            _assemblyLoader = assemblyLoader;
        }

        public IBackupTask CreateBackupTask(ConnectionData connection)
        {
            var expectedType = typeof (IBackupTask);
            var assembly = _assemblyLoader.GetAssemblyFor(connection.DatabaseType);
            var type = assembly.GetTypes().Where(t => typeof(IBackupTask).IsAssignableFrom(t)).FirstOrDefault();
            if (type == null)
            {
                throw new DbToolException("Could not find any {0} for databasetype {1} in assembly {2}",
                    expectedType.Name, connection.DatabaseType, assembly.GetName());
            }

            var instance = Activator.CreateInstance(type, )
        }

        public IRestoreTask CreateRestoreTask(ConnectionData connection)
        {
            throw new System.NotImplementedException();
        }
    }
}