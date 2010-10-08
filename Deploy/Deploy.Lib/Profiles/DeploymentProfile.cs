using System;

namespace Deploy.Lib.Profiles
{
    [Serializable]
    public class DeploymentProfile
    {
        public string Name { get; set; }
        public string PackageDirectory { get; set; }
        public string DestinationDirectory { get; set; }
        public string BackupDirectory { get; set; }
        public string NewWebConfigPath { get; set; }
        public string WebConfigPath { get; set; }
    }
}