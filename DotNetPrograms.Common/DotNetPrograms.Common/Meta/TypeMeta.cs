using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotNetPrograms.Common.Meta
{
    public class TypeMeta
    {
        public Type Type { get; private set; }

        public TypeMeta(Type type)
        {
            Type = type;
        }

        public IEnumerable<string> GetNonProxiableMemberNames()
        {
            var members = new HashSet<string>();
            foreach (var property in Properties.Where(p => !p.IsProxiable))
            {
                members.Add(property.Name);
            }

            foreach (var method in Methods.Where(m => !m.IsProxiable))
            {
                members.Add(method.Name);
            }

            foreach (var ev in Events.Where(e => !e.IsProxiable))
            {
                members.Add(ev.Name);
            }
            return members;
        }

        public IEnumerable<PropertyMeta> GetProperties<T>()
        {
            return Properties.Where(p => p.Type == typeof(T));
        }

        public IEnumerable<PropertyMeta> Properties { get { return Type.GetProperties().Select(p => new PropertyMeta(p)); } }

        public IEnumerable<MethodMeta> Methods
        {
            get
            {
                var ignoredMethods = new[] { "GetType" };
                return Type.GetMethods().Where(m => !ignoredMethods.Contains(m.Name) && !IsPropertyOrEvent(m))
                    .Select(m => new MethodMeta(m));
            }
        }

        private static bool IsPropertyOrEvent(MethodInfo method)
        {
            var name = method.Name;
            return name.StartsWith("get_") ||
                name.StartsWith("set_") ||
                name.StartsWith("add_") ||
                name.StartsWith("remove_");
        }

        public IEnumerable<EventMeta> Events
        {
            get { return Type.GetEvents().Select(e => new EventMeta(e)); }
        }

        public bool IsComplex
        {
            get { return Type != typeof(string) && (Type.IsClass || Type.IsInterface); }
        }

        public bool IsSimple
        {
            get { return Type.IsValueType || Type == typeof (string); }
        }

        public bool IsCollection
        {
            get { return IsEnumerable && Type != typeof(string); }
        }

        public bool IsValueOrString
        {
            get { return Type.IsValueType || Type == typeof(string); }
        }

        public bool IsEnumerable
        {
            get
            {
                return typeof(IEnumerable).IsAssignableFrom(Type)
                    || typeof(IEnumerable<>).IsAssignableFrom(Type);
            }
        }

        public bool IsDictionary
        {
            get
            {
                return typeof(IDictionary).IsAssignableFrom(Type);
            }
        }

        public IEnumerable<PropertyMeta> GetComplexProperties()
        {
            return Properties.Where(p => p.IsComplex);
        }

        public IEnumerable<PropertyMeta> GetCollectionProperties()
        {
            return Properties.Where(p => p.IsCollection);
        }

        public PropertyMeta GetProperty(string name)
        {
            return Properties.SingleOrDefault(p => p.Name == name);
        }

        public object NewUp()
        {
            var ctor = GetDefaultConstructor();
            if (ctor == null)
            {
                throw new InvalidOperationException(string.Format("{0} does not have a parameterless constructor.", Type.FullName));
            }
            try
            {
                return ctor.Invoke(new object[0]);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Could not invoke ctor for {0} ", Type), ex);
            }
        }

        public static T New<T>()
        {
            return (T) New(typeof (T));
        }

        public static object New(Type type)
        {
            return new TypeMeta(type).NewUp();
        }

        private ConstructorInfo GetDefaultConstructor()
        {
            return Type.GetConstructor(Type.EmptyTypes);
        }

        public bool Is<T>()
        {
            return Is(typeof(T));
        }

        public bool Is(Type type)
        {
            return type.IsAssignableFrom(Type);
        }

        public override string ToString()
        {
            var genericArguments = Type.GetGenericArguments();
            return genericArguments.Any()
                       ? string.Format("{0}<{1}>", Type.Name, string.Join(",", genericArguments.Select(a => a.Name)))
                       : Type.Name;
        }

        public bool HasCustomAttribute<T>() where T : Attribute
        {
            return GetCustomAttribute<T>() != null;
        }

        public T GetCustomAttribute<T>() where T : Attribute
        {
            return Type.GetCustomAttribute<T>(false);
        }
    }
}