using System;
using Deploy.Lib.Logging;

namespace Deploy.Lib.Deployment.Steps
{
    public abstract class DeploymentStepBase : IDeploymentStep
    {
        public string Name
        {
            get; private set;
        }

        protected readonly DeploymentStepStatus Status;
        protected readonly ILogger Logger;
        protected DeployParameters Parameters;

        protected DeploymentStepBase(DeployParameters parameters, string name, ILogger logger)
        {
            Parameters = parameters;
            Logger = logger;
            Status = new DeploymentStepStatus {StepName = name};
            Name = name;
        }

        public DeploymentStepStatus Execute()
        {
            try
            {
                return DoExecute();
            }
            catch (Exception e)
            {
                Status.Error = e.ToString();
                Status.Status = DeploymentStepStatus.Fail;
                Status.CanProceed = false;
                return Status;
            }
        }

        protected void SetStatusSkipped()
        {
            Status.Status = DeploymentStepStatus.Skipped;
            Status.CanProceed = true;
        }

        protected abstract DeploymentStepStatus DoExecute();
    }
}
