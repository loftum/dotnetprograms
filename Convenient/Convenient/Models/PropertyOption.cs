using System;
using System.Reflection;

namespace Convenient.Models
{
    public class PropertyOption
    {
        public Type Type { get; private set; }
        public PropertyInfo Property { get; private set; }
        public ObjectTag Tag { get; private set; }

        public PropertyOption(PropertyInfo property, ObjectTag tag)
        {
            Type = property.ReflectedType;
            Property = property;
            Tag = tag;
        }
    }
}