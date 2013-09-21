using System.Data;
using System.Linq;
using MissingLinq.Sql;

namespace WebShop.Core.Data
{
    public class SqlDatabase : ISqlDatabase
    {
        private readonly IQueryProvider _queryProvider;

        public SqlDatabase(IDbConnection connection)
        {
            _queryProvider = new MissingLinqQueryProvider(new QueryableToSqlTranslator(), connection);
        }

        public IQueryable<T> GetAll<T>()
        {
            return new MissingLinqQueryable<T>(_queryProvider);
        }
    }
}