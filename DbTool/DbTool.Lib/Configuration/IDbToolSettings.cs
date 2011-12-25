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
        string CurrentContextName { get; set; }
        DbToolContext CurrentContext { get; }
        IList<DbToolContext> Contexts { get; set; }
        IDictionary<string, string> AssemblyMap { get; }
        ConnectionData DefaultConnection { get; }
        string WorksheetFile { get; set; }
        ConnectionData GetConnection(string name);
        DbToolDatabase GetDatabase(string name);
        void SetCurrentContext(string contextName);
        void DeleteContext(string contextName);
        void Addcontext(string contextName);
        
    }
}