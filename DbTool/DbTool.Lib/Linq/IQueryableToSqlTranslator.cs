using System.Data.Common;
using System.Linq;

namespace DbTool.Lib.Linq
{
    public interface IQueryableToSqlTranslator
    {
        DbCommand Translate(IQueryable queryable);
    }
}