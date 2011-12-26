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
        private readonly IEnumerable<string> _usages;
        private readonly IEnumerable<string> _examples;
        protected IDbToolLogger Logger { get; private set; }
        protected IDbToolSettings Settings { get; private set; }
        private readonly PercentageLogger _percentageLogger;

        protected CommandBase(string name, string usage, string example,
            IDbToolLogger logger,
            IDbToolSettings settings) : this(name, new[]{usage}, new[]{example}, logger, settings)
        {
        }

        protected CommandBase(string name, IEnumerable<string> usages, IEnumerable<string> examples, IDbToolLogger logger, IDbToolSettings settings)
        {
            Name = name;
            _usages = usages;
            _examples = examples;
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
            var builder = new StringBuilder();
            builder.AppendLine("Usage:");
            foreach (var usage in _usages)
            {
                builder.AppendFormat("{0} {1}", Name, usage).AppendLine();
            }
            builder.AppendLine("Example:");
            foreach (var example in _examples)
            {
                builder.AppendFormat("{0} {1}", Name, example).AppendLine();
            }
            return builder.ToString();
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