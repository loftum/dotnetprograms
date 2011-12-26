using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class RestoreDatabase : TaskCommandBase
    {
        public RestoreDatabase(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("restore", "<database> <filepath>", @"MyDatabase C:\mydatabase.bak", logger, settings, taskFactory)
        {
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