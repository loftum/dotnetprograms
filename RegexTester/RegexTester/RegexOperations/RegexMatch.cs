using System.Text;

namespace RegexTester.RegexOperations
{
    public class RegexMatch
    {
        public int MatchNumber { get; private set; }
        public int GroupNumber { get; private set; }
        public string Value { get; private set; }
        public int StartPosition { get; private set; }
        public int Length { get; private set; }
        public int EndPosition { get { return StartPosition + Length; } }

        public RegexMatch(int matchNumber, int groupNumber, string value, int startPosition, int length)
        {
            MatchNumber = matchNumber;
            GroupNumber = groupNumber;
            Value = value;
            StartPosition = startPosition;
            Length = length;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendFormat("[M: {0} G: {1} Pos: {2} Len: {3}]: ", MatchNumber, GroupNumber, StartPosition, Length)
                .AppendFormat("'{0}'", Value)
                .ToString();
        }
    }
}