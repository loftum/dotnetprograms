using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Mappings
{
    public class FjosMap : DomainObjectMap<Fjos>
    {
        public FjosMap()
        {
            HasMany(f => f.Kues).Cascade.All();
        }
    }
}