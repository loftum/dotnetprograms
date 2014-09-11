using System;
using System.Reflection;

namespace DataAccess.Sql.ExtensionMethods
{
    public static class MemberInfoExtensions
    {
        public static object GetValue(this MemberInfo member, object item)
        {
            if (member is FieldInfo)
            {
                var field = (FieldInfo) member;
                return field.GetValue(item);
            }
            if (member is PropertyInfo)
            {
                var property = (PropertyInfo) member;
                return property.GetValue(item);
            }
            throw new InvalidOperationException(string.Format("Don't know how to get value from member type {0}", member.GetType().GetFriendlyName()));
        }
    }
}