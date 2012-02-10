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
            task.Import(args.Arguments[0].ToFullPath(), args.Arguments[1].ToFullPath());
        }

        protected override bool IsValid(CommandArgs args)
        {
            return args.Arguments.Count >= 2 && args.Arguments.All(f => f.ToFullPath().Exists());
        }

        protected override IEnumerable<string> GetUsages()
        {
            return "".AsArray();
        }

        protected override IEnumerable<string> GetExamples()
        {
            return "".AsArray();
        }
    }
}