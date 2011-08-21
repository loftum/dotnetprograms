namespace StuffLibrary.Common.ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string ValueOrDefault(this string value, string defaultValue = "")
        {
            return value ?? defaultValue;
        }
    }
}