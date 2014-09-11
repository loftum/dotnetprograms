using System;

namespace DataAccess.Sql.Linq.Conditions
{
    public class TypeSwitch
    {
        private readonly Type _type;
        
        private TypeSwitch(Type type)
        {
            _type = type;
        }

        public static TypeSwitch Switch(Type type)
        {
            return new TypeSwitch(type);
        }

        public TypeSwitch Case<TType>(Action action)
        {
            if (_type == typeof (TType))
            {
                action();
            }
            return this;
        }
    }
}