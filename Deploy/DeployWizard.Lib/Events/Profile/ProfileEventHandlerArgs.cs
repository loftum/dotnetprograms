namespace DeployWizard.Lib.Events.Profile
{
    public class ProfileEventHandlerArgs
    {
        public string ProfileName { get; private set; }

        public ProfileEventHandlerArgs(string profileName)
        {
            ProfileName = profileName;
        }
    }
}
