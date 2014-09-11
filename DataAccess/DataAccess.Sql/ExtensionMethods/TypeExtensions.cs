using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DataAccess.Sql.ExtensionMethods
{
    public static class TypeExtensions
    {
        private static readonly IDictionary<Type, string> TypeNames = new Dictionary<Type, string>();

        static TypeExtensions()
        {
            TypeNames[typeof (int)] = "int";
            TypeNames[typeof (short)] = "short";
            TypeNames[typeof (byte)] = "byte";
            TypeNames[typeof (bool)] = "bool";
            TypeNames[typeof (long)] = "long";
            TypeNames[typeof (float)] = "float";
            TypeNames[typeof (double)] = "double";
            TypeNames[typeof (decimal)] = "decimal";
            TypeNames[typeof (string)] = "string";
        }

        public static bool IsValueOrString(this Type type)
        {
            return type.IsValueType || type == typeof (string);
        }

        public static string GetFriendlyName(this Type type)
        {
            if (TypeNames.ContainsKey(type))
            {
                return TypeNames[type];
            }
            return type.IsGenericType
                ? string.Format("{0}<{1}>", type.Name.Remove(type.Name.IndexOf('`')), string.Join(", ", type.GetGenericArguments().Select(a => a.GetFriendlyName())))
                : type.Name;
        }

        public static bool IsCollection(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        public static object NewUp(this Type type, params object[] arguments)
        {
            var argumentTypes = arguments.Select(a => a.GetType()).ToArray();
            var ctor = type.GetConstructor(argumentTypes);
            if (ctor == null)
            {
                throw new InvalidOperationException(string.Format("There is no ctor {0}({1})", type.Name, string.Join(", ", argumentTypes.Select(t => t.Name))));
            }
            return ctor.Invoke(arguments);
        }

        public static T NewUpAs<T>(this Type type, params object[] arguments)
        {
            return (T) type.NewUp(arguments);
        }

        public static bool IsAnonymous(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return type.IsClass &&
                   type.GetCustomAttribute<CompilerGeneratedAttribute>() != null &&
                   !type.IsNested &&
                   type.IsSealed &&
                   type.IsGenericType &&
                   (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$")) && type.Name.Contains("AnonymousType");
        }

        public static IEnumerable<MemberInfo> GetSimplePropertiesAndFields(this Type type)
        {
            var members = type.GetProperties()
                .Where(p => p.PropertyType.IsValueOrString())
                .Concat(type.GetFields().Where(f => f.FieldType.IsValueOrString()).Cast<MemberInfo>())
                .ToArray();
            return members;
        }

        public static object GetDefaultValue(this Type type)
        {
            if (type == typeof (DateTime))
            {
                return new DateTime(1900, 01, 01);
            }
            return type.IsValueType
                ? Activator.CreateInstance(type)
                : null;
        }
    }
}