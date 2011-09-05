using System.Collections.ObjectModel;
using System.Linq;
using DbToolGui.ExtensionMethods;

namespace DbToolGui.Data
{
    public class Record
    {
        public ObservableCollection<Property> Properties { get; private set; }

        public Record(params Property[] properties)
        {
            Properties = new ObservableCollection<Property>();
            Properties.AddRange(properties);
        }

        public void Add(Property property)
        {
            Properties.Add(property);
        }

        public object Get(string columnName)
        {
            var property = Properties.Where(p => p.Name.Equals(columnName)).FirstOrDefault();
            return property == null
                ? null
                : property.Value;
        }
    }
}