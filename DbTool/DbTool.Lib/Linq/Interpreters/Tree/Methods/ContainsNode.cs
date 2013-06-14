using System.Linq.Expressions;

namespace DbTool.Lib.Linq.Interpreters.Tree.Methods
{
    public class ContainsNode : StringMethodNode
    {
        public ContainsNode(DbToolSql sql, MethodCallExpression expression) : base(sql, expression)
        {
        }

        public override string Translate()
        {
            return string.Format("{0} like '%' + {1} + '%'", ColumnName, Argument);
        }
    }
}