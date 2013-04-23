using BasicManifest.Core.Domain;

namespace BasicManifest.UnitTesting.Builders
{
    public abstract class DomainBuilderBase<TBuilder, TItem> : BuilderBase<TBuilder, TItem>
        where TBuilder : DomainBuilderBase<TBuilder, TItem>
        where TItem : DomainObject
    {
        private static long _nextId;

        protected DomainBuilderBase(TItem item) : base(item)
        {
        }

        protected static TItem WithId(TItem item)
        {
            item.Id = NextId();
            return item;
        }

        private static long NextId()
        {
            return ++_nextId;
        }
    }
}