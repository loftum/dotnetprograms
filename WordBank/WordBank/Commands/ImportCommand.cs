using System.Collections.Generic;
using System.Linq;
using WordBank.Lib.Tasks;
using Wordbank.Lib.Config;
using Wordbank.Lib.ExtensionMethods;
using Wordbank.Lib.Logging;

namespace Wordbank.Commands
{
    public class ImportCommand : CommandBase
    {
        private readonly ITaskFactory _taskFactory;

        public ImportCommand(IWordBankLogger logger, IWordBankSettings settings, ITaskFactory taskFactory)
            : base("import", logger, settings)
        {
            _taskFactory = taskFactory;
        }

        protected override void DoExecute(CommandArgs args)
        {
            var task = _taskFactory.CreateImportTask();
            if (args.Arguments.Count == 1)
            {
                task.ImportParadigme(args.Arguments[0]);
            }
            else
            {
                task.Import(args.Arguments[0].ToFullPath(), args.Arguments[1].ToFullPath());    
            }
        }

        protected override bool IsValid(CommandArgs args)
        {
            return args.Arguments.Count >= 1 && args.Arguments.All(f => f.ToFullPath().Exists());
        }

        public override IEnumerable<string> GetUsages()
        {
            return string.Format("{0} paradigmeFile fullFormFile", Name).AsArray();
        }

        protected override IEnumerable<string> GetExamples()
        {
            return "".AsArray();
        }
    }
}