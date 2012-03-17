using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Mappings
{
    public class FjosMap : DomainObjectMap<Fjos>
    {
        public FjosMap()
        {
            HasMany(f => f.Kuer).Cascade.All();
        }
    }
}