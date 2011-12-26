using System;
using System.Collections.Generic;
using System.Text;
using DbTool.Lib.Logging;
using System.Linq;

namespace DbTool.Commands
{
    public class PrintUsageCommand : ICommand
    {
        public string Usage { get { return string.Empty; } }
        public string Example { get { return string.Empty; } }
        public string Name { get { return "usage"; } }

        private readonly IDbToolLogger _logger;
        private readonly IDictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

        public PrintUsageCommand(IDbToolLogger logger, IDictionary<string, ICommand> commands)
        {
            _logger = logger;
            _commands = commands;
        }

        public void Execute(IList<string> args)
        {
            _logger.WriteLine(PrintUsage(args));
        }

        private string PrintUsage(IList<string> args)
        {
            if (args.Count == 0 || !_commands.ContainsKey(args[0]))
            {
                return GenerateUsageText();
            }
            var taskName = args[0];
            return _commands[taskName].GenerateUsageText();
        }

        public string GenerateUsageText()
        {
            var commandNames = _commands.Keys.OrderBy(t => t);
            return new StringBuilder()
                .AppendLine("Supported commands: ")
                    .Append(string.Join(Environment.NewLine, commandNames))
                    .ToString();
        }
    }
}