using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbTool.Commands
{
    public class CreateDatabase : TaskCommandBase
    {
        public CreateDatabase(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("create", logger, settings, taskFactory)
        {
        }

        protected override IEnumerable<string> GetUsages()
        {
            return "<databasename>".AsArray();
        }

        protected override IEnumerable<string> GetExamples()
        {
            return "MyDatabase".AsArray();
        }

        public override bool AreValid(CommandArgs args)
        {
            return args.HasArguments;
        }

        public override void DoExecute(CommandArgs args)
        {
            var databaseName = args.Arguments[0];
            var createTask = TaskFactory.CreateCreateDbTask(Settings.DefaultConnection);
            createTask.Create(databaseName);
        }
    }
}