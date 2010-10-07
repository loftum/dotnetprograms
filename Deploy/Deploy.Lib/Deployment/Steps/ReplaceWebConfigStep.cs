using System.IO;

namespace Deploy.Lib.Deployment.Steps
{
    public class ReplaceWebConfigStep : DeploymentStepBase
    {
        public ReplaceWebConfigStep(DeployParameters parameters)
            : base(parameters, "Replace web.config")
        {
        }

        protected override DeploymentStepStatus DoExecute()
        {
            File.Copy(Parameters.NewWebConfigPath, Parameters.WebConfigPath, true);
            Status.AppendDetailsLine("Copying " + Parameters.NewWebConfigPath + " to " + Parameters.WebConfigPath);
            Status.Status = DeploymentStepStatus.Ok;
            return Status;
        }
    }
}
