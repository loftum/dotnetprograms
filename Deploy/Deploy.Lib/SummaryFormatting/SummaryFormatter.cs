using System.Collections.Generic;
using System.Text;

namespace Deploy.Lib.SummaryFormatting
{
    public class SummaryFormatter : ISummaryFormatter
    {
        public string FormatTitle(string title, int level)
        {
            var builder = new StringBuilder();
            for (int ii=0; ii<level; ii++)
            {
                builder.Append("* ");
            }
            return builder.Append(title).ToString();
        }

        public string FormatValue(KeyValuePair<string, object> value, int level)
        {
            var builder = new StringBuilder();
            for (var ii=0; ii<level; ii++)
            {
                builder.Append("  ");
            }
            return builder.Append(value.Key).Append(": ").Append(value.Value).ToString();
        }
    }
}