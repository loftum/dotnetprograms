using System;

namespace DotNetPrograms.Common.Mapping.Exceptions
{
    public class MappingException : Exception
    {
        public MappingException(string message) : base(message)
        {
        }

        public MappingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}