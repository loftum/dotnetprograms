using System.Configuration;
using MongoTool.Core.Configuration;
using MongoTool.Core.CSharp;
using StructureMap.Configuration.DSL;

namespace MongoTool.Ioc
{
    public class MongoToolRegistry : Registry
    {
        public MongoToolRegistry()
        {
            Scan(s =>
            {
                s.Assembly("MongoTool");
                s.Assembly("MongoTool.Core");
                foreach (var assembly in AssemblyLoader.NewAssemblies)
                {
                    s.Assembly(assembly);
                }
                s.WithDefaultConventions();
            });

            For<InteractiveSection>().Use("InteractiveSection", GetInteractiveSection).Singleton();
            For<ICSharpEvaluator>().Use(Interactive.Evaluator).Singleton();
        }

        private static InteractiveSection GetInteractiveSection()
        {
            return (InteractiveSection)ConfigurationManager.GetSection("interactive");
        }
        
    }
}