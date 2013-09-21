using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MissingLinq.Sql.Interpreters.Tree
{
    public class LambdaSelector
    {
        public ParameterExpression Parameter { get; private set; }
        public IList<string> Properties { get; private set; }

        public LambdaSelector(LambdaExpression expression)
        {
            Properties = GetPropertiesFrom((dynamic) expression.Body);
            Parameter = expression.Parameters.Single();
        } 

        private static List<string> GetPropertiesFrom(ParameterExpression parameter)
        {
            return parameter.Type.GetProperties().Select(p => p.Name).ToList();
        } 

        private static List<string> GetPropertiesFrom(MemberExpression member)
        {
            return new List<string>{ member.Member.Name };
        } 

        private static List<string> GetPropertiesFrom(NewExpression newExpression)
        {
            return newExpression.Members.Select(m => m.Name).ToList();
        } 

        private static List<string> GetPropertiesFrom(object invalid)
        {
            throw new InvalidOperationException(string.Format("Don't know how to get properties from a {0}", invalid.GetType()));
        } 
    }
}