namespace BuildMonitor.Lib.Model
{
    public class MonitorModel
    {
        public BuildServerModel BuildServer { get; set; }

        public bool CanBeDisplayed
        {
            get { return BuildServer.IsValid; }
        }

        public MonitorModel()
        {
            BuildServer = new BuildServerModel();
        }

        public MonitorModel(BuildServerModel buildServer)
        {
            BuildServer = buildServer;
        }
    }
}