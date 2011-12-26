using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class ViewDatabaseVersion : TaskCommandBase
    {
        public ViewDatabaseVersion(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("version", "<database>", "MyDatabase", logger, settings, taskFactory)
        {
        }

        public override bool AreValid(CommandArgs args)
        {
            return args.HasArguments;
        }

        public override void DoExecute(CommandArgs args)
        {
            var databaseName = args.Arguments[0];
            var connectionData = Settings.GetConnection(databaseName);
            var viewVersionTask = TaskFactory.CreateViewDbVersionTask(connectionData);
            viewVersionTask.ViewVersion();
        }
    }
}