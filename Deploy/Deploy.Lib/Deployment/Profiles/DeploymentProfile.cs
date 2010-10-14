using System;

namespace Deploy.Lib.Deployment.Profiles
{
    [Serializable]
    public class DeploymentProfile
    {
        public string Name { get; set; }
        public BackupSettings BackupSettings { get; set; }
        public DeployStatusSettings DeployStatusSettings { get; set; }
        public WebConfigSettings WebConfigSettings { get; set; }
    }
}
