using System;
using System.IO;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.SqlServer.Tasks
{
    public class BackupTask : SqlServerProgressTaskBase, IBackupTask
    {
        public BackupTask(IDbToolLogger logger, IDbToolSettings settings)
            : base(logger, settings)
        {
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
                Logger.WriteLine("Backing up to {0}", backupPath);
                backup.Devices.AddDevice(backupPath, DeviceType.File);
                backup.BackupSetName = string.Format("{0} backup", parameters.DatabaseName);
                backup.PercentComplete += HandlePercentComplete;
                backup.Complete += HandleComplete;
                Logger.WriteLine("Running backup...");
                backup.SqlBackup(server);
            }
            finally
            {
                if (server.ConnectionContext.IsOpen)
                {
                    Logger.Write("Closing connection...");
                    server.ConnectionContext.Disconnect();
                    Logger.WriteLine("OK");
                }
            }
        }

        private string GenerateBackupPath(BackupParameters parameters)
        {
            if (parameters.FilePath.IsNotNullOrEmpty())
            {
                return Path.IsPathRooted(parameters.FilePath) ?
                    parameters.FilePath :
                    Path.Combine(Settings.BackupDirectory, parameters.FilePath);
            }
            var filename = string.Format("{0}_{1}.bak",
                parameters.DatabaseName,
                DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            return Path.Combine(Settings.BackupDirectory, filename);
        }
    }
}