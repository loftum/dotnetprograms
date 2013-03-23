using System;
using DotNetPrograms.Common.ExtensionMethods;

namespace Generate.Lib
{
	public abstract class Ssn : LegalNumber
	{
		public SsnNumbers Numbers { get; private set; }
		public DateTime Date { get; private set; }
		public Gender Gender
		{
			get { return Numbers.I3 % 2 == 0 ? Gender.Female : Gender.Male; }
		}

		protected Ssn(string value) : base(value)
		{
			Numbers = new SsnNumbers(value);
			Date = ParseDateOrDefault();
		}

		private DateTime ParseDateOrDefault()
		{
			try
			{
				return ParseDate();
			}
			catch
			{
				return DateTime.MinValue;
			}
		}

		protected abstract DateTime ParseDate();

		public override void Validate()
		{
			long trash;
			if (Value == null || Value.Length != 11)
			{
				throw new Exception("Ssn must be 11 characters");
			}
			if (!long.TryParse (Value, out trash))
			{
				throw new Exception("Ssn must be a number");
			}
			var k1 = CalculateK1(Value);
			if (k1 == 0 || k1 != Numbers.K1)
			{
				throw new Exception("Invalid K1");
			}
			var k2 = CalculateK2(Value);
			if (k2 == 0 || k2 != Numbers.K2)
			{
				throw new Exception("Invalid K2");
			}
			ParseDate();
		}

		public static int CalculateK1(string ssnString)
		{
			var ssn = new SsnNumbers(ssnString);
			var k1 = 11 - ((3 * ssn.D1 + 7 * ssn.D2 + 6 * ssn.M1 + 1 * ssn.M2 + 8 * ssn.Y1 + 9 * ssn.Y2 + 4 * ssn.I1 + 5 * ssn.I2 + 2 * ssn.I3) % 11);
			if (k1 == 10)
			{
				return 0;
			}
			return k1;
		}

		public static int CalculateK2(string ssnString)
		{
			var ssn = new SsnNumbers(ssnString);
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

