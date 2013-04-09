using System;
using DotNetPrograms.Common.ExtensionMethods;

namespace Generate.Lib.Barcodes
{
	public class Gtin : NumberBase
	{
		public GtinNumbers Numbers {get; private set; }

		public Gtin(string value) : base(value)
		{
			Numbers = new GtinNumbers(value);
		}

		public override void Validate()
		{
			if (!Numbers.Length.In(8, 12, 13, 14, 18))
			{
				throw new Exception("Length must be 8, 12, 13, 14 or 18");
			}
			var checksum = CalculateChecksum (Numbers.Values);
			if (Numbers.N18 != checksum)
			{
				throw new Exception("Invalid checksum");
			}
		}

		public static int CalculateChecksum(string value)
		{
			var numbers = new GtinNumbers(value);
			var sum = 3 * numbers.N1 + 
				numbers.N2 + 
					3 * numbers.N3 +
					numbers.N4 + 
					3 * numbers.N5 +
					numbers.N6 + 
					3 * numbers.N7 +
					numbers.N8 + 
					3 * numbers.N9 +
					numbers.N10 + 
					3 * numbers.N11 +
					numbers.N12 + 
					3 * numbers.N13 +
					numbers.N14 + 
					3 * numbers.N15 +
					numbers.N16 + 
					3 * numbers.N17;
			var checksum = sum.UpToNearestTen() - sum;
			return checksum;
		}
	}
}

