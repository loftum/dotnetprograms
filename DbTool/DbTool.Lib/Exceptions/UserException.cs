using System;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Exceptions
{
    public class UserException : DbToolException
    {
        public ExceptionType Type { get; private set; }

        public UserException(Exception innerException, ExceptionType type, params object[] args) : base(type.GetDescription(args), innerException)
        {
            Type = type;
        }

        public UserException(ExceptionType type, params object[] args) : base(type.GetDescription(args))
        {
            Type = type;
        }
    }
}