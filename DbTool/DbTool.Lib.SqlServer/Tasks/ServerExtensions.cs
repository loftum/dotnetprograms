using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.SqlServer.Tasks
{
    public static class ServerExtensions
    {
        public static void RenewConnection(this Server server)
        {
            server.ConnectionContext.Disconnect();
            server.ConnectionContext.Connect();
        }

        public static IList<SqlServerUser> GetLoginUsers(this Server server, Database database)
        {
            var loginNames = from login in server.Logins.Cast<Login>()
                             let mappings = login.EnumDatabaseMappings()
                             where mappings != null && mappings.Any(m => m.DBName.Equals(database.Name))
                             select login.Name;

            return database.Users.Cast<User>()
                .Where(user => loginNames.Any(name => user.Name.Equals(name)))
                .Select(user => new SqlServerUser(user))
                .ToList();
        }
    }
}