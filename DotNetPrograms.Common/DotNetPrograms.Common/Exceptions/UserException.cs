using System;

namespace DotNetPrograms.Common.Exceptions
{
    public class UserException : Exception
    {
        public UserException(string format, params object[] args) : base(string.Format(format, args))
        {
        }
    }
}