using System.Linq.Expressions;

namespace MissingLinq.Sql.Interpreters.Tree.Methods
{
    public class EndsWithNode : StringMethodNode
    {
        public EndsWithNode(MissingLinqSql sql, MethodCallExpression expression) : base(sql, expression)
        {
        }

        public override string Translate()
        {
            return string.Format("{0} like '%' + {1}", ColumnName, Argument);
        }
    }
}