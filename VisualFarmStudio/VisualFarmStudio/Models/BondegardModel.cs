using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Models
{
    public class BondegardModel
    {
        public Bondegard Bondegard { get; set; }

        public BondegardModel()
        {
        }

        public BondegardModel(Bondegard bondegard)
        {
            Bondegard = bondegard;
        }
    }
}