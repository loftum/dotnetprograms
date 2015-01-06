using Ninject.Modules;
using Wordbank.Commands;

namespace WordBank.NinjectModules
{
    public class ConsoleModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandExecutor>().To<CommandExecutor>().InSingletonScope();
        }
    }
}