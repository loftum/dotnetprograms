using Generate.Lib.Personal;

namespace Generate.Commands
{
	public class DnrGeneratorCommand : IGeneratorCommand
	{
		public string Name {get { return "dnr"; } }

		public DnrGeneratorCommand ()
		{
		}

		public string Generate()
		{
			return new SsnGenerator().GenerateDnr ();
		}
	}
}

