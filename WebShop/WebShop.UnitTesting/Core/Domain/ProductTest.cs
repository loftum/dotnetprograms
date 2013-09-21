using System;
using System.Linq;
using NUnit.Framework;
using WebShop.Core.Domain;
using WebShop.Core.Domain.MasterData;
using WebShop.UnitTesting.TestData;

namespace WebShop.UnitTesting.Core.Domain
{
    [TestFixture]
    public class ProductTest
    {
        private Product _parent;
        private Product _product;

        [SetUp]
        public void Setup()
        {
            _parent = new Product();
            _product = new Product{ Parent = _parent };
        }

        [Test]
        public void Description_IsInheritedFromParent_WhenNotSet()
        {
            _parent.Description.Value = Some.Text;

            Assert.That(_product.Description.Calculated, Is.EqualTo(Some.Text));
        }

        [Test]
        public void Description_IsNotInheritedFromParent_WhenSet()
        {
            _parent.Description.Value = Some.Text;
            _product.Description.Value = Some.OtherText;

            Assert.That(_product.Description.Calculated, Is.EqualTo(Some.OtherText));
        }

        [Test]
        public void Description_IsNull_WhenInheritedDescriptionIsNull()
        {
            _parent.Description.Reset();
            _product.Description.Reset();

            Assert.That(_product.Description.Calculated, Is.Null);
        }

        [Test]
        public void Description_Heritage_ContainsTwoLevels()
        {
            _parent.Description.Value = Some.Text;
            _product.Description.Value = Some.OtherText;
            var heritage = _product.Description.Heritage.ToList();
            Assert.That(heritage.Count, Is.EqualTo(2));
        }

        [Test]
        public void Description_Heritage_FirstIsFromProduct()
        {
            _parent.Description.Value = Some.Text;
            _product.Description.Value = Some.OtherText;
            var heritage = _product.Description.Heritage.ToList();
            var first = heritage.First();
            Assert.That(first.Source, Is.EqualTo(_product.Level));
            Assert.That(first.Value, Is.EqualTo(Some.OtherText));
        }
    }
}