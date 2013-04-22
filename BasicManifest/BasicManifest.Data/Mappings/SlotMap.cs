using BasicManifest.Core.Domain;

namespace BasicManifest.Data.Mappings
{
    public class SlotMap : DomainObjectMap<Slot>
    {
        public SlotMap()
        {
            Map(s => s.Price);
            Map(s => s.JumperPays);
            References(s => s.Jumper).Cascade.All();
        }
    }
}