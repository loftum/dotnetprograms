using System.Collections.Generic;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class MethodCallNode : SqlTreeNode<MethodCallExpression>
    {
        protected readonly string MethodName;
        protected readonly IList<ITreeNode> Arguments;

        public MethodCallNode(ITreeNode parent, MethodCallExpression expression) : base(parent, expression)
        {
            MethodName = Expression.Method.Name;
            Arguments = For(this, Expression.Arguments);
        }

        public override string Translate()
        {
            return "";
        }
    }
}