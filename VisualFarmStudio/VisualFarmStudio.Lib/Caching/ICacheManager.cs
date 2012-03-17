using System;
using VisualFarmStudio.Lib.Containers;

namespace VisualFarmStudio.Lib.Caching
{
    public interface ICacheManager
    {
        BondegardContainer GetAllBondegards(Func<BondegardContainer> cacheMissFunc);
    }
}