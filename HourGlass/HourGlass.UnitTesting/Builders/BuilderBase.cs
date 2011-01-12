using HourGlass.Lib.Domain;

namespace HourGlass.UnitTesting.Builders
{
    public abstract class BuilderBase<TBuilder, TItem>
        where TBuilder : BuilderBase<TBuilder, TItem> where TItem : DomainObject
    {
        private static int _id;

        public TItem Item{ get; protected set; }

        protected BuilderBase(TItem item)
        {
            Item = item;
        }

        protected TBuilder MySelf
        {
            get { return (TBuilder) this;}
        }

        public TBuilder WithId(long id)
        {
            Item.Id = id;
            return MySelf;
        }

        protected static int NextId()
        {
            return ++_id;
        }
    }
}