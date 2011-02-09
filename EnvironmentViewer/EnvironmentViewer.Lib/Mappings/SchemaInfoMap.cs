using EnvironmentViewer.Lib.Domain;
using FluentNHibernate.Mapping;

namespace EnvironmentViewer.Lib.Mappings
{
    public class SchemaInfoMap : ClassMap<SchemaInfo>
    {
        public SchemaInfoMap()
        {
            Id(x => x.Version);
        }
    }
}