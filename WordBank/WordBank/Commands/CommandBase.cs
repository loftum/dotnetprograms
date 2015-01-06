using System.Collections.Generic;
using System.Text;
using Wordbank.Lib.Config;
using Wordbank.Lib.Exceptions;
using Wordbank.Lib.Logging;

namespace Wordbank.Commands
{
    public abstract class CommandBase : ICommand
    {
        public string Name { get; private set; }
        protected IWordBankLogger Logger;
        protected IWordBankSettings Settings;

        protected CommandBase(string name, IWordBankLogger logger, IWordBankSettings settings)
        {
            Name = name;
            Logger = logger;
            Settings = settings;
        }

        public void Execute(CommandArgs args)
        {
            if (!IsValid(args))
            {
                throw new UserException(string.Format("Usage: {0}", GetUsageAndExamples()));
            }
            DoExecute(args);
        }

        public string GetUsageAndExamples()
        {
            var builder = new StringBuilder()
                .AppendLine("Usage:");
            foreach (var usage in GetUsages())
            {
                builder.AppendLine(usage);
            }
            builder.AppendLine("Examples:");
            foreach (var example in GetExamples())
            {
                builder.AppendLine(example);
            }
            return builder.ToString();
        }

        protected abstract void DoExecute(CommandArgs args);
        protected abstract bool IsValid(CommandArgs args);
        public abstract IEnumerable<string> GetUsages();
        protected abstract IEnumerable<string> GetExamples();
    }
}