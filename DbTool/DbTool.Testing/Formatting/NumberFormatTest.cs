using NUnit.Framework;
using Moq;

namespace DbTool.Testing.Formatting
{
    [TestFixture]
    public class NumberFormatTest
    {
        [Test]
        public void ShouldThousandSeparateNumbers()
        {
            var formatted = string.Format("{0:0,0}", 9999999);
            Assert.That(formatted, Is.StringMatching(@"9\s{1}999\s{1}999"));
        }
    }
}