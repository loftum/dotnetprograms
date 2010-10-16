using Deploy.Lib.Deployment.Profiles;

namespace DeployWizard.Lib.Models
{
    public class ProfileChangedEventArgs
    {
        public DeploymentProfile Profile { get; private set; }

        public ProfileChangedEventArgs(DeploymentProfile profile)
        {
            Profile = profile;
        }
    }
}