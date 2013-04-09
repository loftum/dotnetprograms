using System;
using Generate.Commands;

namespace Generate
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var command = args.Length == 0 ? "unknown" : args [0];
			Console.WriteLine(Run(command));
		}

		private static string Usage()
		{
			return string.Format("Usage: pnr");
		}

		private static string Run(string command)
		{
			switch (command)
			{
				case "pnr":
                    return new PnrCommand().Generate();
				case "dnr":
                    return new DnrGeneratorCommand().Generate();
				case "hnr":
                    return new HnrGeneratorCommand().Generate();
				default:
					return "Unknown command " + command;
			}
		}
	}
}
