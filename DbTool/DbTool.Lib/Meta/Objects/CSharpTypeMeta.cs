using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Meta.Objects
{
    public class CSharpTypeMeta : TypeMeta
    {
        public Type Type { get; private set; }

        public CSharpTypeMeta(Type type) : base(type.Name, type.Name)
        {
            Type = type;
        }

        public override IEnumerable<TypeMeta> Members
        {
            get { return GetMembers(); }
        }

        public override IEnumerable<TypeMeta> Properties
        {
            get { return GetProperties(); }
        }

        private IList<CSharpTypeMeta> GetProperties()
        {
            if (Type == null)
            {
                return new List<CSharpTypeMeta>();
            }
            var members = new List<CSharpTypeMeta>();
            foreach (var property in Type.GetProperties().Where(p => p != null))
            {
                Safely(() => members.Add(new CSharpTypeMeta(property.PropertyType)));
            }
            return members;
        }

        private IEnumerable<CSharpTypeMeta> GetMembers()
        {
            if (Type == null)
            {
                return new List<CSharpTypeMeta>();
            }

            var members = GetProperties();

            foreach (var field in Type.GetFields(BindingFlags.Public | BindingFlags.Instance).Where(f => f != null))
            {
                Safely(() => members.Add(new CSharpTypeMeta(field.FieldType)));
            }
            return members;
        }

        protected static void Safely(Action action)
        {
            try
            {
                action();
            }
            catch
            {
            }
        }
    }
}