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
            var overwriteExisting = args.Contains(Overwrite);

            var databases = args.Where(a => !IsKeyword(a));
            if (databases.Any())
            {
                populateContextTask.Populate(databases, overwriteExisting);
            }
            else
            {
                populateContextTask.PopulateAll(overwriteExisting);    
            }
        }

        private bool IsKeyword(string word)
        {
            return word.In(Overwrite, Name);
        }
    }
}