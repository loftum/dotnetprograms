using System.Linq.Expressions;

namespace DataAccess.Sql.Linq.Conditions
{
    public class ConstantNode : SqlNode
    {
        public object Value { get; private set; }
        public string ParameterName { get; private set; }

        public ConstantNode(ConstantExpression expression, SqlNode parent) : base(parent)
        {
            Value = expression.Value;
            var parameter = Parent.GetParameterFor(expression.Value);
            ParameterName = parameter.ParameterName;
        }

        public override string ToSql()
        {
            return ParameterName;
        }
    }
}