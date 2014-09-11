using System;
using System.Linq.Expressions;
using DataAccess.Sql.ExtensionMethods;

namespace DataAccess.Sql.Linq.Conditions
{
    public class ColumnNode : SqlNode
    {
        public string Alias { get; private set; }
        public string Name { get; private set; }

        public ColumnNode(MemberExpression expression, SqlNode parent) : base(parent)
        {
            var parameter = expression.Expression as ParameterExpression;
            if (parameter == null)
            {
                throw new InvalidOperationException(string.Format("Cannot select column for a parameterless MemberExpression."));
            }
            Name = expression.Member.Name;
            Alias = GetAliasFor(parameter.Name);
        }

        public override string ToSql()
        {
            return string.Format("{0}.{1}", Alias, Name.InBrackets());
        }
    }
}