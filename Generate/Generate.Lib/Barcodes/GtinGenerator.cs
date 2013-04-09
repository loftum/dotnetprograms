using System;
using System.Linq;

namespace Generate.Lib.Barcodes
{
	public class GtinGenerator
	{
		private static readonly Random Random = new Random();

		public Gtin Generate()
		{
			var values = string.Join("", Enumerable.Range(1, 13).Select(n => Random.Next(0, 9)));
			var checksum = Gtin.CalculateChecksum(values);
			return new Gtin(string.Format("{0}{1}", values, checksum));
		}
	}
}

