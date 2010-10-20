namespace RegexTester.RegexOperations
{
    public interface IRegexMatcher
    {
        RegexMatches Match(string pattern, string input);
    }
}