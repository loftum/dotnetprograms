using System;
using VisualFarmStudio.Lib.Containers;

namespace VisualFarmStudio.Lib.Caching
{
    public class CacheManager : ICacheManager
    {
        private const string AllBondegardsKey = "AllBondegards";

        private readonly ICache _cache;

        public CacheManager(ICache cache)
        {
            _cache = cache;
        }

        public BondegardContainer BondegardContainer
        {
            get { return _cache.Read<BondegardContainer>(AllBondegardsKey); }
        }

        public BondegardContainer GetAllBondegards(Func<BondegardContainer> cacheMissFunc)
        {
            return _cache.Read(AllBondegardsKey, cacheMissFunc);
        }

        public void ClearAllBondegards()
        {
            _cache.Write(AllBondegardsKey, null);
        }
    }
}