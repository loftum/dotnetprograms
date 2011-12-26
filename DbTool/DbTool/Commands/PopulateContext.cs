using System.Linq;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class PopulateContext : TaskCommandBase
    {
        private const string Overwrite = "-f";

        public PopulateContext(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("populate", "[database|database2..] [-f (=overwrite existing)]", "MyDatabase -f", logger, settings, taskFactory)
        {
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