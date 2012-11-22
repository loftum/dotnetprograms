using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace DbTool.IntegrationTests.TableProxies
{
    public class IdConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column("OrderId");
        }
    }
}