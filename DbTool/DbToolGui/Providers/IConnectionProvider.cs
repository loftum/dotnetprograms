using System.Collections.Generic;
using DbTool.Lib.Configuration;

namespace DbToolGui.Providers
{
    public interface IConnectionProvider
    {
        IEnumerable<string> GetConnectionNames();
        string GetDefaultConnectionName();
        ConnectionData GetConnection(string selectedConnection);
    }
}