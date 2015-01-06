using System.Configuration;

namespace MongoTool.Core.Configuration
{
    [ConfigurationCollection(typeof(AssemblyElement))]
    public class AssemblyCollection : ConfigurationElementCollection<AssemblyElement>
    {
        public AssemblyCollection() : base("load")
        {
        }

        protected override object GetElementKey(AssemblyElement element)
        {
            return element.Path;
        }
    }
}