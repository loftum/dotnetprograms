using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.Lib.SummaryFormatting
{
    public class Summary
    {
        public Summary Parent { get; set; }
        public ISummaryFormatter Formatter { get; set; }
        private string _title;
        
        private readonly IList<Summary> _sections = new List<Summary>();
        private readonly IDictionary<string, object> _values = new Dictionary<string, object>();

        public Summary()
        {
        }

        public Summary(ISummaryFormatter formatter, Summary parent = null)
        {
            Formatter = formatter;
            Parent = parent;
        }

        public Summary WithTitle(string title)
        {
            _title = title;
            return this;
        }

        private int GetLevel()
        {
            return Parent == null ? 0 : Parent.GetLevel() + 1;
        }

        public Summary With(Summary section)
        {
            section.Parent = this;
            section.Formatter = Formatter;
            _sections.Add(section);
            return this;
        }

        public Summary NewSection()
        {
            return new Summary(Formatter, this);
        }

        public override string ToString()
        {
            var builder = new StringBuilder(Formatter.FormatTitle(_title, GetLevel())).AppendLine();
            foreach(var value in _values)
            {
                builder.AppendLine(Formatter.FormatValue(value, GetLevel()));
            }
            builder.AppendLine();
            foreach(var section in _sections)
            {
                builder.AppendLine(section.ToString());
            }
            return builder.ToString();
        }

        public Summary WithValue(string name, string value)
        {
            _values.Add(name, value);
            return this;
        }
    }
}