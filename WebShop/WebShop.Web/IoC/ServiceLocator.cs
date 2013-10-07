using System;
using StructureMap;
using WebShop.Core.Users;

namespace WebShop.Web.IoC
{
    public class ServiceLocator
    {
        public static UserModel User { get { return Get<UserModel>(); } }

        public static BasketModel Basket
        {
            get { return User.Basket; }
        }

        public static T Get<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }

        public static object Get(Type type)
        {
            return ObjectFactory.GetInstance(type);
        }
    }
}