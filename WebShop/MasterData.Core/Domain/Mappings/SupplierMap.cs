using MasterData.Core.Domain.Stores;

namespace MasterData.Core.Domain.Mappings
{
    public class SupplierMap : MasterDataObjectMap<Supplier>
    {
        public SupplierMap()
        {
            Map(s => s.Name);
            HasMany(s => s.Products).Cascade.AllDeleteOrphan().Inverse();
            HasManyToMany(s => s.Resellers).Table("ResellerToSupplier").ReadOnly();
        }
    }
}