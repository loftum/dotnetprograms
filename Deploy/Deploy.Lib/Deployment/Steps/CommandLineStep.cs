using System.Diagnostics;

namespace Deploy.Lib.Deployment.Steps
{
    public class CommandLineStep : DeploymentStepBase
    {
        private readonly string _command;
        private readonly string _arguments;

        public CommandLineStep(DeployParameters parameters, string name, string command, string arguments)
            : base(parameters, name)
        {
            _command = command;
            _arguments = arguments;
        }

        protected override DeploymentStepStatus DoExecute()
        {
            var process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName = _command;
            process.StartInfo.Arguments = _arguments;
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            Status.AppendDetails(output);
            process.WaitForExit();
            Status.AppendDetails("Exit code:").AppendDetailsLine(process.ExitCode.ToString());
            Status.Status = DeploymentStepStatus.Ok;
            return Status;
        }
    }
}