using System.Collections.Generic;

namespace RegexTester.RegexOperations
{
    public class RegexModel
    {
        public string Input { get; set; }
        public string Pattern { get; set; }
        public IEnumerable<RegexMatch> Matches { get; set; }

        public RegexModel()
        {
            Matches = new RegexMatch[0];
            Input = string.Empty;
            Pattern = string.Empty;
        }
    }
}
