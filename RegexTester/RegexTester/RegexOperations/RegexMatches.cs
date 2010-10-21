using System.Collections.Generic;
using System.Text;

namespace RegexTester.RegexOperations
{
    public class RegexMatches
    {
        public IEnumerable<RegexMatch> Matches { get; set; }

        public RegexMatches(IEnumerable<RegexMatch> matches)
        {
            Matches = matches;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach(var match in Matches)
            {
                builder.AppendLine(match.ToString());
            }
            return builder.ToString();
        }
    }
}