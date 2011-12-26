using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class DeleteDatabase : TaskCommandBase
    {
        public DeleteDatabase(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("delete", "<database>", "MyDatabase", logger, settings, taskFactory)
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