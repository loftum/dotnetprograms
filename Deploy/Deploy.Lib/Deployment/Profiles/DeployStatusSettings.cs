using System;

namespace Deploy.Lib.Deployment.Profiles
{
    [Serializable]
    public class DeployStatusSettings : Settings
    {
        public string Folder { get; set; }
    }
}