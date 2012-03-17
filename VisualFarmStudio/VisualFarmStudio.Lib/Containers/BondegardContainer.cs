using System.Collections.Generic;
using VisualFarmStudio.Lib.Model;

namespace VisualFarmStudio.Lib.Containers
{
    public class BondegardContainer
    {
        public IList<BondegardModel> Bondegards { get; set; }

        public BondegardContainer(IList<BondegardModel> bondegarder)
        {
            Bondegards = bondegarder;
        }
    }
}