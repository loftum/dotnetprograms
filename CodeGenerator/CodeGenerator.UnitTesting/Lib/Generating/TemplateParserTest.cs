using AutoMoq;
using CodeGenerator.Lib.CSharp;
using CodeGenerator.Lib.Generating;
using CodeGenerator.Lib.Text;
using DotNetPrograms.Common.Collections.Chunking;
using Moq;
using NUnit.Framework;

namespace CodeGenerator.UnitTesting.Lib.Generating
{
    [TestFixture]
    public class TemplateParserTest
    {
        private TemplateEvaluator _templateEvaluator;
        private Mock<ICSharpEvaluator> _evaluator;

        [SetUp]
        public void Setup()
        {
            var mocker = new AutoMoqer();
            _templateEvaluator = mocker.Create<TemplateEvaluator>();
            _evaluator = mocker.GetMock<ICSharpEvaluator>();
            _evaluator.Setup(e => e.Run(It.IsAny<string>())).Returns(EmptyResult);
        }

        private static CSharpResult EmptyResult()
        {
            return new CSharpResult(false, null, null, null);
        }

        [Test]
        public void Parse_ReplacesWildcards()
        {
            const string input = "insert into #0 (name) values ('#1')";
            var record = GetRecord("Person", "Jens");
            var code = _templateEvaluator.Evaluate(input, record);

            Assert.That(code, Is.EqualTo("insert into Person (name) values ('Jens')"));
        }

        [Test]
        public void Parse_EvaluatesCode()
        {
            
            const string input = "insert into @{\"#0\".ToUpper()} (name) values ('#1')";
            var record = GetRecord("Person", "Jens");
            _templateEvaluator.Evaluate(input, record);

            _evaluator.Verify(e => e.Run("\"Person\".ToUpper()"));
        }

        [Test]
        public void Parse_ReplacesCode()
        {
            const string input = "insert into @{\"#0\".ToUpper()} (name) values ('#1')";
            _evaluator.Setup(e => e.Run("\"Person\".ToUpper()")).Returns(new CSharpResult(true, "PERSON", "", ""));
            var record = GetRecord("Person", "Jens");
            var result = _templateEvaluator.Evaluate(input, record);

            Assert.That(result, Is.EqualTo("insert into PERSON (name) values ('Jens')"));
        }

        private static Record GetRecord(params string[] values)
        {
            var block = new TextBlock(0, string.Join(",", values), 0);
            return new Record(block, ",");
        }
    }
}