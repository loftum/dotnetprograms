using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
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
            _steps.Add(new ReplaceWebConfigStep(_parameters));
        }

        public void Deploy()
        {
            var deploymentStatus = new DeploymentStatus{Parameters = _parameters};
            foreach (var deploymentStep in _steps)
            {
                Console.Write("Running " + deploymentStep.Name + "...");
                var status = deploymentStep.Execute();
                deploymentStatus.Add(status);
                Console.WriteLine(status.ToString());
                if (!status.CanProceed)
                {
                    if (status.Error != null)
                    {
                        Console.WriteLine(status.Error);
                    }
                    break;
                }
            }
            Save(deploymentStatus);
        }

        private void Save(DeploymentStatus status)
        {
            var statusFilePath = GenerateStatusFilePath();
            Console.Write("Saving deployment status to {0}...", statusFilePath);
            var serializer = new XmlSerializer(typeof (DeploymentStatus));
            using (var stream = File.Create(statusFilePath))
            {
                serializer.Serialize(stream, status);
            }
            Console.WriteLine("Done");
        }

        private string GenerateStatusFilePath()
        {
            var now = DateTime.Now;
            return new StringBuilder(_parameters.DeployStatusPath)
                .Append(Path.DirectorySeparatorChar)
                .Append("deploystatus_")
                .Append(now.ToShortDateString().Replace(".", string.Empty))
                .Append("_")
                .Append(now.ToShortTimeString().Replace(":", string.Empty))
                .Append(".xml")
                .ToString();
        }
    }
}
