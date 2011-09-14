using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using Microsoft.SqlServer.Management.Smo;
using Migrator.Framework.Loggers;

namespace DbTool.Lib.Tasks
{
    public class BackupTask
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
            var server = new Server("(local)");
            try
            {
                server.ConnectionContext.LoginSecure = true;
                server.ConnectionContext.Connect();

                var backup = new Backup
                {
                    Action = BackupActionType.Database,
                    Database = parameters.DatabaseName
                };
                var backupPath = GenerateBackupPath(args);
                _logger.WriteLine("Backing up to {0}", backupPath);
                backup.Devices.AddDevice(backupPath, DeviceType.File);
                backup.BackupSetName = new StringBuilder().Append(parameters.DatabaseName).Append(" backup").ToString();
                backup.PercentComplete += PrintPercentage;
                backup.Complete += TaskComplete;
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

        private string GenerateBackupPath(IList<string> args)
        {
            if (args.Count > 2)
            {
                var name = args[2];
                return Path.IsPathRooted(name) ?
                    name :
                    Path.Combine(_settings.BackupDirectory, name);
            }
            var databaseName = args[1];
            var filename = new StringBuilder(databaseName).Append("_")
                .Append(DateTime.Now.ToString("yyyyMMdd_hhmmss"))
                .Append(".bak").ToString();
            return Path.Combine(_settings.BackupDirectory, filename);
        }
    }
}