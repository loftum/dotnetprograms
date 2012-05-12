using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Api;
using Ninject.Modules;

namespace BuildMonitor.InjectionModules
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBuildFacade>().To<BuildFacade>().InCurrentScope();
        }
    }
}