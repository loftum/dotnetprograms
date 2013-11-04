namespace MasterData.UnitTesting.TestData.Builders
{
    public class Builder<T> : BuilderBase<T, Builder<T>>
    {
        public Builder(T item) : base(item)
        {
        }
    }
}