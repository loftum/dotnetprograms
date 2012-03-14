using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Mappings
{
    public class StallMap : DomainObjectMap<Stall>
    {
        public StallMap()
        {
            HasMany(s => s.Hester);
        }
    }
}