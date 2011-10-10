using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class DatabaseBackuper : TaskCommandBase
    {
        public DatabaseBackuper(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("backup", "<database>", "MyDatabase", logger, settings, taskFactory)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return args.Count > 1 && !string.IsNullOrWhiteSpace(args[1]);
        }

        public override void DoExecute(IList<string> args)
        {
            var connectionData = Settings.GetConnection(args[1]);

            var parameters = new BackupParameters
                                 {
                                     DatabaseName = connectionData.Name,
                                     FilePath = GetBackupPath(args),
                                     Server = connectionData.Host
                                 };

            var backupTask = TaskFactory.CreateBackupTask(connectionData);
            backupTask.PercentComplete += PrintPercentage;
            backupTask.Complete += TaskComplete;
            backupTask.Backup(parameters);
        }

        private static string GetBackupPath(IList<string> args)
        {
            return args.Count > 2 ? args[2] : string.Empty;
        }
    }
}