using StuffLibrary.Domain;

namespace StuffLibrary.UnitTesting.Builders
{
    public class DomainBuilderBase<TBuilder, TItem> : BuilderBase<TItem>
        where TBuilder : DomainBuilderBase<TBuilder, TItem>
        where TItem : DomainObject, new()
    {
        protected TBuilder MySelf { get { return (TBuilder) this; } }

        protected DomainBuilderBase(TItem item)
        {
            Item = item;
        }

        protected static TItem ExistingItem()
        {
            return new TItem{Id = NextId()};
        }

        public TBuilder WithId(long id)
        {
            Item.Id = id;
            return MySelf;
        }

        
    }
}