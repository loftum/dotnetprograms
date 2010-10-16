using System;
using Deploy.Lib.SummaryFormatting;

namespace Deploy.Lib.Deployment.Profiles
{
    [Serializable]
    public class BackupSettings : Settings
    {
        public string Folder { get; set; }

        public BackupSettings() : base("Backup settings")
        {
        }

        protected override Summary SetValuesIn(Summary summary)
        {
            return summary.WithValue("Folder", Folder);
        }
    }
}