namespace EnvironmentViewer.Lib.Services
{
    public class DatabaseState
    {
        private const string Unknown = "Unknown";

        public string Status { get; set; }
        public string Version { get; set; }

        public DatabaseState()
        {
            Status = Unknown;
            Version = Unknown;
        }
    }
}