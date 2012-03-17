using System;
using System.Collections.Generic;
using VisualFarmStudio.Lib.Model;

namespace VisualFarmStudio.Lib.Caching
{
    public class CacheManager : ICacheManager
    {
        private readonly ICache _cache;

        public CacheManager(ICache cache)
        {
            _cache = cache;
        }

        public IEnumerable<BondegardModel> GetAllBondegards(Func<IEnumerable<BondegardModel>> cacheMissFunc)
        {
            return _cache.Read("AllBondegards", cacheMissFunc);
        }
    }
}