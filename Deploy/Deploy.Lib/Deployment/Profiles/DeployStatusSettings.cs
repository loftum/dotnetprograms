using System;
using Deploy.Lib.SummaryFormatting;

namespace Deploy.Lib.Deployment.Profiles
{
    [Serializable]
    public class DeployStatusSettings : Settings
    {
        public string Folder { get; set; }

        public DeployStatusSettings() : base("Deploy status settings")
        {
        }

        protected override Summary SetValuesIn(Summary summary)
        {
            return summary.WithValue("Folder", Folder);
        }
    }
}