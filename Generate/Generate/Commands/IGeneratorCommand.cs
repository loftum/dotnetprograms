using System;

namespace Generate
{
	public interface IGeneratorCommand
	{
		string Name { get; }
		string Generate();
	}
}

