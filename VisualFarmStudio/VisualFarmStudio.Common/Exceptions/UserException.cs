using System;
using VisualFarmStudio.Common.ExtensionMethods;

namespace VisualFarmStudio.Common.Exceptions
{
    public class UserException : Exception
    {
        public ExceptionType Type { get; private set; }

        public UserException(ExceptionType type, params object[] args) : base(string.Format(type.GetDescription(), args))
        {
            Type = type;
        }
    }
}