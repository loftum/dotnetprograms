using System.ComponentModel;

namespace DbToolGui.Exceptions
{
    public enum ExceptionType
    {
        [Description("Already connected to {0}")]
        AlreadyConnected
    }
}