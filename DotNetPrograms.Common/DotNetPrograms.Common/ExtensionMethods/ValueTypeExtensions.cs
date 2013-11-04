namespace DotNetPrograms.Common.ExtensionMethods
{
    public static class ValueTypeExtensions
    {
        public static bool IsDefault<T>(this T value) where T : struct
        {
            return value.Equals(default(T));
        }
    }
}