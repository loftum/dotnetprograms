using System;
using StructureMap;
using WebShop.Core.Users;

namespace WebShop.Web.IoC
{
    public class ServiceLocator
    {
        public static User User { get { return Get<User>(); } }

        public static Basket Basket
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