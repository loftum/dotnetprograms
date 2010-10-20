using System;

namespace RegexTester.RegexOperations
{
    public class RegexException : Exception
    {
        public RegexException(string message) : base(message)
        {
        }

        public RegexException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}