using System;
using System.Collections;
using System.IO;
using System.Text;
using Mono.CSharp;
using NUnit.Framework;

namespace DbTool.Testing.CSharp
{
    [TestFixture]
    public class EvaluatorTest
    {
        private Evaluator _evaluator;

        [SetUp]
        public void Setup()
        {
            var builder = new StringBuilder();
            var writer = new StringWriter(builder);
            var printer = new StreamReportPrinter(writer);
            var settings = new CompilerSettings();
            var context = new CompilerContext(settings, printer);
            _evaluator = new Evaluator(context);
        }

        [Test]
        public void ShouldDoSomething()
        {
//            string prefix;
//            var completions = _evaluator.GetCompletions("var ", out prefix);
//            Print(prefix);
//            Print(completions);
        }

        private static void Print(IEnumerable values)
        {
            if (values == null)
            {
                Print(null);
                return;
            }
            foreach (var value in values)
            {
                Print(value);
            }
        }

        private static void Print(object obj)
        {
            Console.WriteLine(string.Format("Object: {0}", obj));
        }
    }
}