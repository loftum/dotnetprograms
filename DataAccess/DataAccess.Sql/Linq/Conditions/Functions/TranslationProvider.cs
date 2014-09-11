using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace DataAccess.Sql.Linq.Conditions.Functions
{
    public class TranslationProvider
    {
        private readonly IDictionary<MethodInfo, IMethodTranslator> _translators = new Dictionary<MethodInfo, IMethodTranslator>();

        public void Map<T>(Expression<Action<T>> expression, Func<MethodNode, string> translation)
        {
            var method = GetMethod(expression);
            _translators[method] = new MethodTranslator(translation);
        }

        public void Map<T>(Expression<Action<T>> expression, IMethodTranslator translator)
        {
            var method = GetMethod(expression);
            _translators[method] = translator;
        }

        private static MethodInfo GetMethod(LambdaExpression expression)
        {
            var methodCall = expression.Body as MethodCallExpression;
            if (methodCall == null)
            {
                throw new InvalidOperationException(string.Format("'{0}' is not a valid method call", expression.Body));
            }
            return methodCall.Method;
        }

        public IMethodTranslator Get(MethodInfo method)
        {
            return _translators.ContainsKey(method) ? _translators[method] : null;
        }
    }
}