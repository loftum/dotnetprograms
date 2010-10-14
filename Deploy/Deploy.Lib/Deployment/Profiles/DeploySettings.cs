using System;

namespace Deploy.Lib.Deployment.Profiles
{
    [Serializable]
    public class DeploySettings : Settings
    {
        public string Folder { get; set; }
    }
}