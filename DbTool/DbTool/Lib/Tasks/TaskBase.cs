using System.Collections.Generic;
using System.Text;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.Tasks
{
    public abstract class TaskBase : ITask
    {
        public string Name { get; private set; }
        public string Usage { get; private set; }
        public string Example { get; private set; }
        protected IDbToolLogger Logger { get; private set; }
        protected IDbToolSettings Settings { get; private set; }
        private readonly PercentageLogger _percentageLogger;

        protected TaskBase(string name, string usage, string example,
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
            if (!AreValid(args))
            {
                Logger.WriteLine(GenerateUsageText());
                return;
            }
            DoExecute(args);
        }

        public abstract bool AreValid(IList<string> args);
        public abstract void DoExecute(IList<string> args);

        public string GenerateUsageText()
        {
            var builder = new StringBuilder()
                    .Append("Usage: ").Append(Name).Append(" ").AppendLine(Usage)
                    .Append("Example: ").Append(Name).Append(" ").AppendLine(Example);
            return builder.ToString();
        }

        protected void PrintPercentage(object sender, PercentCompleteEventArgs e)
        {
            _percentageLogger.Log(e.Percent);
        }

        protected void TaskComplete(object sender, ServerMessageEventArgs e)
        {
            Logger.WriteLine(Name + " OK");
        }
    }
}