using BasicManifest.Core.Domain;

namespace BasicManifest.Data.Mappings
{
    public class AccountMap : DomainObjectMap<Account>
    {
        public AccountMap()
        {
            Map(a => a.Balance);
            HasMany(a => a.Transactions).Cascade.AllDeleteOrphan();
        }
    }
}