using System;

namespace Deploy.Lib.Deployment.ProfileManagement
{
    public class DeploymentProfileException : Exception
    {
        public DeploymentProfileException(string message) : base(message)
        {
        }

        public DeploymentProfileException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
