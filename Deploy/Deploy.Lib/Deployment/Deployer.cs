using System;
using System.Collections.Generic;
using Deploy.Lib.Deployment.Steps;

namespace Deploy.Lib.Deployment
{
    public class Deployer
    {
        private readonly DeployParameters _parameters;
        private readonly IList<IDeploymentStep> _steps = new List<IDeploymentStep>();

        public Deployer(DeployParameters parameters)
        {
            _parameters = parameters;
            _steps.Add(new BackupStep(_parameters));
            _steps.Add(new ClearDestinationFolderStep(_parameters));
            _steps.Add(new ExtractPackageStep(_parameters));
        }

        public void Deploy()
        {
            foreach (var deploymentStep in _steps)
            {
                Console.Write("Running " + deploymentStep.Name + "...");
                var status = deploymentStep.Execute();
                Console.WriteLine(status.ToString());
                if (!status.CanProceed)
                {
                    if (status.Exception != null)
                    {
                        Console.WriteLine(status.Exception.ToString());
                    }
                    break;
                }
            }
        }
    }
}
