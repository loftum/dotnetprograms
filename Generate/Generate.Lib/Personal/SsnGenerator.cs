using System;
using DotNetPrograms.Common.DateAndTime;
using System.Text;

namespace Generate.Lib
{
	public class SsnGenerator
	{
		private static readonly Random Random = new Random();

		public SsnGenerator()
		{
		}

		public Pnr GeneratePnr()
		{
			var ssn = DoGenerate ();
			while(!ssn.IsValid)
			{
				ssn = DoGenerate();
			}
			return ssn;
		}

		private Pnr DoGenerate()
		{
			var date = DateTimeProvider.Now.AddYears(-Random.Next(1,70)).AddDays(Random.Next(365));
			var ssn = new StringBuilder (date.ToString ("ddMMyy"))
				.Append (RandomPnrIndividual(date));
			ssn.Append (Ssn.CalculateK1 (ssn.ToString()));
			ssn.Append (Ssn.CalculateK2(ssn.ToString()));
			return new Pnr(ssn.ToString());
		}

		private static string RandomPnrIndividual(DateTime date)
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

