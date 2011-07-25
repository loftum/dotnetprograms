using System.ComponentModel;

namespace StuffLibrary.Models.Messages
{
    public enum MessageType
    {
        [Description("success")]
        Success,
        [Description("info")]
        Info,
        [Description("warning")]
        Warning,
        [Description("error")]
        Error
    }
}