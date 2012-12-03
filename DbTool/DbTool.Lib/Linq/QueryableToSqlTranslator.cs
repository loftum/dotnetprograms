using System.Linq;
using DbTool.Lib.Linq.Interpreters.Tree;

namespace DbTool.Lib.Linq
{
    public class QueryableToSqlTranslator : IQueryableToSqlTranslator
    {
        public DbToolSql Translate(IQueryable queryable)
        {
            var node = TreeNode.For(new DbToolSql(), queryable.Expression);
            return node.Sql;
        }
    }
}