using System.Linq.Expressions;
using DbTool.Lib.Linq.ExtensionMethods;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class WhereNode : SqlTreeNode<MethodCallExpression>
    {
        private readonly ITreeNode _condition;
        private readonly ITreeNode _previousNode;

        public WhereNode(DbToolSql sql, MethodCallExpression expression) : base(sql, expression)
        {
            _previousNode = For(sql, Expression.Arguments[0]);
            _condition = For(sql, Expression.Arguments[1].StripQuotes());
            sql.AppendWhere(_condition.Translate());
        }

        public override string Translate()
        {
            return _condition.Translate();
        }
    }
}