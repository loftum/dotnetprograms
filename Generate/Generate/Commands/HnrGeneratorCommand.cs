using System;
using Generate.Lib;

namespace Generate
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

