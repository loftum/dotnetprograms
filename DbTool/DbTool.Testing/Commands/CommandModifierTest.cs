using DbTool.Lib.Communication.DbCommands.Modifiers;
using NUnit.Framework;

namespace DbTool.Testing.Commands
{
    [TestFixture]
    public class CommandModifierTest
    {
        [Test]
        public void Modify_ShouldReturnNull_WhenInputIsNull()
        {
            var modified = CommandModifier.Modify(null);
            Assert.That(modified, Is.Null);
        }

        [Test]
        public void Modify_ShouldReturnEmpty_WhenInputIsEmpty()
        {
            var modified = CommandModifier.Modify(string.Empty);
            Assert.That(modified, Is.Empty);
        }

        [Test]
        public void Modify_ShouldNotModifyOtherThanDollarExpressions()
        {
            const string notDollarExpression = "from h in hests select h";
            var modified = CommandModifier.Modify(notDollarExpression);
            Assert.That(modified, Is.EqualTo(notDollarExpression));
        }

        [Test]
        public void Modify_ShouldChangeDollarToQuery()
        {
            var modified = CommandModifier.Modify("$(table)");
            Assert.That(modified, Is.StringStarting("Query("));
        }

        [Test]
        public void Modify_ShouldReplaceSingleWordWithSelectAsteriskFromTable()
        {
            var modified = CommandModifier.Modify("$(table)");
            Assert.That(modified, Is.StringEnding("(\"select * from table\")"));
        }

        [Test]
        public void Modify_ShouldAddFnuttsToSelect()
        {
            var modified = CommandModifier.Modify("$(select * from table)");
            Assert.That(modified, Is.StringEnding("(\"select * from table\")"));
        }

        [Test]
        public void Modify_ShouldFillEmptyParanthesisWithFnutts()
        {
            var modified = CommandModifier.Modify("$()");
            Assert.That(modified, Is.StringEnding("(\"\")"));
        }

        [Test]
        public void Modify_ShouldModifyMultipleQueries()
        {
            const string command = "from h in $(hest) where $(select Name from HestType).Any(n => n.Equals(h.Type)) select h";
            var modified = CommandModifier.Modify(command);
            Assert.That(modified, Is.EqualTo("from h in Query(\"select * from hest\") where Query(\"select Name from HestType\").Any(n => n.Equals(h.Type)) select h"));
        }
    }
}