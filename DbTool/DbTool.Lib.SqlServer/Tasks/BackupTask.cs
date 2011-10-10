using System;
using System.IO;
using System.Text;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.SqlServer.Tasks
{
    public class BackupTask : SqlServerProgressTaskBase, IBackupTask
    {
        private readonly IDbToolLogger _logger;
        private readonly IDbToolSettings _settings;

        public BackupTask(IDbToolLogger logger, IDbToolSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public void Backup(BackupParameters parameters)
        {
            var server = new Server(parameters.Server);
            try
            {
                server.ConnectionContext.LoginSecure = true;
                server.ConnectionContext.Connect();

                var backup = new Backup
                {
                    Action = BackupActionType.Database,
                    Database = parameters.DatabaseName
                };
                
                var backupPath = GenerateBackupPath(parameters);
                _logger.WriteLine("Backing up to {0}", backupPath);
                backup.Devices.AddDevice(backupPath, DeviceType.File);
                backup.BackupSetName = string.Format("{0} backup", parameters.DatabaseName);
                backup.PercentComplete += HandlePercentComplete;
                backup.Complete += HandleComplete;
                _logger.WriteLine("Running backup...");
                backup.SqlBackup(server);
            }
            finally
            {
                if (server.ConnectionContext.IsOpen)
                {
                    _logger.Write("Closing connection...");
                    server.ConnectionContext.Disconnect();
                    _logger.WriteLine("OK");
                }
            }
        }

        private string GenerateBackupPath(BackupParameters parameters)
        {
            if (parameters.FilePath.IsNotNullOrEmpty())
            {
                return Path.IsPathRooted(parameters.FilePath) ?
                    parameters.FilePath :
                    Path.Combine(_settings.BackupDirectory, parameters.FilePath);
            }
            var filename = string.Format("{0}_{1}.bak",
                parameters.DatabaseName,
                DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            return Path.Combine(_settings.BackupDirectory, filename);
        }
    }
}