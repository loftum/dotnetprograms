using System;

namespace Deploy.Lib.Deployment
{
    public class DeploymentStepStatus
    {
        public const int Ok = 0;
        public const int Warning = 1;
        public const int Fail = 2;

        public bool CanProceed { get; private set; }
        public int Status { get; private set; }
        public Exception Exception { get; private set; }

        public DeploymentStepStatus(bool canProcess, int status)
            : this(canProcess, status, null)
        {
        }

        public DeploymentStepStatus(bool canProcess, int status, Exception exception)
        {
            CanProceed = canProcess;
            Status = status;
            Exception = exception;
        }

        public override string ToString()
        {
            switch(Status)
            {
                case Ok:
                    return "Done";
                case Warning:
                    return "Warning";
                case Fail:
                    return "Failed";
                default:
                    return "Ok";
            }
        }
    }
}
