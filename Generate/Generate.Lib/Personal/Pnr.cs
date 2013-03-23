using System;
using DotNetPrograms.Common.DateAndTime;

namespace Generate.Lib
{
	public class Pnr : Ssn
	{
		public Pnr (string value) : base(value)
		{
		}

		protected override DateTime ParseDate ()
		{
			return new DateTime(Numbers.Year, Numbers.Month, Numbers.Day);
		}
	}
}

