using System.Linq.Expressions;
using MissingLinq.Sql.ExtensionMethods;

namespace MissingLinq.Sql.Interpreters.Tree
{
    public class UnaryNode : SqlTreeNode<UnaryExpression>
    {
        private readonly ITreeNode _operand;
        private readonly string _operator;
        public UnaryNode(MissingLinqSql sql, UnaryExpression expression) : base(sql, expression)
        {
            _operand = For(sql, expression.Operand.StripQuotes());
            _operator = new SqlOperatorTranslator().Translate(NodeType);
        }

        public override string Translate()
        {
            if (NodeType == ExpressionType.Quote)
            {
                return string.Format("({0})", _operand.Translate());
            }
            return string.Format("{0} {1}", _operator, _operand.Translate());
        }
    }
}