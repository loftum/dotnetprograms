using System;
using DotNetPrograms.Common.ExtensionMethods;

namespace Generate.Lib
{
	public abstract class AbstractValues
	{
		public int Length { get { return Values == null ? 0 : Values.Length; } }

		public string Values{ get; private set;}

		public AbstractValues(string values)
		{
			Values = values;
		}

		public int this[int index]
		{
			get { return GetOrDefault<int>(Values, index);}
		}

		protected T GetOrDefault<T>(string values, int index, int length = 1)
		{
			try
			{
				return values.Substring(index, length).ConvertTo<T>();
			}
			catch
			{
				return default(T);
			}
		}
	}
}

