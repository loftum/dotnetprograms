using System;

namespace Deploy.Lib.Deployment.Profiles
{
    [Serializable]
    public class BackupSettings : Settings
    {
        public string Folder { get; set; }
    }
}