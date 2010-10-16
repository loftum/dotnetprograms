using System;
using Deploy.Lib.SummaryFormatting;

namespace Deploy.Lib.Deployment.Profiles
{
    [Serializable]
    public class WebConfigSettings : Settings
    {
        public string NewWebConfigPath { get; set; }

        public WebConfigSettings() : base("Web.config settings")
        {
        }

        protected override Summary SetValuesIn(Summary summary)
        {
            return summary.WithValue("New web.config path", NewWebConfigPath);
        }
    }
}