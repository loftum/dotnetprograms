using System.ComponentModel;

namespace HourGlass.Lib.Exceptions
{
    public enum ExceptionType
    {
        [Description("Invalid hour code: {0}: {1}")]
        InvalidHourCode
    }
}