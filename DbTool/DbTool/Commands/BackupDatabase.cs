using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbTool.Commands
{
    public class BackupDatabase : TaskCommandBase
    {
        public BackupDatabase(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("backup", logger, settings, taskFactory)
        {
        }

        protected override IEnumerable<string> GetUsages()
        {
            return "<database>".AsArray();
        }

        protected override IEnumerable<string> GetExamples()
        {
            return "MyDatabase".AsArray();
        }

        public override bool AreValid(CommandArgs args)
        {
            return args.Arguments.Count > 0 && !string.IsNullOrWhiteSpace(args.Arguments[0]);
        }

        public override void DoExecute(CommandArgs args)
        {
            var connectionData = Settings.GetConnection(args.Arguments[0]);

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

        private static string GetBackupPath(CommandArgs args)
        {
            return args.Arguments.Count > 1 ? args.Arguments[1] : string.Empty;
        }
    }
}