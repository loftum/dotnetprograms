using System;

namespace DotNetPrograms.Common.Validation
{
    public class To
    {
        public static ValueValidator<T> BeAtLeast<T>(T minValue) where T : struct, IComparable<T>
        {
            return new ValueValidator<T>().AtLeast(minValue);
        }

        public static ValueValidator<T> BeAtMost<T>(T maxValue) where T : struct, IComparable<T>
        {
            return new ValueValidator<T>().AtMost(maxValue);
        }

        public static ValueValidator<T> BeMoreThan<T>(T minValue) where T : struct, IComparable<T>
        {
            return new ValueValidator<T>().MoreThan(minValue);
        }

        public static StringValidator ContainText(string value)
        {
            return new StringValidator().Contains(value);
        }
    }
}