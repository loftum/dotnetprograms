using System;
using System.Collections.Generic;
using WordBank.Lib.Tasks;
using Wordbank.Lib.Config;
using Wordbank.Lib.Logging;

namespace Wordbank.Commands
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IDictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();
        private readonly ICommand _printUsageCommand;
        private readonly IWordBankLogger _logger;
        private readonly IWordBankSettings _settings;
        private readonly ITaskFactory _taskFactory;

        public CommandExecutor(IWordBankLogger logger, IWordBankSettings settings, ITaskFactory taskFactory)
        {
            _logger = logger;
            _settings = settings;
            _taskFactory = taskFactory;
            AddCommand(new ImportCommand(_logger, _settings, _taskFactory));
            _printUsageCommand = new PrintUsageCommand(_commands.Values, _logger);
        }

        private void AddCommand(ICommand command)
        {
            _commands[command.Name.ToLowerInvariant()] = command;
        }

        public void Execute(string[] args)
        {
            var commandArgs = new CommandArgs(args);
            GetCommand(commandArgs.Command).Execute(commandArgs);
        }

        private ICommand GetCommand(string name)
        {
            try
            {
                return _commands[name];
            }
            catch (Exception)
            {
                return _printUsageCommand;
            }
        }
    }
}