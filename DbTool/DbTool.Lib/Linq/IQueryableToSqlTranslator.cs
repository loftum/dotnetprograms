using System.Linq;

namespace DbTool.Lib.Linq
{
    public interface IQueryableToSqlTranslator
    {
        DbToolSql Translate(IQueryable queryable);
    }
}