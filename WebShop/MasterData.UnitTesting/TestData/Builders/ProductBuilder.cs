using MasterData.Core.Domain.Products;

namespace MasterData.UnitTesting.TestData.Builders
{
    public abstract class ProductBuilder<TProduct, TBuilder> : MasterDataObjectBuilderBase<TProduct, TBuilder>
        where TBuilder : ProductBuilder<TProduct, TBuilder>
        where TProduct : Product
    {
        protected ProductBuilder(TProduct item, bool generateId = true) : base(item, generateId)
        {
        }
    }
}