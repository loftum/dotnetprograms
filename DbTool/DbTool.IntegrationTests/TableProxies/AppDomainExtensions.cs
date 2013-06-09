using System;

namespace DbTool.IntegrationTests.TableProxies
{
    public static class AppDomainExtensions
    {
        public static T Create<T>(this AppDomain appDomain, params object[] activationAttributes)
        {
            return (T) appDomain.CreateInstanceFromAndUnwrap(typeof (T).Assembly.FullName, typeof (T).FullName, activationAttributes);
        }
    }
}