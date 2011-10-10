using DbTool.Commands;
using Ninject.Modules;

namespace DbTool.Modules
{
    public class CommandModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandProvider>().To<CommandProvider>().InSingletonScope();
        }
    }
}