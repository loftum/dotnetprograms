using System;
using Deploy.Lib.SummaryFormatting;

namespace Deploy.Lib.Deployment.Profiles
{
    [Serializable]
    public class DeploySettings : Settings
    {
        public string Folder { get; set; }

        public DeploySettings() : base("Deploy settings")
        {
        }

        
        protected override Summary SetValuesIn(Summary summary)
        {
            return summary.WithValue("Folder", Folder);
        }
    }
}