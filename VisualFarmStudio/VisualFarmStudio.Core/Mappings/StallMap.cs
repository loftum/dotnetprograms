using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Mappings
{
    public class StallMap : DomainObjectMap<Stall>
    {
        public StallMap()
        {
            HasMany(s => s.Hester);
        }
    }
}