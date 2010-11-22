namespace MovieBase.Common.Comparison
{
    public class StringValue : Value<string>
    {
        public StringValue(string value) : base(value)
        {
        }

        public static StringValue Of(string value)
        {
            return new StringValue(value);
        }

        public bool Like(string value)
        {
            return GetValue().ToLowerInvariant().Contains(value.ToLowerInvariant());
        }
    }
}