using System.Linq;

namespace MissingLinq.Sql
{
    public interface IQueryableToSqlTranslator
    {
        MissingLinqSql TranslateSelect(IQueryable queryable);
        MissingLinqSql TranslateDelete(IQueryable queryable);
    }
}