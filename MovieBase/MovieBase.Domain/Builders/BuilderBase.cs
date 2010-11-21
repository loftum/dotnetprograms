namespace MovieBase.Domain.Builders
{
    public abstract class BuilderBase<T> where T : DomainObject
    {
        public T Item { get; private set; }

        protected BuilderBase(T item)
        {
            Item = item;
        }
    }
}