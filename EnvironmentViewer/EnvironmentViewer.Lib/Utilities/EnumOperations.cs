using System;
using System.ComponentModel;

namespace EnvironmentViewer.Lib.Utilities
{
    public class EnumOperations
    {
        public const string Undefined = "Undefined";

        public static string GetDescriptionOf(Enum value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
            {
                return Undefined;
            }
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        public static int GetIntValueOf(Enum value)
        {
            return int.Parse(value.ToString("D"));
        }
    }
}