using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Deploy.Lib.DateAndTime;
using Deploy.Lib.Deployment.Steps;
using Deploy.Lib.FileManagement;
using Deploy.Lib.FilenameGenerating;
using Deploy.Lib.Logging;

namespace Deploy.Lib.Deployment
{
    public class Deployer
    {
        private const string Name = "Deployer";
        private readonly DeployParameters _parameters;
        private readonly IList<IDeploymentStep> _steps = new List<IDeploymentStep>();
        public IDeployLogger Logger { get; private set; }
        private readonly IAppender _consoleAppender = new ConsoleAppender();

        public Deployer(DeployParameters parameters)
        {
            _parameters = parameters;
            Logger = new DeployLogger();
            Logger.InfoMessageLogged += _consoleAppender.Append;
            AddSteps();
        }

        private void AddSteps()
        {
            var fileSystemManager = new FileSystemManager();
            _steps.Add(new ExtractPackageStep(_parameters, fileSystemManager, Logger));
            _steps.Add(new BackupStep(new DateProvider(), _parameters, Logger));
            _steps.Add(new ClearDestinationFolderStep(_parameters, Logger));
            _steps.Add(new DeployFilesStep(fileSystemManager, _parameters, Logger));
            _steps.Add(new MigrateDatabaseStep(_parameters, fileSystemManager, Logger));
            _steps.Add(new ReplaceWebConfigStep(_parameters, Logger));
            _steps.Add(new ResetFileAttributesStep(_parameters, Logger));
            _steps.Add(new CleanUpStep(fileSystemManager, _parameters, Logger));
        }

        public DeploymentStatus Deploy()
        {
            var numberOfStepsCompleted = 0;
            Logger.ReportProgress(numberOfStepsCompleted, _steps.Count);
            var deploymentStatus = new DeploymentStatus{Parameters = _parameters};
            foreach (var deploymentStep in _steps)
            {
                Logger.Info(Name, "Running " + deploymentStep.Name + "...");
                var status = deploymentStep.Execute();
                deploymentStatus.Add(status);
                Logger.Info(Name, status.ToString());
                if (!status.CanProceed)
                {
                    if (status.Error != null)
                    {
                        Logger.Info(Name, status.Error);
                    }
                    break;
                }
                numberOfStepsCompleted++;
                Logger.ReportProgress(numberOfStepsCompleted, _steps.Count);
            }
            Save(deploymentStatus);
            return deploymentStatus;
        }

        private void Save(DeploymentStatus status)
        {
            var statusFilePath = GenerateStatusFilePath();
            Logger.Info(Name, string.Format("Saving deployment status to {0}...", statusFilePath));
            var serializer = new XmlSerializer(typeof (DeploymentStatus));
            using (var stream = File.Create(statusFilePath))
            {
                serializer.Serialize(stream, status);
            }
            Logger.Info(Name, "Done");
        }

        private string GenerateStatusFilePath()
        {
            return new StringBuilder(_parameters.Profile.DeployStatusSettings.Folder)
                .Append(Path.DirectorySeparatorChar)
                .Append(new FilenameGenerator().BaseYyyyMmDdHhMmSsExtension("deploystatus", "xml"))
                .ToString();
        }
    }
}
