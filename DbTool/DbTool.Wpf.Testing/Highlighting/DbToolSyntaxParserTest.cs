using DbTool.Lib.CSharp;
using DbTool.Lib.Meta.Types;
using DbTool.Lib.Syntax;
using DbTool.Testing.TestData;
using Moq;
using NUnit.Framework;

namespace DbTool.Wpf.Testing.Highlighting
{
    [TestFixture]
    public class DbToolSyntaxParserTest
    {
        private Mock<ISyntaxProvider> _syntaxProviderMock;
        private Mock<ICSharpEvaluator> _evaluator;
        private DbToolSyntaxParser _parser;
        private TableMeta _object;

        [SetUp]
        public void Setup()
        {
            _object = new TableMeta(Some.Name);
            _syntaxProviderMock = new Mock<ISyntaxProvider>();
            _syntaxProviderMock.Setup(p => p.IsSeparator('.')).Returns(true);
            _syntaxProviderMock.Setup(p => p.IsSeparator(' ')).Returns(true);
            _evaluator = new Mock<ICSharpEvaluator>();
            _parser = new DbToolSyntaxParser(_syntaxProviderMock.Object, _evaluator.Object);
        }

        [Test]
        public void FindSuggestions_ShouldCallSyntaxProvider()
        {
            _parser.FindSuggestions("person", 4);
            _syntaxProviderMock.Verify(p => p.GetType("person"));
        }

        [Test]
        public void FindSuggestions_ShouldAddPropertiesToSuggestionList()
        {
            _object.AddColumn(new ColumnMeta("varchar", "FirstName"));
            _object.AddColumn(new ColumnMeta("varchar", "LastName"));
            _syntaxProviderMock.Setup(p => p.GetType("person")).Returns(_object);

            _parser.FindSuggestions("person", 4);
            Assert.That(_parser.Suggestions, Has.Count.EqualTo(2));
        }
    }
}