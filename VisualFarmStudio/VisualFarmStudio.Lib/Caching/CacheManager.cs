using System;
using System.Collections.Generic;
using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Caching
{
    public class CacheManager : ICacheManager
    {
        private readonly ICache _cache;

        public CacheManager(ICache cache)
        {
            _cache = cache;
        }

        public IEnumerable<Bondegard> GetAllBondegards(Func<IEnumerable<Bondegard>> cacheMissFunc)
        {
            return _cache.Read("AllBondegards", cacheMissFunc);
        }
    }
}