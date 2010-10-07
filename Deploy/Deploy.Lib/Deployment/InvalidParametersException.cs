using System;

namespace Deploy.Lib.Deployment
{
    public class InvalidParametersException : Exception
    {
        public InvalidParametersException(string message) : base(message)
        {
        }
    }
}
