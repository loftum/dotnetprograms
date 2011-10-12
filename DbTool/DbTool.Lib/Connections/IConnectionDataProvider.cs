using System.Collections.Generic;
using DbTool.Lib.Configuration;

namespace DbTool.Lib.Connections
{
    public interface IConnectionDataProvider
    {
        IEnumerable<string> GetConnectionNames();
        string GetDefaultConnectionName();
        ConnectionData GetConnection(string selectedConnection);
    }
}