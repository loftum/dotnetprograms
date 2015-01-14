using System;
using System.Collections;
using System.Collections.Generic;
using Convenient.ExtensionMethods;

namespace Convenient.Models
{
    public class DictionaryObjectModel : CollectionObjectModel
    {
        public DictionaryObjectModel(ObjectModel parent, string name, Type type, object value, ModelOptions options, ObjectTag tag) : base(parent, name, type, value, options, tag)
        {
            ItemType = GetItempType(type);
            TemplateObject = For(this, "[-1]", ItemType, ItemType.GetDefaultValue(), ObjectTag.Template);

            Values = new List<ObjectModel>();
            var dictionary = (IDictionary)Value;
            if (dictionary != null)
            {
                var index = 0;
                foreach (var pair in dictionary)
                {
                    Values.Add(For(this, string.Format("[{0}]", index), ItemType, Map(pair, ItemType)));
                    index++;
                }
            }
        }

        private static object Map(object source, Type destinationType)
        {
            if (source == null)
            {
                return destinationType.GetDefaultValue();
            }
            var sourceType = source.GetType();
            var destination = Activator.CreateInstance(destinationType);
            foreach (var property in destinationType.GetProperties())
            {
                var sourceProperty = sourceType.GetProperty(property.Name);
                property.SetValue(destination, sourceProperty.GetValue(source));
            }
            return destination;
        }

        private static Type GetItempType(Type type)
        {
            return type.IsGenericType ? typeof(KeyValue<,>).MakeGenericType(type.GetGenericArguments()) : typeof(DictionaryEntry);
        }
    }
}