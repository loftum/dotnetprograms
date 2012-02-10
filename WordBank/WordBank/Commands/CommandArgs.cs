using System.Collections.Generic;
using System.Linq;

namespace Wordbank.Commands
{
    public class CommandArgs
    {
        public string Command { get; private set; }
        public IList<string> Arguments { get; private set; }

        public CommandArgs(IList<string> args)
        {
            Command = args[0].ToLowerInvariant();
            Arguments = new List<string>(args.Skip(1));
        }
    }
}