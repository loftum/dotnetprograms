namespace EnvironmentViewer.Lib.Services
{
    public class ApplicationState
    {
        private const string Unknown = "Unknown";

        public string Status { get; set; }
        public string Version { get; set; }

        public ApplicationState()
        {
            Status = Unknown;
            Version = Unknown;
        }
    }
}