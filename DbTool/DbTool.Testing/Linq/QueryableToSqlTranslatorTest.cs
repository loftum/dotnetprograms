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
            var query = Linq<Parent>().Select(h => h);
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Parent"));
        }

        [Test]
        public void Translate_GetsSimpleSelectWithWhere()
        {
            var query = Linq<Parent>().Where(h => h.Name == "Blakken");
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Hest where Name = 'Blakken'"));
        }

        private static IQueryable<T> Linq<T>()
        {
            var mocker = new AutoMoqer();
            var provider = mocker.Create<DbToolQueryProvider>();
            return new DbToolQueryable<T>(provider);
        }
    }
}