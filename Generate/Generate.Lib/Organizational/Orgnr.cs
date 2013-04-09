using System;

namespace Generate.Lib
{
	public class Orgnr : NumberBase
	{
		public OrgnrNumbers Numbers { get; private set; }

		public Orgnr (string value) : base(value)
		{
			Numbers = new OrgnrNumbers(value);
		}

		public override void Validate()
		{
			var sum = 3 * Numbers.S1 + 2 * Numbers.S2 + 7 * Numbers.S3 + 6 * Numbers.S4 + 5 * Numbers.S5 + 4 * Numbers.S6 + 3 * Numbers.S7 + 2 * Numbers.S8;
			var rest = sum % 11;
			var k = rest == 0 ? 0 : 11 - rest;
			if (k == 10 || k != Numbers.K)
			{
				throw new Exception("Invalid K");
			}
		}
	}
}

