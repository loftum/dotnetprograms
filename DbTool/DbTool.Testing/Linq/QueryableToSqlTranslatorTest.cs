﻿using System.Linq;
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
        public void Translate_ReturnSelectAsterisk_ForSimpleQuery()
        {
            var query = Linq<Hest>();
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Hest"));
        }

        [Test]
        public void Translate_ReturnSelectProperties_ForSelectType()
        {
            var query = Linq<Hest>().Select(p => p);
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select Age, Name from Hest"));
        }

        [Test]
        public void Translate_GetsSimpleSelectWithWhere()
        {
            var query = Linq<Hest>()
                .Where(h => h.Name == "Per");
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Hest where (Name = @p1)"));
        }

        [Test]
        public void Translate_GetsWhereWithAndClause()
        {
            var query = Linq<Hest>()
                .Where(h => h.Name == "Per" && h.Age == 21);
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Hest where ((Name = @p1) and (Age = @p2))"));
            Assert.That(result.Parameters["@p1"].Value, Is.EqualTo("Per"));
            Assert.That(result.Parameters["@p2"].Value, Is.EqualTo(21));
        }

        [Test]
        public void Translate_GetsPropertiesFromAnonymousSelectObject()
        {
            var query = Linq<Hest>()
                .Where(h => h.Name == "Blakken")
                .Select(h => new {h.Name});
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select Name from Hest where (Name = @p1)"));
        }

        [Test]
        public void Translate_GetSingleSelectProperty()
        {
            var query = Linq<Hest>()
                .Where(h => h.Name == "Blakken")
                .Select(h => h.Name);
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select Name from Hest where (Name = @p1)"));
        }

        [Test]
        public void Translate_GetsWhereWithOrClause()
        {
            var query = Linq<Hest>()
                .Where(h => h.Name == "Per" || h.Age == 21);
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Hest where ((Name = @p1) or (Age = @p2))"));
        }

        [Test]
        public void Translate_GetsWhereWithAndOrClause()
        {
            var query = Linq<Hest>()
                .Where(h => (h.Name == "Per" && h.Age == 21) || h.Age == 22);
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Hest where (((Name = @p1) and (Age = @p2)) or (Age = @p3))"));
        }

        [Test]
        public void DoubleWhere_AndsTogetherConditions()
        {
            var query = Linq<Hest>()
                .Where(h => (h.Name == "Per"))
                .Where(h => h.Age == 42);
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select * from Hest where (Name = @p1) and (Age = @p2)"));
        }

        [Test]
        public void Translate_TranslatesTakeToTop()
        {
            var query = Linq<Hest>()
                .Where(h => (h.Name == "Per"))
                .Take(42);
            var result = _translator.Translate(query);
            Assert.That(result.CommandText, Is.EqualTo("select top 42 * from Hest where (Name = @p1)"));
        }

        private static IQueryable<T> Linq<T>()
        {
            var mocker = new AutoMoqer();
            var provider = mocker.Create<DbToolQueryProvider>();
            return new DbToolQueryable<T>(provider);
        }
    }
}