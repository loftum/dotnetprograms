using Generate.Commands;
using StructureMap.Configuration.DSL;

namespace Generate.Ioc
{
    public class GenerateRegistry : Registry
    {
        public GenerateRegistry()
        {
            Scan(s =>
            {
                s.TheCallingAssembly();
                s.Assembly("Generate.Lib");
                s.WithDefaultConventions();
                s.AddAllTypesOf<IGeneratorCommand>();
            });
        }
    }
}