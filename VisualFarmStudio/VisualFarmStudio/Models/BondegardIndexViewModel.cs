using System.Collections.Generic;
using System.Linq;
using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Models
{
    public class BondegardIndexViewModel
    {
        public IEnumerable<Bondegard> Bondegards { get; set; }

        public BondegardIndexViewModel()
        {
            Bondegards = Enumerable.Empty<Bondegard>();
        }

        public BondegardIndexViewModel(IEnumerable<Bondegard> bondegards)
        {
            Bondegards = bondegards;
        }
    }
}