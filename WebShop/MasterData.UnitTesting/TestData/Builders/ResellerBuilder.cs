using MasterData.Core.Domain.Stores;

namespace MasterData.UnitTesting.TestData.Builders
{
    public class ResellerBuilder : MasterDataObjectBuilderBase<Reseller, ResellerBuilder>
    {
        public ResellerBuilder(bool generateId = true) : base(new Reseller(), generateId)
        {
        }

        public ResellerBuilder WithSupplier(Supplier supplier)
        {
            Item.AddSupplier(supplier);
            return MySelf;
        }
    }
}