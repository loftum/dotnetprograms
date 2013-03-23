using System;
using NUnit.Framework;
using Generate.Lib;

namespace Generate.UnitTesting
{
	[TestFixture]
	public class PnrTest
	{
		[Test]
		public void Pnr_IsValid_ForValidPnr()
		{
			var pnr = new Pnr("06018209495");
			Assert.That (() => pnr.Validate(), Throws.Nothing);
		}

		[Test]
		public void Pnr_ParsesDate()
		{
			var pnr = new Pnr("06018209495");
			Assert.That (pnr.Date, Is.EqualTo(new DateTime(82, 1, 6)));
		}

		[Test]
		public void Dnr_IsValid_ForValidDnr()
		{
			var dnr = new Dnr("46018209495");
			Assert.That (() => dnr.Validate (), Throws.Nothing);
		}

		[Test]
		public void Dnr_ParsesDate()
		{
			var dnr = new Dnr("46018209495");
			Assert.That (dnr.Date, Is.EqualTo(new DateTime(82, 1, 6)));
		}
	}
}

