using System.Linq.Expressions;

namespace DbTool.Lib.Linq.ExtensionMethods
{
    public static class ExpressionExtensions
    {
        public static Expression StripQuotes(this Expression expression)
        {
            while (expression.NodeType == ExpressionType.Quote)
            {
                expression = ((UnaryExpression)expression).Operand;
            }
            return expression;
        }

        public static LambdaExpression GetLambda(this Expression expression)
        {
            var lambda = StripQuotes(expression) as LambdaExpression;
            if (lambda == null)
            {
                var constantValue = (ConstantExpression)expression;
                lambda = (LambdaExpression)constantValue.Value;
            }
            return lambda;
        }
    }
}