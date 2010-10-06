using System;

namespace Deploy.Deployment
{
    public class InvalidParametersException : Exception
    {
        public InvalidParametersException(string message) : base(message)
        {
        }
    }
}
