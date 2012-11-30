using System.Linq;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class ConstantNode : SqlTreeNode<ConstantExpression>
    {
        public ConstantNode(ITreeNode parent, ConstantExpression expression) : base(parent, expression)
        {
        }

        public override string Translate()
        {
            if (typeof(IQueryable).IsAssignableFrom(Expression.Type))
            {
                var table = Expression.Type.GenericTypeArguments[0].Name;
                return HasParent
                    ? string.Format("from {0}", table)
                    : string.Format("select * from {0}", table);
            }
            return Stringify((dynamic)Expression.Value);
        }

        private static string Stringify(string value)
        {
            return string.Format("'{0}'", value);
        }

        private static string Stringify(object value)
        {
            return value.ToString();
        }
    }
}