using System;
using System.Text;

namespace Deploy.Lib.Values
{
    public class DateValue : Value<DateTime>
    {
        public DateValue(DateTime value) : base(value)
        {
        }

        public string DdMmYyyy()
        {
            return new StringBuilder()
                .Append(LeftPad(TheValue.Day, '0', 2))
                .Append(LeftPad(TheValue.Month, '0', 2))
                .Append(LeftPad(TheValue.Year, '0', 4))
                .ToString();
        }

        public string YyyyMmDd()
        {
            return new StringBuilder()
                .Append(LeftPad(TheValue.Year, '0', 4))
                .Append(LeftPad(TheValue.Month, '0', 2))
                .Append(LeftPad(TheValue.Day, '0', 2))
                .ToString();
        }

        public string HhMmSs()
        {
            return new StringBuilder()
                .Append(LeftPad(TheValue.Hour, '0', 2))
                .Append(LeftPad(TheValue.Minute, '0', 2))
                .Append(LeftPad(TheValue.Second, '0', 2))
                .ToString();
        }

        private static string LeftPad(object value, char padChar, int totalNumberOfCharacters)
        {
            var builder = new StringBuilder(value.ToString());
            while (builder.Length < totalNumberOfCharacters)
            {
                builder.Insert(0, padChar);
            }
            return builder.ToString();
        }
    }
}
