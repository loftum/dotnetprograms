using System;
using System.Collections.Generic;
using VisualFarmStudio.Lib.Model;

namespace VisualFarmStudio.Lib.Caching
{
    public interface ICacheManager
    {
        IEnumerable<BondegardModel> GetAllBondegards(Func<IEnumerable<BondegardModel>> cacheMissFunc);
    }
}