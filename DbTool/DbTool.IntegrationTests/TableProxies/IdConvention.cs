using System.Linq;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace DbTool.IntegrationTests.TableProxies
{
    public class IdConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            var column = instance.EntityType.GetProperties().First().Name;
            instance.Column(column);
        }
    }
}