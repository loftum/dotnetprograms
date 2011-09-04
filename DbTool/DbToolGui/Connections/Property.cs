using System.ComponentModel;

namespace DbToolGui.Connections
{
    public class Property : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; private set; }
        public object Value { get; private set; }

        public Property(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}