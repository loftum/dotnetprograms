using System.Text;

namespace RegexTester.RegexOperations
{
    public class RegexMatch
    {
        public string Value { get; private set; }
        public int StartPosition { get; private set; }
        public int Length { get; private set; }
        public int EndPosition { get { return StartPosition + Length; } }

        public RegexMatch(string value, int startPosition, int length)
        {
            Value = value;
            StartPosition = startPosition;
            Length = length;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(Value).Append(" ")
                .Append(StartPosition).Append(" ")
                .Append(Length)
                .ToString();
        }
    }
}