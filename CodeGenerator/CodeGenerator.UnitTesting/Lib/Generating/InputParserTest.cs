using System.Linq;
using System.Text;
using AutoMoq;
using CodeGenerator.Lib.Generating;
using DotNetPrograms.Common.ExtensionMethods;
using NUnit.Framework;

namespace CodeGenerator.UnitTesting.Lib.Generating
{
    [TestFixture]
    public class InputParserTest
    {
        private InputParser _parser;

        [SetUp]
        public void Setup()
        {
            var mocker = new AutoMoqer();
            _parser = mocker.Create<InputParser>();
        }

        [Test]
        public void Parse_ReturnsRecord()
        {
            const string input = @"a b c";
            var records = _parser.Parse(input, 1, " ");
            Assert.That(records.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Record_IsIndexed()
        {
            const string input = @"a b c";
            var record = _parser.Parse(input, 1, " ").First();
            Assert.That(record[0], Is.EqualTo("a"));
            Assert.That(record[1], Is.EqualTo("b"));
            Assert.That(record[2], Is.EqualTo("c"));
        }

        [Test]
        public void Parse_ReturnsOneRecordPerChunksOfLines()
        {
            var input = new StringBuilder()
                .AppendLine("a b c")
                .AppendLine("d e f")
                .Append("g h i").ToString();

            var records = _parser.Parse(input, 2, " ");
            Assert.That(records.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Parse_IncludesEmptyLines()
        {
            var input = new StringBuilder()
                .AppendLine("a b c")
                .AppendLine()
                .AppendLine("d e f")
                .AppendLine()
                .AppendLine("g h i").ToString();

            var records = _parser.Parse(input, 2, " ");
            Assert.That(records.Count(), Is.EqualTo(3));
        }

        [Test]
        public void Should()
        {
            var input = new StringBuilder()
                .AppendLine("a b c")
                .AppendLine()
                .AppendLine("d e f")
                .AppendLine()
                .AppendLine("g h i").ToString();

            var lines = input.SplitLines(true);
            var chunks = lines.InChunksOf(2);
            

            var list = chunks.ToList();
            Assert.That(list.Count, Is.EqualTo(3));

        }
    }
}