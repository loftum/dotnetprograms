using System;
using CodeGenerator.Lib.Generating;
using CodeGenerator.UnitTesting.TestData;
using DotNetPrograms.Common.Collections.Chunking;
using NUnit.Framework;

namespace CodeGenerator.UnitTesting.Lib.Generating
{
    [TestFixture]
    public class RecordTest
    {
        [Test]
        public void RecordNumber_IsSameAsChunkNumber()
        {
            var chunk = new Chunk<string>(new[] {"a;b;c", "d;e;f"}, Some.Number);
            var record = new Record(chunk, ";");

            Assert.That(record.Number, Is.EqualTo(Some.Number));
        }

        [Test]
        public void RecordText_IsStringJoinedByNewLine()
        {
            var chunk = new Chunk<string>(new[] { "a;b;c", "d;e;f" }, Some.Number);
            var record = new Record(chunk, ";");

            Assert.That(record.Text, Is.EqualTo("a;b;c" + Environment.NewLine + "d;e;f"));
        }

        [Test]
        public void Record_IndexesParameters()
        {
            var chunk = new Chunk<string>(new[] { "a;b;c", "d;e;f" }, Some.Number);
            var record = new Record(chunk, ";");

            Assert.That(record[2], Is.EqualTo("c"));
        }

        [Test]
        public void Record_ReturnsEmptyString_IfParameterNumberDoesNotExist()
        {
            var chunk = new Chunk<string>(new[] { "a;b;c", "d;e;f" }, Some.Number);
            var record = new Record(chunk, ";");

            Assert.That(record[50], Is.EqualTo(""));
        }
    }
}