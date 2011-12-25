using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class PopulateContext : TaskCommandBase
    {
        public PopulateContext(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("populate", "", "", logger, settings, taskFactory)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return true;
        }

        public override void DoExecute(IList<string> args)
        {
            var populateContextTask = TaskFactory.CreatePopulateContextTask(Settings.DefaultConnection);
            populateContextTask.PopulateContext();
        }
    }
}