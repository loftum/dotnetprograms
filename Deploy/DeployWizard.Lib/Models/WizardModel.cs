using System;
using Deploy.Lib.Deployment.Profiles;
using Deploy.Lib.SummaryFormatting;

namespace DeployWizard.Lib.Models
{
    [Serializable]
    public class WizardModel
    {
        public event ProfileChangedEventHandler ProfileChanged;
        public delegate void ProfileChangedEventHandler(object sender, ProfileChangedEventArgs args);

        private DeploymentProfile _deploymentProfile;
        public string Package { get; set; }

        public DeploymentProfile CurrentProfile
        {
            get { return _deploymentProfile; }
            set 
            { 
                _deploymentProfile = value;
                ProfileChanged(this, new ProfileChangedEventArgs(_deploymentProfile));
            }
        }

        public string GetSummary(ISummaryFormatter formatter = null)
        {
            if (formatter == null)
            {
                formatter = new SummaryFormatter();
            }
            var summary = new Summary(formatter)
                .WithTitle("Deployment summary")
                .WithValue("Deployment package", Package)
                .With(_deploymentProfile.GetSummary(formatter));
            return summary.ToString();
        }
    }
}
