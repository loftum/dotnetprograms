using System;

namespace Wordbank.Lib.Exceptions
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {
        }

        public UserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}