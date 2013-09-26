using System.Data;
using System.Linq;

namespace MissingLinq.Sql.Data
{
    public class MissingLinqDatabase : IMissingLinqDatabase
    {
        private readonly IQueryProvider _queryProvider;

        public MissingLinqDatabase(IDbConnection connection)
        {
            _queryProvider = new MissingLinqQueryProvider(new QueryableToSqlTranslator(), connection);
        }

        public IQueryable<T> GetAll<T>()
        {
            return new MissingLinqQueryable<T>(_queryProvider);
        }
    }
}