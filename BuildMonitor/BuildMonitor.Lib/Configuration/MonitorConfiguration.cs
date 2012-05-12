namespace BuildMonitor.Lib.Configuration
{
    public class MonitorConfiguration
    {
        public BuildServerConfig BuildServerConfig { get; set; }

        public MonitorConfiguration()
        {
            BuildServerConfig = new BuildServerConfig();
        }
    }
}