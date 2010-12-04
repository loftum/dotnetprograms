using Deploy.Lib.SummaryFormatting;

namespace Deploy.Lib.Deployment.Profiles
{
    public class MigrateDatabaseSettings : Settings
    {
        public string ConnectionString { get; set; }
        public string MigrationAssemblyPath { get; set; }
        public string DatabaseType { get; set; }

        public MigrateDatabaseSettings() : base("Migrate database")
        {
        }

        protected override Summary SetValuesIn(Summary summary)
        {
            return summary;
        }
    }
}