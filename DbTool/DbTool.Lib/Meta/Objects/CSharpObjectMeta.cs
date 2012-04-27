using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DbTool.Lib.Meta.Objects
{
    public class CSharpObjectMeta : ObjectMeta
    {
        private CSharpTypeMeta CSharpType { get { return (CSharpTypeMeta) Type; } }

        public CSharpObjectMeta(object obj, string name) : base(new CSharpTypeMeta(obj.GetType()),  obj, name)
        {
            
        }

        public CSharpObjectMeta(PropertyInfo property, object obj)
            : base(new CSharpTypeMeta(obj == null ? property.PropertyType : obj.GetType()), property.GetValue(obj, new object[0]), property.Name)
        {
            
        }

        public  CSharpObjectMeta(FieldInfo field, object obj)
            : base(new CSharpTypeMeta(obj == null ? field.FieldType : obj.GetType()), field.GetValue(obj), field.Name)
        {
        }

        public override IEnumerable<ObjectMeta> Properties
        {
            get { return GetProperties(); }
        }

        public override IEnumerable<ObjectMeta> Members
        {
            get { return GetMembers(); }
        }

        private IList<ObjectMeta> GetProperties()
        {
            if (Object == null)
            {
                return new List<ObjectMeta>();
            }
            var members = new List<ObjectMeta>();
            foreach (var property in CSharpType.Type.GetProperties().Where(p => p != null))
            {
                Safely(() => members.Add(new CSharpObjectMeta(property, Object)));
            }
            return members;
        }

        private IEnumerable<ObjectMeta> GetMembers()
        {
            if (Object == null)
            {
                return new List<ObjectMeta>();
            }
            
            var members = GetProperties();

            foreach (var field in CSharpType.Type.GetFields(BindingFlags.Public | BindingFlags.Instance).Where(f => f != null))
            {
                Safely(() => members.Add(new CSharpObjectMeta(field, Object)));
            }
            return members;
        }

        public override ObjectMeta GetMember(string memberName)
        {
            var property = CSharpType.Type.GetProperty(memberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (property != null)
            {
                return new CSharpObjectMeta(property, Object);
            }
            var field = CSharpType.Type.GetField(memberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                return new CSharpObjectMeta(field, Object);
            }
            return null;
        }
    }
}