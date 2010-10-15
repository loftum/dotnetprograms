using Deploy.Lib.RegularExpressions;
using NUnit.Framework;

namespace Deploy.Testing.Deploy.Lib.RegularExpressions
{
    [TestFixture]
    public class RegexifierTest
    {
        [Test]
        public void ShouldNotDoAnythingIfRegexIsValid()
        {
            const string candidate = @"\*\.";
            var pattern = Regexifier.Regexify(candidate).Pattern;
            Assert.That(pattern, Is.EqualTo(candidate));
        }

        [Test]
        public void ShouldAddStartAndEnd()
        {
            var pattern = Regexifier.Regexify("abc").WithStartAndEnd().Pattern;
            Assert.That(pattern, Is.EqualTo("^abc$"));
        }

        [Test]
        public void ShouldAppendTrailingWildard()
        {
            var pattern = Regexifier.Regexify("abc").WithTrailingWildcard().Pattern;
            Assert.That(pattern, Is.EqualTo(@"abc\.*"));
        }
    }
}