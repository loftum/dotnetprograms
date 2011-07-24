using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace StuffLibrary.Common.Operations
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

        public static IEnumerable<int> GetIntValuesOf<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<int>();
        }
    }
}