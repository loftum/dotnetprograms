using System;
using StuffLibrary.Common.ExtensionMethods;

namespace StuffLibrary.Common.Exceptions
{
    public class UserException : Exception
    {
        public ExceptionType Type { get; private set; }

        public UserException(ExceptionType exceptionType)
            : base(exceptionType.GetDescription())
        {
            Type = exceptionType;
        }

        public UserException(ExceptionType exceptionType, Exception inner)
            : base(exceptionType.GetDescription(), inner)
        {
            Type = exceptionType;
        }
    }
}