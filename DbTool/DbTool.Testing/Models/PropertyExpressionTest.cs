﻿using System;
using System.Linq.Expressions;
using DotNetPrograms.Common.ExtensionMethods;
using NUnit.Framework;

namespace DbTool.Testing.Models
{
    [TestFixture]
    public class PropertyExpressionTest
    {
        public string SomeProperty { get; set; }

        [Test]
        public void GetPropertyId_ShouldReturnNameOfPropertyForModel()
        {
            Expression<Func<PropertyExpressionTest, string>> modelPrperty = m => m.SomeProperty;
            var propertyId = modelPrperty.GetPropertyName();
            Assert.That(propertyId, Is.EqualTo("SomeProperty"));
        }

        [Test]
        public void GetPropertyId_ShouldReturnNameOfPropertyForSelf()
        {
            Expression<Func<string>> myProperty = () => SomeProperty;
            var propertyId = myProperty.GetPropertyName();
            Assert.That(propertyId, Is.EqualTo("SomeProperty"));
        }
   
    }
}