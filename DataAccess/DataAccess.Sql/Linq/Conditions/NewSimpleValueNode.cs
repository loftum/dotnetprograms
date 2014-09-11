using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DataAccess.Sql.ExtensionMethods;

namespace DataAccess.Sql.Linq.Conditions
{
    public class NewSimpleValueNode : SqlNode
    {
        public object Value { get; private set; }
        public string ParameterName { get; private set; }

        public NewSimpleValueNode(NewExpression expression, SqlNode parent) : base(parent)
        {
            var arguments = GetArgumentValues(expression).ToArray();
            Value = expression.Constructor.Invoke(arguments);
            var parameter = Parent.GetParameterFor(Value);
            ParameterName = parameter.ParameterName;
        }

        private static IEnumerable<object> GetArgumentValues(NewExpression expression)
        {
            return expression.Arguments.Select(e => GetValueFrom((dynamic) e));
        }

        private static object GetValueFrom(ConstantExpression constant)
        {
            return constant.Value;
        }

        private static object GetValueFrom(MemberExpression expression)
        {
            var constant = expression.Expression as ConstantExpression;
            if (constant == null)
            {
                throw new InvalidOperationException(string.Format("Cannot obtain value from a {0}", expression.Expression.GetType().GetFriendlyName()));
            }
            var member = expression.Member;
            if (member is FieldInfo)
            {
                var field = (FieldInfo) member;
                return field.GetValue(constant.Value);
            }
            if (member is PropertyInfo)
            {
                var property = (PropertyInfo) member;
                return property.GetValue(constant.Value);
            }
            throw new InvalidOperationException(string.Format("Don't know how to get value from a {0}", expression.Member.GetType().GetFriendlyName()));
        }

        private static object GetValueFrom(object invalid)
        {
            throw new InvalidOperationException(string.Format("Cannot retreive value from {0}", invalid.GetType().GetFriendlyName()));
        }

        public override string ToSql()
        {
            return ParameterName;
        }
    }
}