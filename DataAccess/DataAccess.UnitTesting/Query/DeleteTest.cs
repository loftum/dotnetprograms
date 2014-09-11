using System;
using System.Linq;
using DataAccess.Sql.ExtensionMethods;
using DataAccess.Sql.Query;
using DataAccess.UnitTesting.TestData;
using NUnit.Framework;

namespace DataAccess.UnitTesting.Query
{
    [TestFixture]
    public class DeleteTest
    {
        private TestQueryProvider _provider;

        [SetUp]
        public void Setup()
        {
            _provider = new TestQueryProvider();
        }

        [Test]
        public void Delete()
        {
            Query<Person>()
                .Where(p => p.FirstName == "Knut")
                .Delete();
            
            var query = _provider.LastDelete;
            Console.WriteLine(query);
        }

        private IQueryable<T> Query<T>()
        {
            return new DataAccessQueryable<T>(_provider);
        }
    }
}