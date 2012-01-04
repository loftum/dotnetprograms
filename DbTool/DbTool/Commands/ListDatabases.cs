using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class ListDatabases : TaskCommandBase
    {
        public ListDatabases(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("list", logger, settings, taskFactory)
        {
        }

        protected override IEnumerable<string> GetUsages()
        {
            return "[-a]".AsArray();
        }

        protected override IEnumerable<string> GetExamples()
        {
            return new[]
                {
                    ": lists databases in current context",
                    "-a: lists all databases on host"
                };
        }

        public override bool AreValid(CommandArgs args)
        {
            return true;
        }

        public override void DoExecute(CommandArgs args)
        {
            var listDbTask = TaskFactory.CreateListDbTask(Settings.DefaultConnection);
            var showAll = args.Flags.Contains("-a");
            listDbTask.ListDatabases(showAll);
        }
    }
}