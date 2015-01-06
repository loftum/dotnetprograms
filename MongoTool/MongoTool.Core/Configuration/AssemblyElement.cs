using System.Configuration;

namespace MongoTool.Core.Configuration
{
    public class AssemblyElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsRequired = true)]
        public string Path
        {
            get { return (string)this["path"]; }
            set { this["path"] = value; }
        }
    }
}