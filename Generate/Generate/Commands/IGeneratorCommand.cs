namespace Generate.Commands
{
	public interface IGeneratorCommand
	{
		string Name { get; }
		string Generate();
	}
}

