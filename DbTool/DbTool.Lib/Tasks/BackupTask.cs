using System;
using System.IO;
using System.Text;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Logging;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.Tasks
{
    public class BackupTask
    {
        private readonly IDbToolLogger _logger;
        private readonly IDbToolSettings _settings;
        public event PercentCompleteEventHandler PercentComplete;
        public event ServerMessageEventHandler Complete;
        
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
                backup.BackupSetName = new StringBuilder().Append(parameters.DatabaseName).Append(" backup").ToString();
                backup.PercentComplete += PercentComplete;
                backup.Complete += Complete;
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
            var filename = new StringBuilder(parameters.DatabaseName).Append("_")
                .Append(DateTime.Now.ToString("yyyyMMdd_hhmmss"))
                .Append(".bak").ToString();
            return Path.Combine(_settings.BackupDirectory, filename);
        }
    }
}