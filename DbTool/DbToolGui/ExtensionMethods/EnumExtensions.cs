using System;
using System.ComponentModel;

namespace DbToolGui.ExtensionMethods
{
    public static class EnumExtensions
    {
        private const string Undefined = "Undefined";

        public static string GetDescription(this Enum value)
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
    }
}