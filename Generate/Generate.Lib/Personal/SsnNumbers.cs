using System;
using DotNetPrograms.Common.ExtensionMethods;

namespace Generate.Lib
{
	public class SsnNumbers
	{
		public int Day { get; private set; }
		public int Month { get; private set; }
		public int Year { get; private set; }

		public int D1 { get; private set; }
		public int M1 { get; private set; }
		public int M2 { get; private set; }
		public int Y1 { get; private set; }
		public int Y2 { get; private set; }
		public int I1 { get; private set; }
		public int I2 { get; private set; }
		public int I3 { get; private set; }
		public int K1 { get; private set; }
		public int K2 { get; private set; }
		public int D2 { get; private set; }

		public SsnNumbers(string value)
		{
			Day = GetOrDefault<int> (value, 0, 2);
			Month = GetOrDefault<int> (value, 2, 2);
			Year = GetOrDefault<int> (value, 4, 2);
			D1 = GetOrDefault<int>(value, 0);
			D2 = GetOrDefault<int>(value, 1);
			M1 = GetOrDefault<int>(value, 2);
			M2 = GetOrDefault<int>(value, 3);
			Y1 = GetOrDefault<int>(value, 4);
			Y2 = GetOrDefault<int>(value, 5);
			I1 = GetOrDefault<int>(value, 6);
			I2 = GetOrDefault<int>(value, 7);
			I3 = GetOrDefault<int>(value, 8);
			K1 = GetOrDefault<int>(value, 9);
			K2 = GetOrDefault<int>(value, 10);
		}

		private static T GetOrDefault<T>(string value, int index, int length = 1)
		{
			try
			{
				return value.Substring(index, length).ConvertTo<T>();
			}
			catch
			{
				return default(T);
			}
		}
	}
}

