namespace MovieBase.Comparison.TextComparison
{
    public class Value<T>
    {
        private readonly T _value;

        public Value(T value)
        {
            _value = value;
        }

        protected T GetValue()
        {
            return _value;
        }
    }
}