namespace BuildMonitor.Lib.Configuration
{
    public interface IBuildMonitorSettings
    {
        string BuildHost { get; }
        IBuildServerSettings BuildServer { get; }
    }
}