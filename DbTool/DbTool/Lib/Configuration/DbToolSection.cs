using System.Collections.Generic;

namespace DbTool.Lib.Configuration
{
    public class DbToolSection
    {
        public string Name { get; set; }
        public IDictionary<string, string> Settings { get; set; }

        public DbToolSection()
        {
            Settings = new Dictionary<string, string>();
        }
    }
}