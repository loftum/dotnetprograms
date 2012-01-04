using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class PopulateContext : TaskCommandBase
    {
        private const string Overwrite = "-f";

        public PopulateContext(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("populate", logger, settings, taskFactory)
        {
        }

        protected override IEnumerable<string> GetUsages()
        {
            return string.Format("[database|database2..] [{0} (=overwrite existing)]", Overwrite).AsArray();
        }

        protected override IEnumerable<string> GetExamples()
        {
            return string.Format("MyDatabase {0}", Overwrite).AsArray();
        }

        public override bool AreValid(CommandArgs args)
        {
            return true;
        }

        public override void DoExecute(CommandArgs args)
        {
            var populateContextTask = TaskFactory.CreatePopulateContextTask(Settings.DefaultConnection);
            var overwriteExisting = args.Flags.Contains(Overwrite);

            var databases = args.Arguments;
            if (databases.Any())
            {
                populateContextTask.Populate(databases, overwriteExisting);
            }
            else
            {
                populateContextTask.PopulateAll(overwriteExisting);    
            }
        }
    }
}