using System;
using System.Collections.Generic;
using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Caching
{
    public interface ICacheManager
    {
        IEnumerable<Bondegard> GetAllBondegards(Func<IEnumerable<Bondegard>> cacheMissFunc);
    }
}