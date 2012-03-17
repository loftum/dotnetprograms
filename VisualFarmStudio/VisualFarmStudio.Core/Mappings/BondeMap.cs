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
            HasMany(b => b.Bondegards);
            HasManyToMany(b => b.Rolles).Table("BondeRolle");
        }
    }
}