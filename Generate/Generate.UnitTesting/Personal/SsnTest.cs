using System;
using NUnit.Framework;
using Generate.Lib;

namespace Generate.UnitTesting
{
	[TestFixture]
	public class SsnTest
	{
		[Test]
		public void Pnr_IsValid_ForValidPnr()
		{
			var pnr = new Pnr("06018209495");
			Assert.That (() => pnr.Validate(), Throws.Nothing);
		}

		[Test]
		public void Pnr_IsInvalid_ForInvalidLength()
		{
			var pnr = new Pnr("0601820949");
			Assert.That(() => pnr.Validate (), Throws.Exception);
		}

		[Test]
		public void Pnr_IsInvalid_ForNonNumericString()
		{
			var pnr = new Pnr ("06018Æ09495");
			Assert.That(() => pnr.Validate (), Throws.Exception);
		}

		[Test]
		public void Pnr_ParsesDate()
		{
			var pnr = new Pnr("06018209495");
			Assert.That (pnr.Date, Is.EqualTo(new DateTime(1982, 1, 6)));
		}

		[Test]
		public void Dnr_IsValid_ForValidDnr()
		{
			var dnr = new Dnr("64106255294");
			Assert.That (() => dnr.Validate (), Throws.Nothing);
		}

		[Test]
		public void Dnr_ParsesDate()
		{
			var dnr = new Dnr("64106255294");
			Assert.That (dnr.Date, Is.EqualTo(new DateTime(1862, 10, 24)));
		}

		[Test]
		public void Hnr_IsValid_ForValidHnr()
		{
			var hnr = new Hnr ("14475168284");
			Assert.That(() => hnr.Validate (), Throws.Nothing);
		}

		[Test]
		public void Hnr_ParsesDate()
		{
			var hnr = new Hnr ("14475168284");
			Assert.That (hnr.Date, Is.EqualTo (new DateTime(1851, 7, 14)));
		}
	}
}

