using HourGlass.Lib.Domain;

namespace HourGlass.Lib.Mappings
{
    public class HourCodeMap : DomainObjectMap<HourCode>
    {
        public HourCodeMap()
        {
            Map(x => x.Code);
            Map(x => x.Name);
            HasMany(x => x.Usages).Inverse().Cascade.All();
        }
    }
}