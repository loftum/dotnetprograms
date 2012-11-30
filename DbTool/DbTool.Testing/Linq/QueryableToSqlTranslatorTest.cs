using System.Linq;
using AutoMoq;
using DbTool.Lib.Linq;
using NUnit.Framework;

namespace DbTool.Testing.Linq
{
    [TestFixture]
    public class QueryableToSqlTranslatorTest
    {
        private QueryableToSqlTranslator _translator;

        [SetUp]
        public void Setup()
        {
            _translator = new QueryableToSqlTranslator();
        }

        [Test]
        public void Translate_GetsSimpleSelect()
        {
            var query = Linq<Hest>();
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Hest"));
        }

        [Test]
        public void Translate_GetsSelect()
        {
            var query = Linq<Hest>().Select(p => p);
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Hest"));
        }

        [Test]
        public void Translate_GetsSimpleSelectWithWhere()
        {
            var query = Linq<Hest>()
                .Where(h => h.Name == "Per")
                .Select(h => h);
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Hest where (Name = 'Per')"));
        }

        [Test]
        public void Translate_GetsWhereWithAndClause()
        {
            var query = Linq<Hest>()
                .Where(h => h.Name == "Per" && h.Age == 21)
                .Select(p => p);
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Hest where ((Name = 'Per') and (Age = 21))"));
        }

        [Test]
        public void Translate_GetsWhereWithOrClause()
        {
            var query = Linq<Hest>()
                .Where(h => h.Name == "Per" || h.Age == 21)
                .Select(p => p);
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Hest where ((Name = 'Per') or (Age = 21))"));
        }

        [Test]
        public void ShouldDoSomething()
        {
            var query = Linq<Hest>()
                .Where(h => (h.Name == "Per" && h.Age == 21) || h.Age == 22)
                .Select(p => p);
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Hest where (((Name = 'Per') and (Age = 21)) or (Age = 22))"));
        }


        private static IQueryable<T> Linq<T>()
        {
            var mocker = new AutoMoqer();
            var provider = mocker.Create<DbToolQueryProvider>();
            return new DbToolQueryable<T>(provider);
        }
    }
}