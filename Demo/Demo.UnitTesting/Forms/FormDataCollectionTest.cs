using System.Collections.Generic;
using Demo.Core.Forms;
using NUnit.Framework;

namespace Demo.UnitTesting.Forms
{
    [TestFixture]
    public class FormDataCollectionTest
    {
        [Test]
        public void Should()
        {
            var data = new Dictionary<string, string>();
            data["order.OrderId"] = "OrderId";
            data["order.Items[0].Description"] = "Item 1";
            data["order.Items[1].Description"] = "Item 2";

            var tree = new FormDataCollection(data);

        }
    }
}