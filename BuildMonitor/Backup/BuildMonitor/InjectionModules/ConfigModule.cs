using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Configuration;
using Ninject.Modules;

namespace BuildMonitor.InjectionModules
{
    public class ConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBuildMonitorSettings>().To<BuildMonitorSettings>().InCurrentScope<BuildMonitorSettings>();
        }
    }
}