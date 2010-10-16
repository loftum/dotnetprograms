using System;

namespace Deploy.Lib.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}