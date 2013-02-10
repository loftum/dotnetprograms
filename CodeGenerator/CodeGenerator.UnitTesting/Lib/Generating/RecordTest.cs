using System;
using CodeGenerator.Lib.Generating;
using CodeGenerator.Lib.Text;
using NUnit.Framework;

namespace CodeGenerator.UnitTesting.Lib.Generating
{
    [TestFixture]
    public class RecordTest
    {
        [Test]
        public void Record_IndexesParameters()
        {
            var block = new TextBlock(0, "a;b;c" + Environment.NewLine + "d;e;f", 0);
            var record = new Record(block, ";");

            Assert.That(record[2], Is.EqualTo("c"));
        }

        [Test]
        public void Record_ReturnsEmptyString_IfParameterNumberDoesNotExist()
        {
            var block = new TextBlock(0, "a;b;c" + Environment.NewLine + "d;e;f", 0);
            var record = new Record(block, ";");

            Assert.That(record[50], Is.EqualTo(""));
        }
    }
}