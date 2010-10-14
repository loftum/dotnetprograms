using System;

namespace DeployWizard.Lib.Steps
{
    public class WizardStepException : Exception
    {
        public WizardStepException(string message) : base(message)
        {
        }

        public WizardStepException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}