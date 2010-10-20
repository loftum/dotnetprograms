using System;
using System.Text.RegularExpressions;

namespace RegexTester.RegexOperations
{
    public class RegexValidator : IRegexValidator
    {
        public void Validate(string patternCandidate)
        {
            try
            {
                new Regex(patternCandidate).IsMatch(string.Empty);
            }
            catch (ArgumentException e)
            {
                throw new RegexException(e.Message, e);
            }
        }



        public bool IsValid(string patternCandidate)
        {
            try
            {
                Validate(patternCandidate);
                return true;
            }
            catch (RegexException)
            {
                return false;
            }
        }

        public string ValidationMessageFor(string patternCandidate)
        {
            try
            {
                Validate(patternCandidate);
                return "Pattern is ok";
            }
            catch (RegexException e)
            {
                return e.Message;
            }
        }
    }
}
