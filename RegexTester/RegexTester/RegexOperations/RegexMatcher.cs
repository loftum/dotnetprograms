using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexTester.RegexOperations
{
    public class RegexMatcher : IRegexMatcher
    {
        public RegexMatches Match(string pattern, string input)
        {
            var regex = new Regex(pattern);
            var matchCollection = regex.Matches(input);
            var regexMatches = new List<RegexMatch>();
            for (var ii = 0; ii < matchCollection.Count; ii++ )
            {
                var groups = matchCollection[ii].Groups;
                for (var jj=0; jj<groups.Count; jj++)
                {
                    var group = groups[jj];
                    regexMatches.Add(new RegexMatch(ii, jj, group.Value, group.Index, group.Length));
                }
            }

            return new RegexMatches(regexMatches);
        }
    }
}