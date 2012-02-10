using System.Collections.Generic;
using Wordbank.Lib.Logging;

namespace Wordbank.Commands
{
    public class PrintUsageCommand : ICommand
    {
        public string Name
        {
            get { return "Usage"; }
        }

        private readonly IWordBankLogger _logger;
        private readonly IEnumerable<ICommand> _commands;

        public PrintUsageCommand(IEnumerable<ICommand> commands, IWordBankLogger logger)
        {
            _commands = commands;
            _logger = logger;
        }

        public void Execute(CommandArgs args)
        {
            foreach (var command in _commands)
            {
                

                
            }
        }
    }
}