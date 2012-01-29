using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.SqlServer.Tasks
{
    public class SqlServerUser
    {
        public string Name { get; private set; }
        public string Login { get; private set; }
        public string Database { get; private set; }
        public IEnumerable<string> Roles { get; private set; }

        public SqlServerUser(User user)
        {
            Name = user.Name;
            Login = user.Login;
            Database = user.Parent.Name;
            Roles = user.EnumRoles().Cast<string>().ToList();
        }
    }
}