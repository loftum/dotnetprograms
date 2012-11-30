using System.Linq.Expressions;
using DbTool.Lib.Linq.ExtensionMethods;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class UnaryNode : SqlTreeNode<UnaryExpression>
    {
        private readonly ITreeNode _operand;
        private readonly string _operator;
        public UnaryNode(ITreeNode parent, UnaryExpression expression) : base(parent, expression)
        {
            _operand = For(this, expression.Operand.StripQuotes());
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