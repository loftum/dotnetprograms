using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class DropDatabase : TaskCommandBase
    {
        public DropDatabase(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("drop", "<database>", "MyDatabase", logger, settings, taskFactory)
        {
        }

        public override bool AreValid(CommandArgs args)
        {
            return args.HasArguments;
        }

        public override void DoExecute(CommandArgs args)
        {
            var databaseName = args.Arguments[0];
            var deleteDbTask = TaskFactory.CreateDeleteDbTask(Settings.DefaultConnection);
            deleteDbTask.Delete(databaseName);
        }
    }
}