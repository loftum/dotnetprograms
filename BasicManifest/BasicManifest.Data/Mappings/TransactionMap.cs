using BasicManifest.Core.Domain;

namespace BasicManifest.Data.Mappings
{
    public class TransactionMap : DomainObjectMap<Transaction>
    {
        public TransactionMap()
        {
            Map(t => t.Description);
            Map(t => t.Amount);
            Map(t => t.Type).CustomType<TransactionType>();
            References(t => t.Account).Not.Nullable();
        }
    }
}