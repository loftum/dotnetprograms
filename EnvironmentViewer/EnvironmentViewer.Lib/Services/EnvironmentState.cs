namespace EnvironmentViewer.Lib.Services
{
    public class EnvironmentState
    {
        private const string Unknown = "Unknown";

        public string ApplicationVersion { get; set; }
        public string ApplicationStatus { get; set; }
        public string DatabaseVersion { get; set; }
        public string DatabaseStatus { get; set; }

        public EnvironmentState()
        {
            ApplicationVersion = Unknown;
            ApplicationStatus = Unknown;
            DatabaseVersion = Unknown;
            DatabaseStatus = Unknown;
        }
    }
}