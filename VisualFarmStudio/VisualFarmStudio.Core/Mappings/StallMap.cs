using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Mappings
{
    public class StallMap : DomainObjectMap<Stall>
    {
        public StallMap()
        {
            References(s => s.Bondegard);
            HasMany(s => s.Hestes).Cascade.All();
        }
    }
}