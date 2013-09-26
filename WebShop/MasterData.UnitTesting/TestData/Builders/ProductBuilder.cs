using MasterData.Core.Domain.MasterData;

namespace MasterData.UnitTesting.TestData.Builders
{
    public abstract class ProductBuilder<TProduct, TBuilder> : MasterDataObjectBuilderBase<TProduct, TBuilder>
        where TBuilder : ProductBuilder<TProduct, TBuilder>
        where TProduct : Product
    {
        protected ProductBuilder(TProduct item, bool generateId = true) : base(item, generateId)
        {
        }

        public TBuilder WithName(string name)
        {
            Item.Name = name;
            return MySelf;
        }

        public TBuilder WithDescription(string description)
        {
            Item.Description = description;
            return MySelf;
        }
    }
}