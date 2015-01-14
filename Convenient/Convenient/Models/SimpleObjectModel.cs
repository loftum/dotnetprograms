using System;

namespace Convenient.Models
{
    public class SimpleObjectModel : ObjectModel
    {
        public SimpleObjectModel(ObjectModel parent, string name, Type type, object value, ModelOptions options, ObjectTag tag) : base(parent, name, type, value, options, tag)
        {
        }
    }
}