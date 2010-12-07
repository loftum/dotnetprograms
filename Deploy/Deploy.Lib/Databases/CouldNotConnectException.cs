using System;

namespace Deploy.Lib.Databases
{
    public class CouldNotConnectException : Exception
    {
        public CouldNotConnectException(string message) : base(message)
        {
        }

        public CouldNotConnectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}