using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Commands
{
    public class CommandArgs
    {
        public const string HelpFlag = "--help";

        public string Command { get; private set; }
        public IList<string> AllArguments { get; private set; }
        public IList<string> Arguments { get; private set; }
        public IList<string> Flags { get; private set; }
        public IList<string> DoubleFlags { get; private set; }
        public bool Help { get; private set; }

        public bool HasArguments{ get { return Arguments.Any(); } }
        public bool HasFlags{ get { return Flags.Any(); } }

        public CommandArgs(IEnumerable<string> args)
        {
            args.ShouldNotBeNullOrEmpty("args");
            Command = args.First();
            AllArguments = args.Skip(1).ToList();
            Arguments = AllArguments.Where(IsNotFlag).ToList();
            Flags = AllArguments.Where(IsSingleFlag).ToList();
            DoubleFlags = AllArguments.Where(IsDoubleFlag).ToList();
            Help = DoubleFlags.Any(f => f.EqualsIgnoreCase(HelpFlag));
        }

        private static bool IsNotFlag(string argument)
        {
            return !argument.StartsWith("-");
        }

        private static bool IsSingleFlag(string argument)
        {
            return argument.Matches(@"^[\-]{1}[^\-\s]+");
        }

        private static bool IsDoubleFlag(string argument)
        {
            return argument.Matches(@"^[\-]{2}[^\-\s]+");
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Command, string.Join(" ", Arguments));
        }
    }
}