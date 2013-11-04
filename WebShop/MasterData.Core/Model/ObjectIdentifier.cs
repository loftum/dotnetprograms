namespace MasterData.Core.Model
{
    public class ObjectIdentifier : IObjectIdentifier
    {
        public string Name { get; private set; }

        public ObjectIdentifier(string name)
        {
            Name = name;
        }
    }
}