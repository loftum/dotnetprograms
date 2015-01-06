using System.Configuration;

namespace MongoTool.Core.Configuration
{
    public class UsingNamespace : ConfigurationElement
    {
        [ConfigurationProperty("namespace")]
        public string Namespace
        {
            get { return (string)this["namespace"]; }
            set { this["namespace"] = value; }
        }
    }
}