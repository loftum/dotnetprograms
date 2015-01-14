using System;
using System.Collections.Generic;
using System.Linq;

namespace Convenient.Models
{
    public class EnumObjectModel : SimpleObjectModel
    {
        public IList<object> EnumValues { get; private set; }

        public EnumObjectModel(ObjectModel parent, string name, Type type, object value, ModelOptions options, ObjectTag tag) : base(parent, name, type, value, options, tag)
        {
            EnumValues = Enum.GetValues(Type).Cast<object>().ToList();
        }
    }
}