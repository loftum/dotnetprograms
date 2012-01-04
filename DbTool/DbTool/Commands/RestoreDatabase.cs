using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class RestoreDatabase : TaskCommandBase
    {
        public RestoreDatabase(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("restore", logger, settings, taskFactory)
        {
        }

        protected override IEnumerable<string> GetUsages()
        {
            return "<database> <filepath>".AsArray();
        }

        protected override IEnumerable<string> GetExamples()
        {
            return @"MyDatabase C:\mydatabase.bak".AsArray();
        }

        public override bool AreValid(CommandArgs args)
        {
            return args.HasArguments && args.Arguments.Count > 1;
        }

        public override void DoExecute(CommandArgs args)
        {
            var connectionData = Settings.GetConnection(args.Arguments[0]);
            var parameters = new BackupParameters
                {
                    DatabaseName = connectionData.Name,
                    FilePath = args.Arguments[1],
                    Server = connectionData.Host
                };

            var restoreTask = TaskFactory.CreateRestoreTask(connectionData);
            restoreTask.PercentComplete += PrintPercentage;
            restoreTask.Complete += TaskComplete;
            restoreTask.Restore(parameters);
        }
    }
}