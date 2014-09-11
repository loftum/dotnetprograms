using System.Linq.Expressions;
using DataAccess.Sql.Linq.Conditions;

namespace DataAccess.Sql.Linq.Selections
{
    public class SumSelectNode : SelectNode
    {
        public SqlNode Argument { get; private set; }

        public SumSelectNode(Expression expression, SqlNode parent) : base(parent)
        {
            Argument = For(expression);
        }

        public override string ToSql()
        {
            return string.Format("sum({0})", Argument);
        }
    }
}