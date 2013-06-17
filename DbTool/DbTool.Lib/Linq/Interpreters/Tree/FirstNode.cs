using System.Linq.Expressions;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class FirstNode : SqlTreeNode<MethodCallExpression>
    {
        private ITreeNode _nextNode;
        public FirstNode(DbToolSql sql, MethodCallExpression expression) : base(sql, expression)
        {
            sql.Count = 1;
            _nextNode = For(sql, expression.Arguments[0]);
        }

        public override string Translate()
        {
            return "";
        }
    }
}