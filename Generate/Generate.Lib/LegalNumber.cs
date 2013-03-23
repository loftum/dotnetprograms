using System;

namespace Generate.Lib
{
	public abstract class LegalNumber
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

		public LegalNumber (string value)
		{
			Value = value;
		}

		public abstract void Validate();

		public static implicit operator string(LegalNumber legalNumber)
		{
			return legalNumber == null ? null : legalNumber.Value;
		}
	}
}

