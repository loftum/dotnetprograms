using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Mappings
{
    public class BondeMap : DomainObjectMap<Bonde>
    {
        public BondeMap()
        {
            Map(b => b.Fornavn);
            Map(b => b.Etternavn);
            Map(b => b.Brukernavn);
            HasMany(b => b.Bondegards).Cascade.All();
            HasManyToMany(b => b.Rolles)
                .Cascade.All()
                .Table("BondeRolle");
        }
    }
}