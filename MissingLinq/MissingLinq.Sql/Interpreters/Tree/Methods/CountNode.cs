using System.Linq.Expressions;

namespace MissingLinq.Sql.Interpreters.Tree.Methods
{
    public class CountNode : SqlTreeNode<MethodCallExpression>
    {
        private readonly ITreeNode _nextNode;

        public CountNode(MissingLinqSql sql, MethodCallExpression expression) : base(sql, expression)
        {
            sql.What = "count('x')";
            _nextNode = For(sql, expression.Arguments[0]);
        }

        public override string Translate()
        {
            return null;
        }
    }
}