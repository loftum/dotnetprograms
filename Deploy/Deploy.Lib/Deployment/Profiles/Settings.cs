using System;
using Deploy.Lib.SummaryFormatting;

namespace Deploy.Lib.Deployment.Profiles
{
    [Serializable]
    public abstract class Settings
    {
        public string Name { get; set; }
        public bool Skip { get; set; }

        protected Settings(string name)
        {
            Name = name;
        }

        public Summary GetSummary(ISummaryFormatter formatter)
        {
            var summary = new Summary(formatter).WithTitle(Name);
            if (Skip)
            {
                return summary.WithValue("Skipped", "Yes");
            }
            return SetValuesIn(summary);
        }

        protected abstract Summary SetValuesIn(Summary summary);
    }
}