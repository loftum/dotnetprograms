using System.Linq;
using MasterData.Core.Domain.MasterData;
using MasterData.UnitTesting.TestData;
using NUnit.Framework;

namespace MasterData.UnitTesting.Core.Domain
{
    [TestFixture]
    public class ProductTest
    {
        private ProductMaster _master;
        private ProductVariant _variant;

        [SetUp]
        public void Setup()
        {
            _master = new ProductMaster();
            _variant = new ProductVariant();
            _master.Add(_variant);
        }

        [Test]
        public void Description_IsInheritedFromParent_WhenNotSet()
        {
            _master.Description = Some.Text;

            Assert.That(_variant.GetDescription().Value, Is.EqualTo(Some.Text));
        }

        [Test]
        public void Description_IsNotInheritedFromParent_WhenSet()
        {
            _master.Description = Some.Text;
            _variant.Description= Some.OtherText;

            Assert.That(_variant.GetDescription().Value, Is.EqualTo(Some.OtherText));
        }

        [Test]
        public void Description_IsNull_WhenInheritedDescriptionIsNull()
        {
            _master.Description = null;
            _variant.Description = null;

            Assert.That(_variant.GetDescription().Value, Is.Null);
        }

        [Test]
        public void Description_Heritage_ContainsTwoLevels()
        {
            _master.Description = Some.Text;
            _variant.Description = Some.OtherText;
            var heritage = _variant.GetDescription().Heritage.ToList();
            Assert.That(heritage.Count, Is.EqualTo(2));
        }

        [Test]
        public void Description_Heritage_FirstIsFromProduct()
        {
            _master.Description = Some.Text;
            _variant.Description = Some.OtherText;
            var heritage = _variant.GetDescription().Heritage.ToList();
            var first = heritage.First();
            Assert.That(first.Source, Is.EqualTo(_variant.Level));
            Assert.That(first.OwnValue, Is.EqualTo(Some.OtherText));
        }
    }
}