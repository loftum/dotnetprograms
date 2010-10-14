using System;

namespace Deploy.Lib.Deployment.Profiles
{
    [Serializable]
    public class WebConfigSettings : Settings
    {
        public string NewWebConfigPath { get; set; }
        public string WebConfigPath { get; set; }
    }
}