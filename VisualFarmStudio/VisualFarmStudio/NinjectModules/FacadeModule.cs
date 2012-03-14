using Ninject.Modules;
using VisualFarmStudio.Lib.Caching;
using VisualFarmStudio.Lib.Configuration;
using VisualFarmStudio.Lib.Facades;
using VisualFarmStudio.Lib.Scoping;

namespace VisualFarmStudio.NinjectModules
{
    public class FacadeModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVFSConfig>().To<VFSConfig>().InSingletonScope();
            Bind<ICache>().To<HttpCache>().InScope(c => InjectionScope.Current);
            Bind<ICacheManager>().To<CacheManager>().InScope(c => InjectionScope.Current);
            Bind<IBondegardFacade>().To<BondegardFacade>().InScope(c => InjectionScope.Current);
        }
    }
}