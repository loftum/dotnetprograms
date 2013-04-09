using System;
using Generate.Lib;

namespace Generate.Lib.Barcodes
{
	public class GtinNumbers : AbstractValues
	{
		private string _paddedValues;

		public int N1 { get; private set; }
		public int N2 { get; private set; }
		public int N3 { get; private set; }
		public int N4 { get; private set; }
		public int N5 { get; private set; }
		public int N6 { get; private set; }
		public int N7 { get; private set; }
		public int N8 { get; private set; }
		public int N9 { get; private set; }
		public int N10 { get; private set; }
		public int N11 { get; private set; }
		public int N12 { get; private set; }
		public int N13 { get; private set; }
		public int N14 { get; private set; }
		public int N15 { get; private set; }
		public int N16 { get; private set; }
		public int N17 { get; private set; }
		public int N18 { get; private set; }

		public GtinNumbers(string values) : base(values)
		{
			_paddedValues = Pad();
			N1 = GetOrDefault<int>(_paddedValues, 0);
			N2 = GetOrDefault<int>(_paddedValues, 1);
			N3 = GetOrDefault<int>(_paddedValues, 2);
			N4 = GetOrDefault<int>(_paddedValues, 3);
			N5 = GetOrDefault<int>(_paddedValues, 4);
			N6 = GetOrDefault<int>(_paddedValues, 5);
			N7 = GetOrDefault<int>(_paddedValues, 6);
			N8 = GetOrDefault<int>(_paddedValues, 7);
			N9 = GetOrDefault<int>(_paddedValues, 8);
			N10 = GetOrDefault<int>(_paddedValues, 9);
			N11 = GetOrDefault<int>(_paddedValues, 10);
			N12 = GetOrDefault<int>(_paddedValues, 11);
			N13 = GetOrDefault<int>(_paddedValues, 12);
			N14 = GetOrDefault<int>(_paddedValues, 13);
			N15 = GetOrDefault<int>(_paddedValues, 14);
			N16 = GetOrDefault<int>(_paddedValues, 15);
			N17 = GetOrDefault<int>(_paddedValues, 16);
			N18 = GetOrDefault<int>(_paddedValues, 17);
		}

		private string Pad()
		{
			var values = Values == null ? string.Empty : Values;
			return values.PadLeft(18, '0');
		}
	}
}

