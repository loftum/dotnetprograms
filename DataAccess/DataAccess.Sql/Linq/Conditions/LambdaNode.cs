using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Sql.Linq.Conditions
{
    public class LambdaNode : SqlNode
    {
        public IList<SqlNode> Parameters { get; private set; }
        public SqlNode Body { get; private set; }

        public LambdaNode(LambdaExpression expression, SqlNode parent) : base(parent)
        {
            Parameters = expression.Parameters.Select(For).ToList();
            Body = For(expression.Body);
        }

        public override string ToSql()
        {
            return Body.ToSql();
        }
    }
}