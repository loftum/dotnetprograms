using MasterData.Core.Domain.Stores;

namespace MasterData.Core.Domain.Mappings
{
    public class ResellerMap : MasterDataObjectMap<Reseller>
    {
        public ResellerMap()
        {
            Map(r => r.Name);
            HasManyToMany(r => r.Suppliers).Table("ResellerToSupplier");
            HasMany(r => r.StoreProducts).Cascade.AllDeleteOrphan().Inverse();
            HasMany(r => r.Salespoints).Cascade.AllDeleteOrphan().Inverse();
        }
    }
}