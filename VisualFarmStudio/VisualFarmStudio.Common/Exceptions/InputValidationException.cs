using System;

namespace VisualFarmStudio.Common.Exceptions
{
    public class InputValidationException : Exception
    {
        public InputValidationException(string message) : base(message)
        {
        }
    }
}