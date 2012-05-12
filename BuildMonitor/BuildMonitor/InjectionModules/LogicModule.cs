using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Api;
using BuildMonitor.Lib.Data;
using Ninject.Modules;

namespace BuildMonitor.InjectionModules
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMonitorFacade>().To<MonitorFacade>().InCurrentScope();
            Bind<IBuildServerFacade>().To<BuildServerFacade>().InCurrentScope();
            Bind<IBuildMonitorRepo>().To<BuildMonitorFileRepo>().InCurrentScope();
            Bind<IFileManager>().To<FileManager>().InCurrentScope();
        }
    }
}