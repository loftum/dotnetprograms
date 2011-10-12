using DbTool.Lib.Configuration;

namespace DbTool.Lib.Connections
{
    public interface IDbContextFactory
    {
        DbContext CreateDbContext(ConnectionData connectionData);
    }
}