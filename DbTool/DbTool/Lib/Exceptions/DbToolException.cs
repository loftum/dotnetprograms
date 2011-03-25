using System;

namespace DbTool.Lib.Exceptions
{
    public class DbToolException : Exception
    {
        public DbToolException(string message) : base(message)
        {
        }

        public DbToolException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}