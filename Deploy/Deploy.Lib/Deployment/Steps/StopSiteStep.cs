using Microsoft.Web.Administration;

namespace Deploy.Lib.Deployment.Steps
{
    public class StopSiteStep : DeploymentStepBase
    {
        public StopSiteStep(DeployParameters parameters)
            : base(parameters, "Stop site")
        {
        }

        protected override DeploymentStepStatus DoExecute()
        {
            var iisManager = new ServerManager();
            iisManager.Sites[""].Stop();
            Status.Status = DeploymentStepStatus.Ok;
            return Status;
        }
    }
}
