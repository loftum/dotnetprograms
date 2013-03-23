using System;
using DotNetPrograms.Common.ExtensionMethods;

namespace Generate.Lib
{
	public class Ssn
	{
		public string Value { get; private set; }
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
		public Gender Gender
		{
			get { return I3 % 2 == 0 ? Gender.Female : Gender.Male; }
		}
		public bool IsValid
		{
			get
			{
				try
				{
					Validate();
					return true;
				}
				catch
				{
					return false;
				}
			}
		}

		public Ssn(string value)
		{
			Value = value;
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

		public void Validate()
		{
			var k1 = CalculateK1(Value);
			if (k1 == 0 || k1 != K1)
			{
				throw new Exception("Invalid K1");
			}
			var k2 = CalculateK2(Value);
			if (k2 == 0 || k2 != K2)
			{
				throw new Exception("Invalid K2");
			}
		}

		public static int CalculateK1(string ssnString)
		{
			var ssn = new Ssn(ssnString);
			var k1 = 11 - ((3 * ssn.D1 + 7 * ssn.D2 + 6 * ssn.M1 + 1 * ssn.M2 + 8 * ssn.Y1 + 9 * ssn.Y2 + 4 * ssn.I1 + 5 * ssn.I2 + 2 * ssn.I3) % 11);
			if (k1 == 10)
			{
				return 0;
			}
			return k1;
		}

		public static int CalculateK2(string ssnString)
		{
			var ssn = new Ssn(ssnString);
			var k2 = 11 - ((5 * ssn.D1 + 4 * ssn.D2 + 3 * ssn.M1 + 2 * ssn.M2 + 7 * ssn.Y1 + 6 * ssn.Y2 + 5 * ssn.I1 + 4 * ssn.I2 + 3 * ssn.I3 + 2 * ssn.K1) % 11);
			if (k2 == 10)
			{
				return 0;
			}
			return k2;
		}

		private static T GetOrDefault<T>(string value, int index)
		{
			try
			{
				return value.Substring(index, 1).ConvertTo<T>();
			}
			catch
			{
				return default(T);
			}
		}

		public override string ToString ()
		{
			return string.Format ("{0} ({1})", Value, IsValid ? "valid" : "invalid");
		}
	}
}

