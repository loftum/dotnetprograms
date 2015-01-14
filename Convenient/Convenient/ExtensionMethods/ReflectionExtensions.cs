using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Convenient.Models;

namespace Convenient.ExtensionMethods
{
    public static class ReflectionExtensions
    {
        private static readonly IDictionary<Type, string> ValueNames = new Dictionary<Type, string>
        {
            {typeof(int), "int"},
            {typeof(short), "short"},
            {typeof(byte), "byte"},
            {typeof(bool), "bool"},
            {typeof(long), "long"},
            {typeof(float), "float"},
            {typeof(double), "double"},
            {typeof(decimal), "decimal"},
            {typeof(string), "string"}
        };

        public static string GetFriendlyName(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (ValueNames.ContainsKey(type))
            {
                return ValueNames[type];
            }
            if (type.IsGenericType)
            {
                return string.Format("{0}<{1}>", type.Name.Split('`')[0],
                    string.Join(", ", type.GetGenericArguments().Select(GetFriendlyName)));
            }
            return type.Name;
        }

        public static bool IsCollection(this Type type)
        {
            return !type.IsSimpleType() && typeof (IEnumerable).IsAssignableFrom(type);
        }

        public static bool IsSimpleType(this Type type)
        {
            return type.IsValueType || type == typeof (string);
        }

        public static object GetDefaultValue(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static bool IsDictionary(this Type type)
        {
            return !type.IsSimpleType() && typeof (IDictionary).IsAssignableFrom(type);
        }

        public static ObjectType GetObjectType(this Type type)
        {
            if (type.IsEnum)
            {
                return ObjectType.Enum;
            }
            if (type.IsSimpleType())
            {
                return ObjectType.Simple;
            }
            if (type.IsDictionary())
            {
                return ObjectType.Dictionary;
            }
            if (type.IsCollection())
            {
                return ObjectType.Collection;
            }
            return ObjectType.Complex;
        }

        public static bool HasCustomAttribute<T>(this MemberInfo member) where T : Attribute
        {
            return member.GetCustomAttribute<T>() != null;
        }
    }
}