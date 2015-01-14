using System;
using System.Collections.Generic;

namespace Convenient.Models
{
    public abstract class CollectionObjectModel : ObjectModel
    {
        public IList<ObjectModel> Values { get; protected set; }
        public Type ItemType { get; protected set; }

        public ObjectModel TemplateObject { get; protected set; }

        protected CollectionObjectModel(ObjectModel parent, string name, Type type, object value, ModelOptions options, ObjectTag tag) : base(parent, name, type, value, options, tag)
        {
        }
    }
}