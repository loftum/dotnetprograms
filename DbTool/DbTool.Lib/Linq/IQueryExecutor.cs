using System.Data.Common;

namespace DbTool.Lib.Linq
{
    public interface IQueryExecutor
    {
        object Execute(DbCommand command);
    }
}