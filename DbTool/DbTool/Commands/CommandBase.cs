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
        protected IDbToolLogger Logger { get; private set; }
        protected IDbToolSettings Settings { get; private set; }
        private readonly PercentageLogger _percentageLogger;

        protected CommandBase(string name, IDbToolLogger logger, IDbToolSettings settings)
        {
            Name = name;
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
        protected abstract IEnumerable<string> GetUsages();
        protected abstract IEnumerable<string> GetExamples();

        public string GenerateUsageText()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Usage:");
            foreach (var usage in GetUsages())
            {
                builder.AppendFormat("{0} {1}", Name, usage).AppendLine();
            }
            builder.AppendLine("Example:");
            foreach (var example in GetExamples())
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