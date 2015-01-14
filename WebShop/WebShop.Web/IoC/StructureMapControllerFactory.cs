using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace WebShop.Web.IoC
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }
            try
            {
                return ObjectFactory.GetInstance(controllerType) as Controller;
            }
            catch (StructureMapException ex)
            {
                throw new Exception(string.Format("This is what I have: {0}", ObjectFactory.WhatDoIHave()), ex);
            }
        }
    }
}