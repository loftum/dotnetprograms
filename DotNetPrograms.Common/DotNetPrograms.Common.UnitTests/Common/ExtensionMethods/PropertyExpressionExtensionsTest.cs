using System;
using System.Linq.Expressions;
using DotNetPrograms.Common.ExtensionMethods;
using NUnit.Framework;

namespace DotNetPrograms.Common.UnitTests.Common.ExtensionMethods
{
    [TestFixture]
    public class PropertyExpressionExtensionsTest
    {
        [Test]
        public void GetPropertyName_GetsNestedPropertyName()
        {
            var property = GetPropertyName<Farmer>(f => f.Tractor.Brand);
            Assert.That(property, Is.EqualTo("Tractor.Brand"));
        }

        [Test]
        public void GetMemberName_GetsMemberNameFromValueType()
        {
            var member = GetMemberName<Farmer>(f => f.Tractor.Manufactured);
            Assert.That(member, Is.EqualTo("Manufactured"));
        }

        [Test]
        public void GetMemberName_GetsMemberNameFromObjectType()
        {
            var member = GetMemberName<Farmer>(f => f.Tractor.Brand);
            Assert.That(member, Is.EqualTo("Brand"));
        }

        private static string GetPropertyName<TModel>(Expression<Func<TModel, object>> expression)
        {
            return expression.GetPropertyName();
        }

        private static string GetMemberName<TModel>(Expression<Func<TModel, object>> expression)
        {
            return expression.GetMemberName();
        }
    }
}