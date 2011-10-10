using DbTool.Lib.Logging;
using Ninject.Modules;

namespace DbTool.Modules
{
    public class LogModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbToolLogger>().To<ConsoleLogger>().InSingletonScope();
        }
    }
}