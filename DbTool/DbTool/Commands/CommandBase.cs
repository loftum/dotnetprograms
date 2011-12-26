using System.Collections.Generic;
using System.Text;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public abstract class CommandBase : ICommand
    {
        public string Name { get; private set; }
        public string Usage { get; private set; }
        public string Example { get; private set; }
        protected IDbToolLogger Logger { get; private set; }
        protected IDbToolSettings Settings { get; private set; }
        private readonly PercentageLogger _percentageLogger;

        protected CommandBase(string name, string usage, string example,
            IDbToolLogger logger,
            IDbToolSettings settings)
        {
            Name = name;
            Usage = usage;
            Example = example;
            Logger = logger;
            Settings = settings;
            _percentageLogger = new PercentageLogger(Logger);
        }

        public void Execute(IList<string> args)
        {
            var commandArgs = new CommandArgs(args);
            if (!AreValid(commandArgs) || commandArgs.Help)
            {
                Logger.WriteLine(GenerateUsageText());
                return;
            }
            DoExecute(commandArgs);
        }

        public abstract bool AreValid(CommandArgs args);
        public abstract void DoExecute(CommandArgs args);

        public string GenerateUsageText()
        {
            return new StringBuilder()
                .AppendFormat("Usage: {0} {1}", Name, Usage).AppendLine()
                .AppendFormat("Example: {0} {1}", Name, Example).AppendLine()
                .ToString();
        }

        protected void PrintPercentage(object sender, TaskProgressEventArgs e)
        {
            _percentageLogger.Log(e.Percent);
        }

        protected void TaskComplete(object sender, TaskProgressCompleteEventArgs e)
        {
            Logger.WriteLine("{0} OK", Name);
        }
    }
}