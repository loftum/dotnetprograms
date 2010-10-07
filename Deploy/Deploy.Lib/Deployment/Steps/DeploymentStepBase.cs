using System;

namespace Deploy.Lib.Deployment.Steps
{
    public abstract class DeploymentStepBase : IDeploymentStep
    {
        public string Name
        {
            get; private set;
        }

        protected readonly DeploymentStepStatus Status;

        protected DeployParameters Parameters;

        protected DeploymentStepBase(DeployParameters parameters, string name)
        {
            Parameters = parameters;
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

        protected abstract DeploymentStepStatus DoExecute();
    }
}
