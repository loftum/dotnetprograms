using Generate.Lib.Barcodes;

namespace Generate.Commands
{
	public class GtinGeneratorCommand : IGeneratorCommand
	{
		public GtinGeneratorCommand()
		{
		}

		public string Generate()
		{
			return new GtinGenerator().Generate();
		}

		public string Name
		{
			get
			{
				return "gtin";
			}
		}
	}
}

