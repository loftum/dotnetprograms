using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Models
{
    public class BondegardViewModel
    {
        public Bondegard Bondegard { get; set; }

        public BondegardViewModel()
        {
        }

        public BondegardViewModel(Bondegard bondegard)
        {
            Bondegard = bondegard;
        }
    }
}