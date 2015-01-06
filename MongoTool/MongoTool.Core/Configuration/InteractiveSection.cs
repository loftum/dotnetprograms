using System.Configuration;

namespace MongoTool.Core.Configuration
{
    public class InteractiveSection : ConfigurationSection
    {
        [ConfigurationProperty("assemblies")]
        public AssemblyCollection Assemblies
        {
            get { return (AssemblyCollection) this["assemblies"]; }
            set { this["assemblies"] = value; }
        }

        [ConfigurationProperty("namespaces")]
        public NamespaceCollection Namespaces
        {
            get { return (NamespaceCollection) this["namespaces"]; }
            set { this["namespaces"] = value; }
        }
    }
}