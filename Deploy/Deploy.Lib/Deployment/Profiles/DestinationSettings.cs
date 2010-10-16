using System;
using Deploy.Lib.SummaryFormatting;

namespace Deploy.Lib.Deployment.Profiles
{
    [Serializable]
    public class DestinationSettings : Settings
    {
        public String Folder { get; set; }

        public DestinationSettings() : base("Destination settings")
        {
        }

        protected override Summary SetValuesIn(Summary summary)
        {
            return summary.WithValue("Folder", Folder);
        }
    }
}