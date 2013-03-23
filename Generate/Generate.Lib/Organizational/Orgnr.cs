using System;

namespace Generate.Lib
{
	public class Orgnr : LegalNumber
	{
		public Orgnr (string value) : base(value)
		{
		}

		public override void Validate()
		{
			throw new NotImplementedException();
		}
	}
}

