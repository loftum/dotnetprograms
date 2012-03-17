using System.Collections.Generic;
using System.Linq;
using VisualFarmStudio.Lib.Model;

namespace VisualFarmStudio.Models
{
    public class BondegardIndexViewModel
    {
        public IEnumerable<BondegardModel> Bondegards { get; set; }

        public BondegardIndexViewModel()
        {
            Bondegards = Enumerable.Empty<BondegardModel>();
        }

        public BondegardIndexViewModel(IEnumerable<BondegardModel> bondegards)
        {
            Bondegards = bondegards;
        }
    }
}