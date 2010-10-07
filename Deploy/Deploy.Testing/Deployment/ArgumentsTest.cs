using Deploy.Lib.Deployment;
using NUnit.Framework;

namespace Deploy.Testing.Deployment
{
    [TestFixture]
    public class ArgumentsTest
    {
        [Test]
        public void ShouldGetArgumentByName()
        {
            var args = new[] {"name1:arg1", "name2:arg2"};
            var arguments = new Arguments(args);
            Assert.That(arguments.ByNameOrIndex("name1", 0), Is.EqualTo("arg1"));
        }

        [Test]
        public void ShouldGetArgumentByIndexIfThereIsNoName()
        {
            var args = new[] { "arg1", "arg2" };
            var arguments = new Arguments(args);
            Assert.That(arguments.ByNameOrIndex("name1", 0), Is.EqualTo("arg1"));
        }
    }
}
