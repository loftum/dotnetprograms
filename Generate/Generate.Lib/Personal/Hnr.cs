using System;

namespace Generate.Lib.Personal
{
	public class Hnr : Ssn
	{
		public Hnr (string value) : base(value)
		{
		}

		protected override DateTime ParseDate ()
		{
			var year = Numbers.Individual < 500 ? 1900 + Numbers.Year : 1800 + Numbers.Year;
			return new DateTime(year, Numbers.Month - 40, Numbers.Day);
		}
	}
}

