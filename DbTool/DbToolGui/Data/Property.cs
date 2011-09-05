using System;
using System.ComponentModel;

namespace DbToolGui.Data
{
    public class Property : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; private set; }
        public object Value { get; private set; }
        public string ValueAsString { get { return ParseValue(); } }

        public Property(string name, object value)
        {
            Name = name;
            Value = value;
        }

        private string ParseValue()
        {
            if (Value == null)
            {
                return "(null)";
            }
            if (Value is byte[])
            {
                return Convert.ToBase64String((byte[]) Value);
            }
            return Value.ToString();
        }
    }
}