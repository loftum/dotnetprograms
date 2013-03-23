using System;

namespace Generate.Lib
{
	public class Hnr : Ssn
	{
		public Hnr (string value) : base(value)
		{
		}

		protected override DateTime ParseDate ()
		{
			return new DateTime(Numbers.Year, Numbers.Month - 40, Numbers.Day);
		}
	}
}

