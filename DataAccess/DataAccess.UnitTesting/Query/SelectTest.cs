using System;
using System.Linq;
using DataAccess.Sql.ExtensionMethods;
using DataAccess.Sql.Query;
using DataAccess.UnitTesting.TestData;
using NUnit.Framework;

namespace DataAccess.UnitTesting.Query
{
    [TestFixture]
    public class SelectTest
    {
        private TestQueryProvider _provider;

        [SetUp]
        public void Setup()
        {
            _provider = new TestQueryProvider();
        }

        [Test]
        public void Query()
        {
            var person = new Person();
            var result = Query<Person>()
                .OrderBy(p => p.FirstName)
                .Select(p => new Person{FirstName = p.FirstName + 1})
                .OrderByDescending(p => p.FirstName)
                .Select(p => p.BirthDate)
                .Skip(5)
                .Distinct()
                .ToList();

            Console.WriteLine(result.GetType().GetFriendlyName());
            var query = _provider.LastQuery;

            Console.WriteLine(query);
        }

        [Test]
        public void Delete()
        {
            var result = Query<Person>()
                .Where(p => p.FirstName.StartsWith("A"))
                .Delete();
        }

        private IQueryable<T> Query<T>()
        {
            return new DataAccessQueryable<T>(_provider);
        }
    }
}