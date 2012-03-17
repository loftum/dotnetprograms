using System;
using VisualFarmStudio.Lib.Containers;

namespace VisualFarmStudio.Lib.Caching
{
    public class CacheManager : ICacheManager
    {
        private readonly ICache _cache;

        public CacheManager(ICache cache)
        {
            _cache = cache;
        }

        public BondegardContainer GetAllBondegards(Func<BondegardContainer> cacheMissFunc)
        {
            return _cache.Read("AllBondegards", cacheMissFunc);
        }
    }
}