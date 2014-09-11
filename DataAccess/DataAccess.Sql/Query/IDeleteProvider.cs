using System.Linq.Expressions;

namespace DataAccess.Sql.Query
{
    public interface IDeleteProvider
    {
        int ExecuteDelete(Expression expression);
    }
}