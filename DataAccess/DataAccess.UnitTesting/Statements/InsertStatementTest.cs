using DataAccess.Sql.Statements;
using DataAccess.UnitTesting.TestData;
using NUnit.Framework;

namespace DataAccess.UnitTesting.Statements
{
    [TestFixture]
    public class InsertStatementTest
    {
        [Test]
        public void InsertStatement_CommandText()
        {
            var statement = InsertStatementTemplate.For<Person>();
            Assert.That(statement.CommandText, Is.EqualTo("insert into [Person] ([Id], [Name], [BirthDate]) values (@Id, @Name, @BirthDate)"));
        }

        [Test]
        public void InsertStatemtn_ForCustomTable()
        {
            var statement = InsertStatementTemplate.For<Person>("Balloon");
            Assert.That(statement.CommandText, Is.EqualTo("insert into [Balloon] ([Id], [Name], [BirthDate]) values (@Id, @Name, @BirthDate)"));
        }
    }
}