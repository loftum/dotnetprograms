using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Convenient.ExtensionMethods;

namespace Convenient.Models
{
    public class ComplexObjectModel : ObjectModel
    {
        public IList<ObjectModel> Properties { get; private set; }
        public IEnumerable<SimpleObjectModel> SimpleProperties { get { return Properties.OfType<SimpleObjectModel>(); } }
        public IEnumerable<ComplexObjectModel> ComplexProperties { get { return Properties.OfType<ComplexObjectModel>(); } }
        public IEnumerable<EnumObjectModel> EnumProperties { get { return Properties.OfType<EnumObjectModel>(); } }
        public IEnumerable<CollectionObjectModel> CollectionProperties { get {return Properties.OfType<CollectionObjectModel>(); } }

        public ComplexObjectModel(ObjectModel parent, string name, Type type, object value, ModelOptions options, ObjectTag tag) : base(parent, name, type, value, options, tag)
        {
            Properties = Type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => For(this, p.Name, p.PropertyType, value == null ? p.PropertyType.GetDefaultValue() : p.GetValue(Value)))
                .ToList();
        }

        public static ComplexObjectModel For(object value, Action<ModelOptions> action = null)
        {
            var options = new ModelOptions();
            if (action != null)
            {
                action(options);
            }
            var type = value.GetType();
            return new ComplexObjectModel(null, type.Name, type, value, options, ObjectTag.Edit);
        }
    }
}