using System.Linq.Expressions;
using DataAccess.Sql.Linq.Conditions;
using DataAccess.Sql.Statements;

namespace DataAccess.Sql.Linq
{
    public class OrderNode : SqlNode
    {
        public SqlNode Argument { get; private set; }
        public SortDir Direction { get; private set; }
        public string LambdaParameter { get; private set; }
        public string Alias { get; private set; }

        public OrderNode(LambdaExpression expression, SortDir direction, string alias, SqlNode parent) : base(parent)
        {
            LambdaParameter = expression.Parameters[0].Name;
            Alias = alias;
            Direction = direction;
            Argument = For(expression.Body);
        }

        public override string GetAliasFor(string lambdaParameter)
        {
            return lambdaParameter == LambdaParameter ? Alias : base.GetAliasFor(lambdaParameter);
        }

        public override string ToSql()
        {
            return string.Format("{0} {1}", Argument, Direction);
        }
    }
}