using System;

namespace DeployWizard.Lib.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}