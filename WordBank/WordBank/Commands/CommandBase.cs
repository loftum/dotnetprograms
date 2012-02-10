using System.Collections.Generic;
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
                throw new UserException(string.Format("Invalid arguments: {0}", string.Join(",", args.Arguments)));
            }
            DoExecute(args);
        }

        protected abstract void DoExecute(CommandArgs args);
        protected abstract bool IsValid(CommandArgs args);
        protected abstract IEnumerable<string> GetUsages();
        protected abstract IEnumerable<string> GetExamples();
    }
}