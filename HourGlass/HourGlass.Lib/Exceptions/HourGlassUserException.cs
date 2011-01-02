using System;
using HourGlass.Lib.Utilities;

namespace HourGlass.Lib.Exceptions
{
    public class HourGlassUserException : Exception
    {
        public HourGlassUserException(ExceptionType exceptionType, params object[] parameters) : 
            base(string.Format(EnumOperations.GetDescriptionOf(exceptionType), parameters))
        {
        }

        public HourGlassUserException(Exception innerException, ExceptionType exceptionType, params object[] parameters) :
            base(string.Format(EnumOperations.GetDescriptionOf(exceptionType), parameters), innerException)
        {
        }
    }
}