using Ninject;
using VisualFarmStudio.Common.Configuration;
using VisualFarmStudio.Core.Repository;
using VisualFarmStudio.Lib.Caching;
using VisualFarmStudio.Lib.Facades;

namespace VisualFarmStudio.Lib.Interactive
{
    public class InteractiveStuff
    {
        public static StandardKernel Kernel { get; set; }
        
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        public static IVisualFarmRepo Repo { get { return Get<IVisualFarmRepo>(); } }
        public static IVFSConfig Config { get { return Get<IVFSConfig>(); } }
        public static IBondegardFacade BondegardFacade { get { return Get<IBondegardFacade>(); } }
        public static ICache Cache { get { return Get<ICache>(); } }
        public static ICacheManager CacheManager { get { return Get<ICacheManager>(); } }
    }
}