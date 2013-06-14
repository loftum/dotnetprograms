using System.Linq;
using System.Linq.Expressions;
using DbTool.Lib.Linq.ExtensionMethods;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class OrderByNode : SqlTreeNode<MethodCallExpression>
    {
        private readonly string _column;
        private readonly LambdaSelector _lambda;
        private readonly ITreeNode _nextNode;

        public OrderByNode(DbToolSql sql, MethodCallExpression expression, bool ascending = true)
            : base(sql, expression)
        {
            _lambda = new LambdaSelector(Expression.Arguments[1].GetLambda());
            _column = _lambda.Properties.Single();
            _nextNode = For(sql, expression.Arguments[0]);
            sql.OrderBy(_column, ascending);
        }

        public override string Translate()
        {
            return null;
        }
    }
}