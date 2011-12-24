using System.ComponentModel;

namespace DbTool.Lib.Exceptions
{
    public enum ExceptionType
    {
        [Description("Already connected to {0}")]
        AlreadyConnected,
        [Description("Not connected to database")]
        NotConnected,
        [Description("Unknown database command '{0}'")]
        UnknownDatabaseCommand,
        [Description("Void?")]
        NoSettingGiven,
        [Description("{0}? Never heard about.")]
        UnknownSetting,
        [Description("There is no context defined. Create a context.")]
        NoContextExists
    }
}