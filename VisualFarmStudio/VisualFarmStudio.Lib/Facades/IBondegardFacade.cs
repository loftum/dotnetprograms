using System.Collections.Generic;
using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Facades
{
    public interface IBondegardFacade
    {
        Bondegard GetDefaultBondegard();
        IEnumerable<Bondegard> GetAllBondegards();
        void Save(Bondegard bondegard);
    }
}