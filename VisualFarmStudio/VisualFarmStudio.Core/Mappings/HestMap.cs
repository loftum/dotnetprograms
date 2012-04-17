using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Mappings
{
    public class HestMap : DyrMap<Hest>
    {
        public HestMap()
        {
            References(h => h.Stall);
        }
    }
}