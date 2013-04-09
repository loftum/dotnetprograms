using System;

namespace Generate.Lib.Personal
{
	public class NonDate
	{
		public int Year { get; set; }
		public int Month { get; set; }
		public int Day { get; set; }

		public NonDate(DateTime date) : this(date.Year, date.Month, date.Day)
		{
		}

		private NonDate (int year, int month, int day)
		{
			Year = GetTwoLastDigitsOf(year);
			Month = month;
			Day = day;
		}

		private int GetTwoLastDigitsOf (int year)
		{
			var ret = year;
			while (ret > 99)
			{
				ret -= 100;
			}
			return ret;
		}

		public override string ToString()
		{
			return string.Format("{0}{1}{2}", LeftPad(Day), LeftPad(Month), LeftPad(Year));
		}

		private static string LeftPad(int number)
		{
			return number.ToString ().PadLeft (2, '0');
		}
	}
}

