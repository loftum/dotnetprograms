using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Convenient.ExtensionMethods;

namespace Convenient.Models
{
    public class ModelOptions
    {
        public IList<PropertyOption> Operations { get; private set; }

        public ModelOptions()
        {
            Operations = new List<PropertyOption>();
        }

        public ModelOptions(IList<PropertyOption> operations)
        {
            Operations = operations;
        }

        public ModelOptions For<T>(Action<ModelOptions<T>> action)
        {
            action(new ModelOptions<T>(Operations));
            return this;
        }

        public ObjectTag GetOperationFor(Type type, string name, ObjectTag defaultTag = ObjectTag.Edit)
        {
            var operation = Operations.FirstOrDefault(o => o.Type.IsAssignableFrom(type) && o.Property.Name == name);
            return operation == null ? defaultTag : operation.Tag;
        }
    }

    public class ModelOptions<T> : ModelOptions
    {
        public ModelOptions(IList<PropertyOption> operations) : base(operations)
        {
        }

        public ModelOptions<T> Hide<TProperty>(Expression<Func<T, TProperty>> property)
        {
            Operations.Add(new PropertyOption(property.GetProperty(), ObjectTag.Hide));
            return this;
        }

        public ModelOptions<T> ReadOnly<TProperty>(Expression<Func<T, TProperty>> property)
        {
            Operations.Add(new PropertyOption(property.GetProperty(), ObjectTag.ReadOnly));
            return this;
        }
    }
}