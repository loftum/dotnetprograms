using System;
using System.Collections.Generic;
using System.Linq;

namespace Generate.Commands
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IList<IGeneratorCommand> _commands;

        public CommandExecutor(IList<IGeneratorCommand> commands)
        {
            _commands = commands;
        }

        public string Execute(string name)
        {
            var command = _commands.FirstOrDefault(c => c.Name == name);
            return command == null ? Usage() : command.Generate();
        }

        private string Usage()
        {
            var commands = _commands.Select(c => c.Name);
            var usage = string.Format("Commands:\n{0}", string.Join(Environment.NewLine, commands));
            return usage;
        }
    }
}