using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class DatabaseVersionViewer : TaskCommandBase
    {
        public DatabaseVersionViewer(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("version", "<database>", "MyDatabase", logger, settings, taskFactory)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return args.Count > 1;
        }

        public override void DoExecute(IList<string> args)
        {
            var databaseName = args[1];
            var connectionData = Settings.GetConnection(databaseName);
            var viewVersionTask = TaskFactory.CreateViewDbVersionTask(connectionData);
            viewVersionTask.ViewVersion();
        }
    }
}