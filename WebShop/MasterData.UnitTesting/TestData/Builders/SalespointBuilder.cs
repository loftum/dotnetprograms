using MasterData.Core.Domain.Stores;

namespace MasterData.UnitTesting.TestData.Builders
{
    public class SalespointBuilder : MasterDataObjectBuilderBase<Salespoint, SalespointBuilder>
    {
        public SalespointBuilder(bool generateId = true) : base(new Salespoint(), generateId)
        {
        }

        public SalespointBuilder ForReseller(Reseller reseller)
        {
            reseller.AddSalespoint(Item);
            return MySelf;
        }
    }
}