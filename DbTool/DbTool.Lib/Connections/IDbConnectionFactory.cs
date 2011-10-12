using System.Data.Common;
using DbTool.Lib.Configuration;

namespace DbTool.Lib.Connections
{
    public interface IDbConnectionFactory
    {
        DbConnection CreateConnection(ConnectionData connectionData);
    }
}