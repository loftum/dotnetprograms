using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Tasks
{
    public class DatabaseRestorer : TaskBase
    {
        public DatabaseRestorer(IDbToolLogger logger, IDbToolSettings settings)
            : base("restore", "<database> <filepath>", @"MyDatabase C:\mydatabase.bak", logger, settings)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return args.Count > 2 &&
                !string.IsNullOrWhiteSpace(args[1]) &&
                !string.IsNullOrWhiteSpace(args[2]);
        }

        public override void DoExecute(IList<string> args)
        {
            var connectionData = Settings.GetConnection(args[1]);
            var parameters = new BackupParameters
                {
                    DatabaseName = connectionData.Name,
                    FilePath = args[2],
                    Server = connectionData.Host
                };

            var restoreTask = new RestoreTask(Logger, Settings);
            restoreTask.PercentComplete += PrintPercentage;
            restoreTask.Complete += TaskComplete;
            restoreTask.Restore(parameters);
        }
    }
}