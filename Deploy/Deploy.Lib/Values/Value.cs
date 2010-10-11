namespace Deploy.Lib.Values
{
    public class Value<T>
    {
        protected readonly T TheValue;

        public Value(T value)
        {
            TheValue = value;
        }

        public override string ToString()
        {
            return TheValue.ToString();
        }
    }
}
