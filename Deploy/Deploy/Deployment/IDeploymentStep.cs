namespace Deploy.Deployment
{
    public interface IDeploymentStep
    {
        string Name { get; }
        DeploymentStepStatus Execute();
    }
}
