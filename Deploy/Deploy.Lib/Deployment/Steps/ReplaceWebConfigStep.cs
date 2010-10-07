using System.IO;

namespace Deploy.Lib.Deployment.Steps
{
    public class ReplaceWebConfigStep : DeploymentStepBase
    {
        private readonly DeploymentStepStatus _status;
        public ReplaceWebConfigStep(DeployParameters parameters)
            : base(parameters, "Replace web.config")
        {
            _status = new DeploymentStepStatus();
        }

        public override DeploymentStepStatus Execute()
        {
            File.Copy(Parameters.NewWebConfigPath, Parameters.WebConfigPath, true);
            _status.AppendCommentLine("Copying " + Parameters.NewWebConfigPath + " to " + Parameters.WebConfigPath);
            _status.Status = DeploymentStepStatus.Ok;
            return _status;
        }
    }
}
