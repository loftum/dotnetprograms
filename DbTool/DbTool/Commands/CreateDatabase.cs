using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class CreateDatabase : TaskCommandBase
    {
        public CreateDatabase(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("create", "<databasename>", "MyDatabase", logger, settings, taskFactory)
        {
        }

        public override bool AreValid(CommandArgs args)
        {
            return args.HasArguments;
        }

        public override void DoExecute(CommandArgs args)
        {
            var databaseName = args.Arguments[0];
            var createTask = TaskFactory.CreateCreateDbTask(Settings.DefaultConnection);
            createTask.Create(databaseName);
        }
    }
}