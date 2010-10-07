namespace Deploy.Lib.Deployment.Steps
{
    public abstract class DeploymentStepBase : IDeploymentStep
    {
        public string Name
        {
            get; private set;
        }

        protected DeployParameters Parameters;

        protected DeploymentStepBase(DeployParameters parameters, string name)
        {
            Parameters = parameters;
            Name = name;
        }

        public abstract DeploymentStepStatus Execute();
    }
}
