using System;
using System.Text;
using NumberGenerator.Lib.DateAndTime;

namespace NumberGenerator.Lib.Personal
{
    public class SsnGenerator
    {
        private static readonly Random Random = new Random();

        public Ssn Generate()
        {
            var date = RandomDate();
            return GenerateFor(date);
        }

        private Ssn GenerateFor(DateTime date)
        {
            var number = new StringBuilder(date.ToString("ddMMyy"))
                .Append(Random.Next(0, 9))
                .Append(Random.Next(0, 9))
                .Append(Random.Next(0, 9));
            return new Ssn(number.ToString());
        }

        private static DateTime RandomDate()
        {
            return DateTimeProvider.Now
                .AddYears(- Random.Next(70))
                .AddDays(Random.Next(364));
        }
    }
}