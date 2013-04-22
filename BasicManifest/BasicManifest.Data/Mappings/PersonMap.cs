using BasicManifest.Core.Domain;

namespace BasicManifest.Data.Mappings
{
    public class PersonMap : DomainObjectMap<Person>
    {
        public PersonMap()
        {
            Map(p => p.Role).CustomType<PersonRole>();
            Map(p => p.FirstName);
            Map(p => p.LastName);
            Map(p => p.BirthDate);
            HasMany(p => p.Slots).Cascade.None();
            References(p => p.Account).Cascade.All();
        }
    }
}