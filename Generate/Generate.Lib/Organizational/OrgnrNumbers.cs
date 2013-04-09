using DotNetPrograms.Common.ExtensionMethods;

namespace Generate.Lib.Organizational
{
	public class OrgnrNumbers
	{
		public int S1 { get; private set; }
		public int S2 { get; private set; }
		public int S3 { get; private set; }
		public int S4 { get; private set; }
		public int S5 { get; private set; }
		public int S6 { get; private set; }
		public int S7 { get; private set; }
		public int S8 { get; private set; }
		public int K { get; private set; }

		public OrgnrNumbers (string value)
		{
			S1 = GetOrDefault<int>(value, 0);
			S2 = GetOrDefault<int>(value, 1);
			S3 = GetOrDefault<int>(value, 2);
			S4 = GetOrDefault<int>(value, 3);
			S5 = GetOrDefault<int>(value, 4);
			S6 = GetOrDefault<int>(value, 5);
			S7 = GetOrDefault<int>(value, 6);
			S8 = GetOrDefault<int>(value, 7);
			K = GetOrDefault<int>(value, 8);
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

