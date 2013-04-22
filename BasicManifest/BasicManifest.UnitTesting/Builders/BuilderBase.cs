namespace BasicManifest.UnitTesting.Builders
{
    public abstract class BuilderBase<TBuilder, TItem> where TBuilder : BuilderBase<TBuilder, TItem>
    {
        public TItem Item { get; private set; }
        protected TBuilder MySelf { get { return (TBuilder) this; } }

        protected BuilderBase(TItem item)
        {
            Item = item;
        }

        public static implicit operator TItem(BuilderBase<TBuilder, TItem> builder)
        {
            return builder.Item;
        }
    }
}