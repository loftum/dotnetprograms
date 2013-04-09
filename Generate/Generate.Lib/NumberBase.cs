using System;

namespace Generate.Lib
{
	public abstract class NumberBase
	{
		public string Value { get; private set; }

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

		public NumberBase(string value)
		{
			Value = value;
		}

		public abstract void Validate();

		public static implicit operator string(NumberBase legalNumber)
		{
			return legalNumber == null ? null : legalNumber.Value;
		}
	}
}

