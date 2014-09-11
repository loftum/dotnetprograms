using System.Linq.Expressions;
using DataAccess.Sql.Linq.Conditions;

namespace DataAccess.Sql.Linq
{
    public class WhereNode : SqlNode
    {
        public string LambdaParameter { get; private set; }
        public string Alias { get; private set; }
        public SqlNode Sql { get; private set; }

        public WhereNode(LambdaExpression expression, string alias, SqlNode parent) : base(parent)
        {
            LambdaParameter = expression.Parameters[0].Name;
            Alias = alias;
            Sql = For(expression.Body);
        }

        public override string GetAliasFor(string lambdaParameter)
        {
            return lambdaParameter == LambdaParameter ? Alias : null;
        }

        public override string ToSql()
        {
            return Sql.ToSql();
        }
    }
}