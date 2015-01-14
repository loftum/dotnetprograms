using System;
using System.Collections;
using System.Collections.Generic;
using Convenient.ExtensionMethods;

namespace Convenient.Models
{
    public class ListObjectModel : CollectionObjectModel
    {
        public ListObjectModel(ObjectModel parent, string name, Type type, object value, ModelOptions options, ObjectTag tag) : base(parent, name, type, value, options, tag)
        {
            ItemType = GetItempType(type);
            TemplateObject = For(this, "[-1]", ItemType, ItemType.GetDefaultValue(), ObjectTag.Template);

            Values = new List<ObjectModel>();
            var collection = (IEnumerable)Value;
            if (collection != null)
            {
                var index = 0;
                foreach (var collectionValue in collection)
                {
                    Values.Add(For(this, string.Format("[{0}]", index), collectionValue.GetType(), collectionValue));
                    index++;
                }
            }
        }

        private static Type GetItempType(Type type)
        {
            return type.IsGenericType ? type.GetGenericArguments()[0] : typeof (object);
        }
    }
}