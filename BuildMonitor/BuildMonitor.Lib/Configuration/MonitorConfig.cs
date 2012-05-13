namespace BuildMonitor.Lib.Configuration
{
    public class MonitorConfig
    {
        public BuildServerConfig BuildServerConfig { get; set; }

        public MonitorConfig()
        {
            BuildServerConfig = new BuildServerConfig();
        }
    }
}