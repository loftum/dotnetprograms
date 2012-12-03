using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class MethodCallNode : SqlTreeNode<MethodCallExpression>
    {
        protected readonly string MethodName;
        protected readonly IList<ITreeNode> Arguments;

        public MethodCallNode(DbToolSql sql, MethodCallExpression expression) : base(sql, expression)
        {
            MethodName = Expression.Method.Name;
            Arguments = For(sql, Expression.Arguments);
        }

        public override string Translate()
        {
            return "";
        }
    }
}