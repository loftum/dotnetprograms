using VisualFarmStudio.Lib.Model;

namespace VisualFarmStudio.Models
{
    public class BondegardViewModel
    {
        public BondegardModel Bondegard { get; set; }

        public BondegardViewModel()
        {
        }

        public BondegardViewModel(BondegardModel bondegard)
        {
            Bondegard = bondegard;
        }
    }
}