using System;
using Deploy.Lib.Deployment.Profiles;

namespace DeployWizard.Lib
{
    [Serializable]
    public class WizardModel
    {
        public event ProfileChangedEventHandler ProfileChanged;
        private DeploymentProfile _deploymentProfile;
        public string Package { get; set; }

        public DeploymentProfile CurrentProfile
        {
            get { return _deploymentProfile; }
            set 
            { 
                _deploymentProfile = value;
                ProfileChanged(this, new ProfileChangedEventHandlerArgs(_deploymentProfile));
            }
        }
    }

    public delegate void ProfileChangedEventHandler(object sender, ProfileChangedEventHandlerArgs args);

    public class ProfileChangedEventHandlerArgs
    {
        public DeploymentProfile Profile { get; private set; }

        public ProfileChangedEventHandlerArgs(DeploymentProfile profile)
        {
            Profile = profile;
        }
    }
}
