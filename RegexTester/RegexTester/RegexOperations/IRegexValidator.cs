namespace RegexTester.RegexOperations
{
    public interface IRegexValidator
    {
        void Validate(string patternCandidate);
        bool IsValid(string patternCandidate);
        string ValidationMessageFor(string patternCandidate);
    }
}