using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Logging;

namespace DbTool.Commands
{
    public class ContextCommand : CommandBase
    {
        private const string AddFlag = "-a";
        private const string DeleteFlag = "-d";

        public ContextCommand(IDbToolLogger logger, IDbToolSettings settings)
            : base("context", logger, settings)
        {
        }

        protected override IEnumerable<string> GetUsages()
        {
            return string.Format("[context] [{0}] [{1}]", AddFlag, DeleteFlag).AsArray();
        }

        protected override IEnumerable<string> GetExamples()
        {
            return new []
                {
                    "lists contexts",
                    "mycontext : switches to mycontext",
                    "-a mycontext : adds mycontext",
                    "-d mycontext : removes mycontext"
                };
        }

        public override bool AreValid(CommandArgs args)
        {
            return !(args.HasFlags && !args.HasArguments);
        }

        public override void DoExecute(CommandArgs args)
        {
            if (!args.HasArguments)
            {
                PrintContexts();
                return;
            }

            if (!args.HasFlags)
            {
                SwitchContextTo(args.Arguments[0]);
            }
            else
            {
                HandleContext(args);
            }
        }

        private void HandleContext(CommandArgs args)
        {
            var flag = args.Flags.First();
            switch(flag)
            {
                case AddFlag:
                    CreateContext(args.Arguments[0]);
                    break;
                case DeleteFlag:
                    DeleteContext(args.Arguments[0]);
                    break;
                default:
                    Logger.WriteLine("Unknown flag {0}", flag);
                    break;
            }
        }

        private void CreateContext(string contextName)
        {
            Settings.Addcontext(contextName);
            Logger.WriteLine("Added context {0}", contextName);
        }

        private void DeleteContext(string contextName)
        {
            Settings.DeleteContext(contextName);
            Logger.WriteLine("Deleted context {0}", contextName);
        }

        private void SwitchContextTo(string contextName)
        {
            Settings.SetCurrentContext(contextName);
            Logger.WriteLine("Switched to context {0}", Settings.CurrentContext.Name);
        }

        private void PrintContexts()
        {
            var currentName = Settings.CurrentContext.Name;
            Settings.Contexts.Each(context =>
                Logger.WriteLine(NameOf(context, currentName.Equals(context.Name))));
        }

        private static string NameOf(DbToolContext context, bool isCurrent)
        {
            return isCurrent 
                ? string.Format("* {0}", context.Name)
                : string.Format("  {0}", context.Name);
        }
    }
}