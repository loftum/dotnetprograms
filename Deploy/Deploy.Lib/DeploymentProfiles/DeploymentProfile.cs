namespace Deploy.Lib.DeploymentProfiles
{
    public class DeploymentProfile
    {
        public string Name { get; set; }
        public string PackagePath { get; set; }
        public string DestinationFolder { get; set; }
        public string BackupFolder { get; set; }
        public string NewWebConfigPath { get; set; }
        public string WebConfigPath { get; set; }
        public string DeployStatusPath { get; set; }
    }
}
