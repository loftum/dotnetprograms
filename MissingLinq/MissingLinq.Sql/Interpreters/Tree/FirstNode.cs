using System.Linq.Expressions;

namespace MissingLinq.Sql.Interpreters.Tree
{
    public class FirstNode : SqlTreeNode<MethodCallExpression>
    {
        private ITreeNode _nextNode;
        public FirstNode(MissingLinqSql sql, MethodCallExpression expression) : base(sql, expression)
        {
            sql.Count = 1;
            sql.AllowDefault = expression.Method.Name.Contains("OrDefault");
            _nextNode = For(sql, expression.Arguments[0]);
        }

        public override string Translate()
        {
            return "";
        }
    }
}