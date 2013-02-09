using AutoMoq;
using CodeGenerator.Lib.Generating;
using NUnit.Framework;

namespace CodeGenerator.UnitTesting.Lib.Generating
{
    [TestFixture]
    public class TemplateParserTest
    {
        private TemplateParser _parser;

        [SetUp]
        public void Setup()
        {
            var mocker = new AutoMoqer();
            _parser = mocker.Create<TemplateParser>();
        }

        [Test]
        public void Should()
        {
            const string input = "do da #0 la #1 la";
            var record = new Record("hest", "ku");
            var code = _parser.Parse(input, record);

            Assert.That(code, Is.EqualTo("do da hest la ku la"));
        }
    }
}