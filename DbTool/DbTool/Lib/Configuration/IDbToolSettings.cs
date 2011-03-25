namespace DbTool.Lib.Configuration
{
    public interface IDbToolSettings
    {
        string DataDirectory { get; }
        string LogDirectory { get; }
        string BackupDirectory { get; }
        string MigrationPath { get; }
        DbConnection DefaultConnection { get; }
        bool HasConnectionString(string name);
        string GetConnectionString(string name);
    }
}