using System.ComponentModel;

namespace VisualFarmStudio.Common.Exceptions
{
    public enum ExceptionType
    {
        [Description("Brukernavnet er tatt")]
        BrukernavnIsTaken,
        [Description("Ugyldig bruker")]
        InvalidCredentials
    }
}