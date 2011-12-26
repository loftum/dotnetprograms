using System.Collections.Generic;
using DbTool.Commands;
using DbTool.Lib.ExtensionMethods;
using DbTool.Testing.Assertions;
using DbTool.Testing.TestData;
using NUnit.Framework;

namespace DbTool.Testing.Commands
{
    [TestFixture]
    public class CommandArgsTest
    {
        [Test]
        public void Ctor_ShouldSetCommand()
        {
            var args = new CommandArgs(ArgumentList());

            Assert.That(args.Command, Is.EqualTo(Some.Command));
        }

        [Test]
        public void Ctor_ShouldSetArguments()
        {
            var args = new CommandArgs(ArgumentList(Some.Argument, Some.OtherArgument));

            MakeSure.ThatCollection(args.Arguments).ContainsOnly(Some.Argument, Some.OtherArgument);
        }

        [Test]
        public void Ctor_ShouldSetFlags()
        {
            var args = new CommandArgs(ArgumentList(Some.Flag, Some.DoubleFlag));

            MakeSure.ThatCollection(args.Flags).ContainsOnly(Some.Flag);
        }

        [Test]
        public void Ctor_ShouldSetDoubleFlags()
        {
            var args = new CommandArgs(ArgumentList(Some.Flag, Some.DoubleFlag));

            MakeSure.ThatCollection(args.DoubleFlags).ContainsOnly(Some.DoubleFlag);
        }

        [Test]
        public void Ctor_ShouldSetHelp_WhenThereIsHelpFlag()
        {
            var args = new CommandArgs(ArgumentList(CommandArgs.HelpFlag));

            Assert.That(args.Help);
        }

        [Test]
        public void Ctor_ShouldNotSetHelp_WhenThereIsNoHelpFlag()
        {
            var args = new CommandArgs(ArgumentList(Some.Argument, Some.Flag, Some.DoubleFlag));
            Assert.That(args.Help, Is.False);
        }

        [Test]
        public void HasFlags_ShouldBeTrue_WhenThereIsFlag()
        {
            var args = new CommandArgs(ArgumentList(Some.Flag));
            Assert.That(args.HasFlags);
        }

        [Test]
        public void HasFlags_ShouldBeFalse_WhenThereIsNoFlag()
        {
            var args = new CommandArgs(ArgumentList(Some.Argument));
            Assert.That(args.HasFlags, Is.False);
        }

        [Test]
        public void HasArguments_ShouldBeFalse_WhenThereIsNoArgument()
        {
            var args = new CommandArgs(ArgumentList(Some.Flag, Some.DoubleFlag));
            Assert.That(args.HasArguments, Is.False);
        }

        [Test]
        public void HasArguments_ShouldBeTrue_WhenThereIsArgument()
        {
            var args = new CommandArgs(ArgumentList(Some.Argument));
            Assert.That(args.HasArguments);
        }

        private static IEnumerable<string> ArgumentList(params string[] values)
        {
            return Some.Command.ToListWith(values);
        }
    }
}