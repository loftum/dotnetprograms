using MasterData.Core.Domain.Products;
using MasterData.Core.Domain.Stores;

namespace MasterData.UnitTesting.TestData.Builders
{
    public class ProductMasterBuilder : MasterDataObjectBuilderBase<ProductMaster, ProductMasterBuilder>
    {
        public ProductMasterBuilder(bool generateId = true) : base(new ProductMaster(), generateId)
        {
        }

        public ProductMasterBuilder WithProducer(Producer producer)
        {
            producer.Add(Item);
            return MySelf;
        }
    }
}