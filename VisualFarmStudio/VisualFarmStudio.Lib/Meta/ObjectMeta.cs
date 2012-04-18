using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VisualFarmStudio.Lib.Meta
{
    public class ObjectMeta
    {
        public bool IsInterestingType
        {
            get
            {
                return Type != null && Type.Namespace != null && Type.Namespace.Contains("VisualFarmStudio");
            }
        }

        public bool IsAnonymousType { get { return Type != null && Type.Namespace == null; } }

        public bool IsCollection { get { return Type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(Type); } }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public Type Type { get; private set; }
        public object Value { get; private set; }

        public ObjectMeta(PropertyInfo property, object obj)
        {
            Type = property.PropertyType;
            Value = property.GetValue(obj, new object[0]);
            Name = property.Name;
            DisplayName = GetDisplayName();
        }

        public ObjectMeta(FieldInfo field, object obj)
        {
            Type = field.FieldType;
            Value = field.GetValue(obj);
            Name = field.Name;
            DisplayName = GetDisplayName();
        }

        private string GetDisplayName()
        {
            if (IsCollection)
            {
                var count = 0;
                if (Value != null)
                {
                    count = ((IEnumerable)Value).Cast<object>().Count();
                }
                return string.Format("{0} ({1})", Name, count);
            }
            return Name;
        }

        public ObjectMeta(object obj, string name)
        {
            Value = obj;
            Type = obj.GetType();
            Name = name;
            DisplayName = GetDisplayName();
        }

        public IEnumerable<ObjectMeta> GetStaticMembers()
        {
            if (Value == null)
            {
                return Enumerable.Empty<ObjectMeta>();
            }
            var type = Value.GetType();
            var members = new List<ObjectMeta>();
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).Where(p => p != null))
            {
                Safely(() => members.Add(new ObjectMeta(property, Value)));
            }
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).Where(f => f != null))
            {
                Safely(() => members.Add(new ObjectMeta(field, Value)));
            }
            return members;
        }

        public IEnumerable<ObjectMeta> GetMembers()
        {
            if (Value == null)
            {
                return Enumerable.Empty<ObjectMeta>();
            }
            var type = Value.GetType();
            var members = new List<ObjectMeta>();
            foreach (var property in type.GetProperties().Where(p => p != null))
            {
                Safely(() => members.Add(new ObjectMeta(property, Value)));
            }
            foreach (var field in type.GetFields().Where(f => f != null))
            {
                Safely(() => members.Add(new ObjectMeta(field, Value)));
            }
            return members;
        }

        private static void Safely(Action action)
        {
            try
            {
                action();
            }
            catch
            {
            }
        }

        public ObjectMeta GetMember(string memberName)
        {
            var property = Type.GetProperty(memberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (property != null)
            {
                return new ObjectMeta(property, Value);
            }
            var field = Type.GetField(memberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                return new ObjectMeta(field, Value);
            }
            return null;
        }

        public static ObjectMeta FromPath(object root, string rootName, IEnumerable<string> path)
        {
            var current = new ObjectMeta(root, rootName);
            foreach (var memberName in path)
            {
                var member = current.GetMember(memberName);
                if (member == null)
                {
                    throw new Exception(string.Format("Unknown member '{0}'", memberName));
                }
                current = member;
            }
            return current;
        }
    }
}