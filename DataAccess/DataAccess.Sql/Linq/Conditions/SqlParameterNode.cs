using System.Linq.Expressions;

namespace DataAccess.Sql.Linq.Conditions
{
    public class SqlParameterNode : SqlNode
    {
        public string ParameterName { get; private set; }
        public object Value { get; private set; }

        public SqlParameterNode(MemberExpression expression, SqlNode parent) : base(parent)
        {
            Value = ExpressionValueRetreiver.GetValueFrom(expression);
            var parameter = GetParameterFor(Value);
            ParameterName = parameter.ParameterName;
        }

        public override string ToSql()
        {
            return ParameterName;
        }
    }
}