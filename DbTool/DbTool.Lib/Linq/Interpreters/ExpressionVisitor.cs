using System;

namespace DbTool.Lib.Linq.Interpreters
{
    public class ExpressionVisitor
    {
        protected void Visit(object invalid)
        {
            throw new InvalidOperationException(string.Format("Don't know how to visit {0}", invalid.GetType()));
        }
    }
}