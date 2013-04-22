namespace BasicManifest.Common.Configuration
{
    public interface IBMConfig
    {
        string BasicManifestConnectionString { get; }
        bool ShowSql { get; }
        bool EnableNHibernateDiagnostics { get; }
    }
}