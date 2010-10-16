using System.Collections.Generic;

namespace Deploy.Lib.SummaryFormatting
{
    public interface ISummaryFormatter
    {
        string FormatTitle(string title, int level);
        string FormatValue(KeyValuePair<string, object> value, int level);
    }
}