using System.Linq.Expressions;
using DataAccess.Sql.Linq.Conditions;

namespace DataAccess.Sql.Linq.Selections
{
    public class LambdaSelectNode : SelectNode
    {
        public string LambdaParameter { get; private set; }
        public string Alias { get; private set; }
        public SqlNode Sql { get; private set; }

        public LambdaSelectNode(LambdaExpression expression, string alias, SqlNode parent) : base(parent)
        {
            LambdaParameter = expression.Parameters[0].Name;
            Alias = alias;
            Sql = For(expression.Body);
        }

        public override string GetAliasFor(string lambdaParameter)
        {
            return lambdaParameter == LambdaParameter ? Alias : Parent.GetAliasFor(lambdaParameter);
        }

        public override string ToSql()
        {
            return Sql.ToSql();
        }
    }
}