using System;
using System.Web.Mvc;
using StructureMap;

namespace BasicManifest.Web.Ioc
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, null);
            }
            return ObjectFactory.GetInstance(controllerType) as Controller;
        }
    }
}