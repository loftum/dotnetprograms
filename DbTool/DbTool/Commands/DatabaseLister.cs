using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class DatabaseLister : TaskCommandBase
    {
        public DatabaseLister(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("list", string.Empty, string.Empty, logger, settings, taskFactory)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return true;
        }

        public override void DoExecute(IList<string> args)
        {
            var listDbTask = TaskFactory.CreateListDbTask(Settings.DefaultConnection);
            listDbTask.ListDatabases();
        }
    }
}