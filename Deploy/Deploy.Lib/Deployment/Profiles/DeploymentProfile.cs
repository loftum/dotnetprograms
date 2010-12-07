using System;
using Deploy.Lib.SummaryFormatting;

namespace Deploy.Lib.Deployment.Profiles
{
    [Serializable]
    public class DeploymentProfile
    {
        public string Name { get; set; }
        public BackupSettings BackupSettings { get; set; }
        public DeployStatusSettings DeployStatusSettings { get; set; }
        public DestinationSettings DestinationSettings { get; set; }
        public WebConfigSettings WebConfigSettings { get; set; }
        public MigrateDatabaseSettings MigrateDatabaseSettings { get; set; }

        public Summary GetSummary(ISummaryFormatter formatter)
        {
            return new Summary(formatter)
                .WithTitle("Deployment profile")
                .WithValue("Name", Name)
                .With(BackupSettings.GetSummary(formatter))
                .With(DeployStatusSettings.GetSummary(formatter))
                .With(WebConfigSettings.GetSummary(formatter))
                .With(DestinationSettings.GetSummary(formatter));
        }
    }
}
