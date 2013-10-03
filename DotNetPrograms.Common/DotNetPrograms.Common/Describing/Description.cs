using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DotNetPrograms.Common.ExtensionMethods;

namespace DotNetPrograms.Common.Describing
{
    public class Description
    {
        private readonly StringBuilder _builder = new StringBuilder();

        public Description(string name)
        {
            _builder.Append(name);
        }

        public Description By(Expression<Func<object>> property, params Expression<Func<object>>[] properties)
        {
            return By(new[] {property}.Concat(properties));
        }

        public Description By(IEnumerable<Expression<Func<object>>> properties)
        {
            if (properties.Any())
            {
                _builder.AppendFormat(" with {0}", Properties(properties));
            }
            return this;
        }

        public override string ToString()
        {
            return _builder.ToString();
        }

        public static Description Of<T>(params Expression<Func<object>>[] properties)
        {
            return OfThing(typeof (T).Name, properties);
        }

        public static Description OfThing(string thing, params Expression<Func<object>>[] properties)
        {
            return new Description(thing).By(properties);
        }

        private static string Properties(IEnumerable<Expression<Func<object>>> properties)
        {
            return string.Join(" and ", properties.Select(NameEqualsValue));
        }

        private static object NameEqualsValue(Expression<Func<object>> property)
        {
            var name = property.GetMemberName();
            var value = property.Compile().Invoke();
            return string.Format("{0}='{1}'", name, value);
        }
    }
}