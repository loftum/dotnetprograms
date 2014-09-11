using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Sql.Linq.Conditions.Functions;

namespace DataAccess.Sql.Linq.Conditions
{
    public class MethodNode : SqlNode
    {
        public SqlNode Object { get; private set; }
        public IList<SqlNode> Arguments { get; private set; }
        public string MethodName { get; private set; }
        private readonly IMethodTranslator _translator;

        public MethodNode(MethodCallExpression expression, SqlNode parent, IMethodTranslator translator) : base(parent)
        {
            MethodName = expression.Method.Name;
            Object = For(expression.Object);
            Arguments = expression.Arguments.Select(For).ToList();
            _translator = translator;
        }

        public override string ToSql()
        {
            return _translator.Translate(this);
        }
    }
}