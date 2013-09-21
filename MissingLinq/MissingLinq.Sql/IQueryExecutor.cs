using System.Data.Common;

namespace MissingLinq.Sql
{
    public interface IQueryExecutor
    {
        object Execute(DbCommand command);
    }
}