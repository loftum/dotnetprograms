using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace DotNetPrograms.Common.Meta
{
    public class PropertyMeta : MemberMeta
    {
        public Type Type { get; private set; }
        public TypeMeta Meta { get; private set; }
        private readonly PropertyInfo _property;

        public override string Name
        {
            get
            {
                return _property.Name.Replace("get_", string.Empty).Replace("set_", string.Empty);
            }
        }

        public PropertyMeta(PropertyInfo property)
        {
            _property = property;
            Type = _property.PropertyType;
            Meta = new TypeMeta(_property.PropertyType);
        }

        public bool IsProxiable { get { return _property.GetAccessors(true).All(IsProxiableMethod); } }

        public bool IsComplex
        {
            get { return Meta.IsComplex; }
        }

        public bool IsSimpleType
        {
            get { return Meta.IsSimple; }
        }

        public bool IsCollection
        {
            get { return Meta.IsCollection; }
        }

        public bool HasSetter
        {
            get { return _property.GetSetMethod() != null; }
        }

        public bool HasGetter
        {
            get { return _property.GetGetMethod() != null; }
        }

        public object GetValue(object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            try
            {
                return _property.GetValue(item, new object[0]);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Cannot get {0} {1}.{2}", Type, item.GetType(), Name), ex);
            }
        }

        public void SetValue(object item, object value)
        {
            _property.SetValue(item, value, new object[0]);
        }

        public IEnumerable GetValues(object item)
        {
            if (!IsCollection)
            {
                throw new InvalidOperationException(string.Format("{0} is a {1}, and not a collection", Name, Type));
            }
            return (IEnumerable)_property.GetValue(item, null);
        }

        public T GetCustomAttribute<T>()
        {
            return (T)_property.GetCustomAttributes(typeof(T), true).FirstOrDefault();
        }

        public bool HasCustomAttribute<T>()
        {
            return _property.GetCustomAttributes(typeof(T), true).Any();
        }
    }
}