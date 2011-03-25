using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.Tasks
{
    public class DatabaseBackuper : TaskBase
    {
        public DatabaseBackuper(IDbToolLogger logger, IDbToolSettings settings)
            : base("backup", "<database>", "MyDatabase", logger, settings)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return args.Count > 1 && !string.IsNullOrWhiteSpace(args[1]);
        }

        public override void DoExecute(IList<string> args)
        {
            var databaseName = args[1];
            var server = new Server("(local)");
            try
            {
                server.ConnectionContext.LoginSecure = true;
                server.ConnectionContext.Connect();

                var backup = new Backup
                    {
                        Action = BackupActionType.Database,
                        Database = databaseName
                    };
                var backupPath = GenerateBackupPath(args);
                Logger.WriteLine("Backing up to {0}", backupPath);
                backup.Devices.AddDevice(backupPath, DeviceType.File);
                backup.BackupSetName = new StringBuilder().Append(databaseName).Append(" backup").ToString();
                backup.PercentComplete += PrintPercentage;
                backup.Complete += TaskComplete;
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

        private string GenerateBackupPath(IList<string> args)
        {
            if (args.Count > 2)
            {
                var name = args[2];
                return Path.IsPathRooted(name) ?
                    name :
                    Path.Combine(Settings.BackupDirectory, name);
            }
            var databaseName = args[1];
            var filename = new StringBuilder(databaseName).Append("_")
                .Append(DateTime.Now.ToString("yyyyMMdd_hhmmss"))
                .Append(".bak").ToString();
            return Path.Combine(Settings.BackupDirectory, filename);
        }
    }
}