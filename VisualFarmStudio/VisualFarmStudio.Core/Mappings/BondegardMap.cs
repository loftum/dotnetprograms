using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Mappings
{
    public class BondegardMap : DomainObjectMap<Bondegard>
    {
        public BondegardMap()
        {
            References(g => g.Bonde);
            Map(g => g.Navn);
            HasMany(g => g.Fjoses).Cascade.All();
            HasMany(g => g.Stalls).Cascade.All();
            HasMany(g => g.Traktors).Cascade.All();
        }
    }
}