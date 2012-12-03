using System.Linq.Expressions;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public abstract class SqlTreeNode<TExpression> : TreeNode where TExpression : Expression
    {
        protected readonly ExpressionType NodeType;
        protected readonly TExpression Expression;

        protected SqlTreeNode(DbToolSql sql, TExpression expression) : base(sql)
        {
            Expression = expression;
            NodeType = Expression.NodeType;
        }
    }
}