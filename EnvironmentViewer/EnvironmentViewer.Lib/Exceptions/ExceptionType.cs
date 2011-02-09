using System.ComponentModel;

namespace EnvironmentViewer.Lib.Exceptions
{
    public enum ExceptionType
    {
        [Description("Invalid databasetype: {0}")]
        InvalidDatabaseType
    }
}