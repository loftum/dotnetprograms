namespace DbTool.Lib.Configuration
{
    public class ConnectionData
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string ConnectionString { get; set; }
        public bool Default { get; set; }
        public string MigrationPath { get; set; }
    }
}