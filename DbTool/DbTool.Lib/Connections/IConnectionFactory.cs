using System.Data.Common;
using DbTool.Lib.Configuration;

namespace DbTool.Lib.Connections
{
    public interface IConnectionFactory
    {
        DbConnection CreateConnection(ConnectionData connectionData);
    }
}