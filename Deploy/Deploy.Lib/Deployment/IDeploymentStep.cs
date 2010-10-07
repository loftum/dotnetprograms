namespace Deploy.Lib.Deployment
{
    public interface IDeploymentStep
    {
        string Name { get; }
        DeploymentStepStatus Execute();
    }
}
