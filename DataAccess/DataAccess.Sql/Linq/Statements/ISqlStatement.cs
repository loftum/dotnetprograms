using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess.Sql.Linq.Statements
{
    public interface ISqlStatement
    {
        string CommandText { get; }
        IList<SqlParameter> Parameters { get; }
    }
}