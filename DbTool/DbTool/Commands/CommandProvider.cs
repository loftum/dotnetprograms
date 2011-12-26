using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class CommandProvider : ICommandProvider
    {
        private readonly IDictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

        private readonly IDbToolLogger _logger;
        private readonly IDbToolSettings _settings;
        private readonly ITaskFactory _taskFactory;
        private readonly PrintUsageCommand _printUsageCommand;

        public CommandProvider(IDbToolConfig config, IDbToolLogger logger, ITaskFactory taskFactory)
        {
            _logger = logger;
            _settings = config.Settings;
            _taskFactory = taskFactory;
            Add(new RestoreDatabase(_logger, _settings, _taskFactory),
                new DeleteDatabase(_logger, _settings, _taskFactory),
                new BackupDatabase(_logger, _settings, _taskFactory),
                new CreateDatabase(_logger, _settings, _taskFactory),
                new ListeDatabases(_logger, _settings, _taskFactory),
                new PopulateContext(_logger, _settings, _taskFactory),
                new MigrateDatabase(_logger, _settings, _taskFactory),
                new ViewDatabaseVersion(_logger, _settings, _taskFactory),
                new ViewDbToolVersion(_logger, _settings),
                new ContextCommand(_logger, _settings),
                new SetSettings(_logger, _settings));
            _printUsageCommand = new PrintUsageCommand(_logger, _commands);
        }

        private void Add(params ICommand[] commands)
        {
            foreach (var task in commands)
            {
                _commands.Add(task.Name, task);    
            }
        }

        public ICommand GetCommand(IList<string> args)
        {
            if (args.Count == 0 || !_commands.ContainsKey(args[0]))
            {
                return _printUsageCommand;
            }
            return _commands[args[0]];
        }
    }
}