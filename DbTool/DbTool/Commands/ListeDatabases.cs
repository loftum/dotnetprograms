using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class ListeDatabases : TaskCommandBase
    {
        public ListeDatabases(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("list", string.Empty, string.Empty, logger, settings, taskFactory)
        {
        }

        public override bool AreValid(CommandArgs args)
        {
            return true;
        }

        public override void DoExecute(CommandArgs args)
        {
            var listDbTask = TaskFactory.CreateListDbTask(Settings.DefaultConnection);
            listDbTask.ListDatabases();
        }
    }
}