using System;
using System.Collections.Generic;
using System.Linq;
using DotNetPrograms.Common.Mapping;
using DotNetPrograms.Common.UnitTests.TestData;
using NUnit.Framework;

namespace DotNetPrograms.Common.UnitTests.Common.Mapping
{
    [TestFixture]
    public class MapTest
    {
        [Test]
        public void MapsSimpleProperties()
        {
            var map = new Mapper<Order, OrderDto>();
            Console.WriteLine(map.Mappings);

            var order = new Order
                {
                    OrderId = Some.Number,
                    Number = null,
                    Customer = new Customer
                        {
                            FirstName = Some.FirstName,
                            LastName = Some.LastName
                        }
                };

            var dto = map.Map(order);
            Assert.That(dto.OrderId, Is.EqualTo(order.OrderId));
            Assert.That(dto.Number, Is.Null);
        }

        [Test]
        public void MapsComplexProperties()
        {
            var map = new Mapper<Order, OrderDto>();

            var customer = new Customer
                {
                    FirstName = Some.FirstName,
                    LastName = Some.LastName
                };
            var order = new Order
            {
                Customer = customer
            };

            var dto = map.Map(order);

            Assert.That(dto.Customer.FirstName, Is.EqualTo(customer.FirstName));
            Assert.That(dto.Customer.LastName, Is.EqualTo(customer.LastName));
        }

        [Test]
        public void MapsCollections()
        {
            var map = new Mapper<Order, OrderDto>();

            var order = new Order
            {
                Lines = new List<OrderLine>
                    {
                        new OrderLine{Description = Some.Text, ProductId = 1, Price = new Price(Some.Amount, Some.OtherAmount)},
                        new OrderLine{Description = Some.OtherText, Price = new Price(Some.OtherAmount, Some.Amount)}
                    }
            };
            var dto = map.Map(order);
            Assert.That(dto.Lines.Count, Is.EqualTo(2));

            var firstLine = dto.Lines.First();
            Assert.That(firstLine.Description, Is.EqualTo(Some.Text));
            Assert.That(firstLine.Price.IncVat, Is.EqualTo(Some.Amount));
            Assert.That(firstLine.Price.ExVat, Is.EqualTo(Some.OtherAmount));
            Assert.That(firstLine.ProductId, Is.EqualTo("1"));
        }


        [Test]
        public void ShouldValidate()
        {
            var map = new Mapper<Order, OrderDto>();

            var order = new Order
            {
                OrderId = 42
            };

            var dto = map.Map(order);
            map.ValidateProperties(order, dto);
        }
    }
}