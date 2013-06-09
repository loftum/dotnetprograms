using System;
using System.Reflection;

namespace DbTool.IntegrationTests.TableProxies
{
    [Serializable]
    public class Resolver
    {
        public Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            Console.WriteLine(args.Name);
            Console.WriteLine(args.RequestingAssembly);
            return null;
        }
    }
}