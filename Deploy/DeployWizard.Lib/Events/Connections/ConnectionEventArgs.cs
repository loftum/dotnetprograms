namespace DeployWizard.Lib.Events.Connections
{
    public class ConnectionEventArgs
    {
        public string ConnectionString { get; private set; }

        public ConnectionEventArgs(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}