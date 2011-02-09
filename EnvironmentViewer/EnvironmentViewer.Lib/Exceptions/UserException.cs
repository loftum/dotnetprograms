using System;
using EnvironmentViewer.Lib.Utilities;

namespace EnvironmentViewer.Lib.Exceptions
{
    public class UserException : Exception
    {
        public UserException(ExceptionType exceptionType, params object[] values)
            : base(Format(exceptionType, values))
        {
        }

        public UserException(Exception inner, ExceptionType exceptionType, params object[] values)
            : base(Format(exceptionType, values), inner)
        {
        }

        private static string Format(ExceptionType exceptionType, params object[] values)
        {
            return string.Format(EnumOperations.GetDescriptionOf(exceptionType), values);
        }
    }
}