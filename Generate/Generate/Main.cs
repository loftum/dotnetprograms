using System;
using Generate.Commands;
using Generate.Ioc;

namespace Generate
{
	public class MainClass
	{
		public static void Main (string[] args)
		{
            ObjectContainer.Init(new GenerateRegistry());
		    var executor = ObjectContainer.Get<ICommandExecutor>();
		    var result = executor.Execute(args.Length == 0 ? "unknown" : args[0]);
			Console.WriteLine(result);
		}
	}
}
