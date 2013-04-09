using System;

namespace Generate.Lib.Personal
{
	public class Pnr : Ssn
	{
		public Pnr (string value) : base(value)
		{
		}

		protected override DateTime ParseDate ()
		{
			var year = Numbers.Individual < 500 ? 1900 + Numbers.Year : 1800 + Numbers.Year;
			return new DateTime(year, Numbers.Month, Numbers.Day);
		}
	}
}

