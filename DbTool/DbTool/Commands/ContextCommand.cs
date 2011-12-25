using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Logging;

namespace DbTool.Commands
{
    public class ContextCommand : CommandBase
    {
        private const string Delete = "-d";
        private const string Add = "-a";

        public ContextCommand(IDbToolLogger logger, IDbToolSettings settings)
            : base("context", "[name]", "MyContext", logger, settings)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return true;
        }

        public override void DoExecute(IList<string> args)
        {
            switch (args.Count)
            {
                case 1:
                    PrintContexts();
                    break;
                case 2:
                    SwitchContextTo(args[1]);
                    break;
                default:
                    HandleContext(args);
                    break;
            }
        }

        private void HandleContext(IList<string> args)
        {
            var flag = args[1];
            switch(flag)
            {
                case Add:
                    CreateContext(args[2]);
                    break;
                case Delete:
                    DeleteContext(args[2]);
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