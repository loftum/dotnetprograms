using System;

namespace MasterData.UnitTesting.TestData.Builders
{
    public abstract class BuilderBase<TItem, TBuilder> where TBuilder : BuilderBase<TItem, TBuilder>
    {
        public TItem Item { get; private set; }
        protected TBuilder MySelf { get { return (TBuilder) this; } }

        protected BuilderBase(TItem item)
        {
            Item = item;
        }

        public TBuilder With(Action<TItem> action)
        {
            action(Item);
            return MySelf;
        }

        public static implicit operator TItem(BuilderBase<TItem, TBuilder> builder)
        {
            return builder.Item;
        }
    }
}