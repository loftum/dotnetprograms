using System;

namespace Generate.Lib
{
	public class Dnr : Ssn
	{
		public Dnr (string value) : base(value)
		{
		}

		protected override DateTime ParseDate ()
		{
			return new DateTime (Numbers.Year, Numbers.Month, Numbers.Day - 40);
		}
	}
}

