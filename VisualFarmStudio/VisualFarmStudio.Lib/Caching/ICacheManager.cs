using System;
using VisualFarmStudio.Lib.Containers;

namespace VisualFarmStudio.Lib.Caching
{
    public interface ICacheManager
    {
        BondegardContainer BondegardContainer { get; }
        BondegardContainer GetAllBondegards(Func<BondegardContainer> cacheMissFunc);
        void ClearAllBondegards();
    }
}