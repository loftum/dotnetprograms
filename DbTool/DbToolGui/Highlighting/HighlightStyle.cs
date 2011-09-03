using System.Collections.Generic;
using System.Windows;

namespace DbToolGui.Highlighting
{
    public class HighlightStyle
    {
        public IDictionary<DependencyProperty, object> Properties { get; private set; }

        public HighlightStyle()
        {
            Properties = new Dictionary<DependencyProperty, object>();
        }

        public HighlightStyle With(DependencyProperty property, object value)
        {
            Properties[property] = value;
            return this;
        }
    }
}