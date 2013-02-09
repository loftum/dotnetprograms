using System.IO;
using Mono.CSharp;

namespace CodeGenerator.Lib.CSharp
{
    public class CodeGeneratorInteractive
    {
        public static Evaluator Evaluator;
        public static TextWriter Output = new StringWriter();

        public static string vars
        {
            get { return Evaluator.GetVars(); }
        }

        public static string usings
        {
            get { return Evaluator.GetUsing(); }
        }
    }
}