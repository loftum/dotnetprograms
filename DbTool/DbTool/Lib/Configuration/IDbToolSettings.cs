namespace DbTool.Lib.Configuration
{
    public interface IDbToolSettings
    {
        string DataDirectory { get; }
        string LogDirectory { get; }
        string BackupDirectory { get; }
        ConnectionData DefaultConnection { get; }
        ConnectionData GetConnection(string name);
        bool HasConnectionString(string name);
        string GetConnectionString(string name);
    }
}