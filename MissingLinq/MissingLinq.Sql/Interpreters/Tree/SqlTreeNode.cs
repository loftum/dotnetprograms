using System.Linq.Expressions;

namespace MissingLinq.Sql.Interpreters.Tree
{
    public abstract class SqlTreeNode<TExpression> : TreeNode where TExpression : Expression
    {
        protected readonly ExpressionType NodeType;
        protected readonly TExpression Expression;

        protected SqlTreeNode(MissingLinqSql sql, TExpression expression) : base(sql)
        {
            Expression = expression;
            NodeType = Expression.NodeType;
        }
    }
}