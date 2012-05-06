using System;
using System.Collections;
using System.IO;
using System.Text;
using Mono.CSharp;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace DbTool.Testing.CSharp
{
    [TestFixture]
    public class EvaluatorTest
    {
        private readonly string _hestNameSpace = typeof (Hest).Namespace;
        private Evaluator _evaluator;

        [SetUp]
        public void Setup()
        {
            var builder = new StringBuilder();
            var writer = new StringWriter(builder);
            var printer = new StreamReportPrinter(writer);
            var settings = new CompilerSettings();
            settings.AssemblyReferences.Add("DbTool.Testing");
            
            var context = new CompilerContext(settings, printer);
            _evaluator = new Evaluator(context);
        }

        [Test]
        public void ShouldGetCompletions()
        {
            string prefix;
            _evaluator.Run(string.Format("using {0};", _hestNameSpace));
            _evaluator.Run("var hest = new Hest();");
            var completions = _evaluator.GetCompletions("hest.N", out prefix);
            Print(prefix);
            Print(completions);
            Assert.That(prefix, Is.EqualTo("N"));
            Assert.That(completions.Length, Is.EqualTo(2));
            Assert.That(completions, Contains.Item("ame").And.Contains("umber"));
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

        private static void Print(string s)
        {
            Console.WriteLine(string.Format("string: {0}", s));
        }

        private static void Print(object obj)
        {
            Console.WriteLine(string.Format("{0}: {1}", obj.GetType(), obj));
        }
    }
}