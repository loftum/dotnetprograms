using System.Collections.Generic;
namespace DbTool.Lib.Configuration
{
    public interface IDbToolSettings
    {
        bool LoadSchema { get; set; }
        int MaxResult { get; set; }
        string DataDirectory { get; }
        string LogDirectory { get; }
        string BackupDirectory { get; }
        IDictionary<string, string> AssemblyMap { get; }
        ConnectionData DefaultConnection { get; }
        ConnectionData GetConnection(string name);
        bool HasConnectionString(string name);
        string GetConnectionString(string name);
    }
}