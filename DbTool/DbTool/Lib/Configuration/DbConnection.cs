namespace DbTool.Lib.Configuration
{
    public class DbConnection
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string ConnectionString { get; set; }
        public bool Default { get; set; }
    }
}