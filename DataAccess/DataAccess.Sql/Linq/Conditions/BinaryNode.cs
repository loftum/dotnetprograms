using System.Linq.Expressions;

namespace DataAccess.Sql.Linq.Conditions
{
    public class BinaryNode : SqlNode
    {
        public SqlNode Left { get; private set; }
        public string Operator { get; private set; }
        public SqlNode Right { get; private set; }

        public BinaryNode(BinaryExpression expression, SqlNode parent) : base(parent)
        {
            Left = For(expression.Left);
            Operator = SqlOperator.For(expression.NodeType);
            Right = For(expression.Right);
        }

        public override string ToSql()
        {
            return Operator == "or"
                ? string.Format("({0} {1} {2})", Left.ToSql(), Operator, Right.ToSql())
                : string.Format("{0} {1} {2}", Left.ToSql(), Operator, Right.ToSql());
        }
    }
}