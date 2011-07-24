using Ninject.Modules;
using StuffLibrary.Common.Configuration;

namespace StuffLibrary.IntegrationTesting.NinjectModules
{
    public class ConfigSingletonModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IStuffLibraryConfig>().To<StuffLibraryConfig>().InSingletonScope();
        }
    }
}