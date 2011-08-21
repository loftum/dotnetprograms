using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace StuffLibrary.Repository.Configuration
{
    public class ForeignKeyConvention : IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.Column(string.Format("{0}Id", instance.Class.Name));
        }
    }
}