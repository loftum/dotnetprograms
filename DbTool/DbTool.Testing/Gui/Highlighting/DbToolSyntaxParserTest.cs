using DbTool.Lib.Objects;
using DbTool.Lib.Objects.Database;
using DbTool.Lib.Ui.Syntax;
using DbTool.Testing.TestData;
using DbToolGui.Highlighting;
using NUnit.Framework;
using Moq;

namespace DbTool.Testing.Gui.Highlighting
{
    [TestFixture]
    public class DbToolSyntaxParserTest
    {
        private Mock<ISyntaxProvider> _syntaxProviderMock;
        private DbToolSyntaxParser _parser;
        private DbToolObject _object;

        [SetUp]
        public void Setup()
        {
            _object = new TableObject(Some.Namespace, Some.Name);
            _syntaxProviderMock = new Mock<ISyntaxProvider>();
            _syntaxProviderMock.Setup(p => p.IsSeparator('.')).Returns(true);
            _syntaxProviderMock.Setup(p => p.IsSeparator(' ')).Returns(true);
            _parser = new DbToolSyntaxParser(_syntaxProviderMock.Object);
        }

        [Test]
        public void FindSuggestions_ShouldCallSyntaxProvider()
        {
            _parser.FindSuggestions("person", 4);
            _syntaxProviderMock.Verify(p => p.GetObject("person"));
        }

        [Test]
        public void FindSuggestions_ShouldAddPropertiesToSuggestionList()
        {
            _object.AddProperty(new DbToolProperty("FirstName"));
            _object.AddProperty(new DbToolProperty("LastName"));
            _syntaxProviderMock.Setup(p => p.GetObject("person")).Returns(_object);

            _parser.FindSuggestions("person", 4);
            Assert.That(_parser.Suggestions, Has.Count.EqualTo(2));
        }

        [Test]
        public void Should()
        {
            
        }
    }
}