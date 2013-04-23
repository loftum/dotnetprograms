using BasicManifest.Core.Domain;

namespace BasicManifest.Data.Mappings
{
    public class SkydiverMap : DomainObjectMap<Skydiver>
    {
        public SkydiverMap()
        {
            Map(p => p.Role).CustomType<PersonRole>();
            Map(p => p.FirstName);
            Map(p => p.LastName);
            Map(p => p.BirthDate).Not.Nullable();
            HasMany(p => p.Slots).Cascade.None();
            References(p => p.Account).Cascade.All();
            References(p => p.Camp).Cascade.All();
        }
    }
}