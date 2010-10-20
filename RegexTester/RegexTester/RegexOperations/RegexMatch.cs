using System.Text;

namespace RegexTester.RegexOperations
{
    public class RegexMatch
    {
        public string Value { get; private set; }
        public int Index { get; private set; }
        public int Length { get; private set; }

        public RegexMatch(string value, int index, int length)
        {
            Value = value;
            Index = index;
            Length = length;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(Value).Append(" ")
                .Append(Index).Append(" ")
                .Append(Length)
                .ToString();
        }
    }
}