using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetPrograms.Common.Meta
{
    public class ObjectReflector
    {
        public object Value { get; private set; }
        public TypeMeta Meta { get; private set; }

        public ObjectReflector(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            Value = value;
            Meta = new TypeMeta(value.GetType());
        }

        public IList<PropertyReflector> Properties
        {
            get { return PropertyReflectorsFor(Meta.Properties); }
        }

        public IList<PropertyReflector> GetProperties<T>()
        {
            return PropertyReflectorsFor(Meta.GetProperties<T>());
        }

        private IList<PropertyReflector> PropertyReflectorsFor(IEnumerable<PropertyMeta> properties)
        {
            return properties.Select(p => new PropertyReflector(Value, p)).ToList();
        }

        public IList<PropertyReflector> GetSimpleTypeProperties()
        {
            return PropertyReflectorsFor(Meta.Properties.Where(p => p.IsSimpleType));
        }
    }
}