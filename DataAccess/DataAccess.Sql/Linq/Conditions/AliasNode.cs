using System.Linq.Expressions;

namespace DataAccess.Sql.Linq.Conditions
{
    public class AliasNode : SqlNode
    {
        public string Name { get; private set; }
        public string Alias { get; private set; }

        public AliasNode(ParameterExpression expression, SqlNode parent) : base(parent)
        {
            Name = expression.Name;
            Alias = GetAliasFor(expression.Name);
        }

        public override string ToSql()
        {
            return Alias;
        }
    }
}