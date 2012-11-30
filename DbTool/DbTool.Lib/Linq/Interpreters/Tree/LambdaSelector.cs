using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbTool.Lib.Linq.Interpreters.Tree
{
    public class LambdaSelector
    {
        private ParameterExpression _parameter;
        public IList<string> Properties { get; private set; }

        public LambdaSelector(LambdaExpression expression)
        {
            Properties = new List<string> { "*" };
            _parameter = expression.Parameters.Single();
        }
    }
}