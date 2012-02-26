using Ninject.Modules;
using StuffLibrary.Common.Configuration;
using StuffLibrary.Common.Scoping;

namespace StuffLibrary.NinjectModules
{
    public class ConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IStuffLibraryConfig>().To<StuffLibraryConfig>().InCurrentInjectionScope();
        }
    }
}