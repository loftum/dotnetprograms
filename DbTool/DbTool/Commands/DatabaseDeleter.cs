using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class DatabaseDeleter : TaskCommandBase
    {
        public DatabaseDeleter(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("delete", "<database>", "MyDatabase", logger, settings, taskFactory)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return args.Count > 1 && !string.IsNullOrWhiteSpace(args[1]);
        }

        public override void DoExecute(IList<string> args)
        {
            var databaseName = args[1];
            var deleteDbTask = TaskFactory.CreateDeleteDbTask(Settings.DefaultConnection);
            deleteDbTask.Delete(databaseName);
        }
    }
}