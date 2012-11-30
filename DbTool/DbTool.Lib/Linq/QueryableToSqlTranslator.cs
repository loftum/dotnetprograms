using System.Linq;
using DbTool.Lib.Linq.Interpreters.Tree;

namespace DbTool.Lib.Linq
{
    public class QueryableToSqlTranslator : IQueryableToSqlTranslator
    {
        public DbToolSql Translate(IQueryable queryable)
        {
            var result = new DbToolSql();
            var node = TreeNode.For(null, queryable.Expression);
            result.Append(node.Translate());
            return result;
        }
    }
}