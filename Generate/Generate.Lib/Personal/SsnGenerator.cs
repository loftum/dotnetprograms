using System;
using DotNetPrograms.Common.DateAndTime;
using System.Text;

namespace Generate.Lib.Personal
{
	public class SsnGenerator : ISsnGenerator
	{
		private static readonly Random Random = new Random();

		public SsnGenerator()
		{
		}

		public Pnr GeneratePnr()
		{
			return DoGenerate(Pnr);
		}

		public Dnr GenerateDnr()
		{
			return DoGenerate(Dnr);
		}

		public Hnr GenerateHnr()
		{
			return DoGenerate(Hnr);
		}

		private Hnr Hnr()
		{
			var date = new NonDate(RandomDate());
			date.Month += 40;
			var ssn = new StringBuilder(date.ToString())
				.Append (RandomPnrIndividual(date));
			ssn.Append (Ssn.CalculateK1 (ssn.ToString()));
			ssn.Append (Ssn.CalculateK2(ssn.ToString()));
			return new Hnr(ssn.ToString());
		}

		private Dnr Dnr()
		{
			var date = new NonDate(RandomDate());
			date.Day += 40;
			var ssn = new StringBuilder(date.ToString())
				.Append (RandomPnrIndividual(date));
			ssn.Append (Ssn.CalculateK1 (ssn.ToString()));
			ssn.Append (Ssn.CalculateK2(ssn.ToString()));
			return new Dnr(ssn.ToString());
		}

		private Pnr Pnr()
		{
			var date = new NonDate(RandomDate());
			var ssn = new StringBuilder (date.ToString())
				.Append (RandomPnrIndividual(date));
			ssn.Append (Ssn.CalculateK1 (ssn.ToString()));
			ssn.Append (Ssn.CalculateK2(ssn.ToString()));
			return new Pnr(ssn.ToString());
		}

		private static DateTime RandomDate()
		{
			return DateTimeProvider.Now.AddYears(-Random.Next(1,70)).AddDays(Random.Next(365));
		}

		private T DoGenerate<T>(Func<T> func) where T : Ssn
		{
			var ssn = func ();
			while (!ssn.IsValid)
			{
				ssn = func();
			}
			return ssn;
		}

		private static string RandomPnrIndividual(NonDate date)
		{
			if (date.Year >= 1900)
			{
				return Random.Next (0, 499).ToString().PadLeft(3, '0');
			}
			return Random.Next (500, 749).ToString().PadLeft(3, '0');
		}

		private static string RandomDnrIndividual()
		{
			return Random.Next (750, 999).ToString().PadLeft(3, '0');
		}
	}
}

