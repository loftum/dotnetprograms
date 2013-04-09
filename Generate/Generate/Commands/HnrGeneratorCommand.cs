using Generate.Lib.Personal;

namespace Generate.Commands
{
	public class HnrGeneratorCommand : IGeneratorCommand
	{
		public string Name { get { return "hnr"; } }

		public HnrGeneratorCommand ()
		{
		}

		public string Generate()
		{
			return new SsnGenerator().GenerateHnr();
		}
	}
}

