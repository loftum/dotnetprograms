using System.Linq;
using MissingLinq.Sql.Interpreters.Tree;

namespace MissingLinq.Sql
{
    public class QueryableToSqlTranslator : IQueryableToSqlTranslator
    {
        public MissingLinqSql TranslateSelect(IQueryable queryable)
        {
            var node = TreeNode.For(new MissingLinqSql(), queryable.Expression);
            return node.Sql;
        }

        public MissingLinqSql TranslateDelete(IQueryable queryable)
        {
            var node = TreeNode.For(new MissingLinqSql(), queryable.Expression);
            var sql = node.Sql;
            sql.Verb = "delete";
            sql.What = "";
            return sql;
        }
    }
}