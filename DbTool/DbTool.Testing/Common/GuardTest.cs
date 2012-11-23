using System;
using DotNetPrograms.Common.Validation;
using NUnit.Framework;

namespace DbTool.Testing.Common
{
    [TestFixture]
    public class GuardTest
    {
        [Test]
        public void NotNull_ShouldThrowException_WithArgumentName()
        {
            Assert.That(() => SomeMethod(null), Throws.InstanceOf<ArgumentNullException>().With.Message.Contains("someArg"));
        }

        [Test]
        public void NotNull_ShouldNotThrowAnything_whenArgumentIsNotNull()
        {
            Assert.That(() => SomeMethod("hest"), Throws.Nothing);
        }

        private static void SomeMethod(string someArg)
        {
            Guard.NotNull(() => someArg);
        }
    }
}