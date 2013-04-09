using System;

namespace NumberGenerator.Lib.DateAndTime
{
    public static class DateTimeProvider
    {
        private static Func<DateTime> _now;
        static DateTimeProvider()
        {
            Reset();
        }

        private static void Reset()
        {
            _now = () => DateTime.Now;
        }

        public static DateTime Now
        {
            get
            {
                if (_now == null)
                {
                    _now = () => DateTime.Now;
                }
                return _now();
            }
            set { _now = () => value; }
        }
    }
}