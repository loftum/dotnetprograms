using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetPrograms.Common.Meta
{
    public class EnumMeta<TEnum>
    {
        public EnumMeta()
        {
            var type = typeof(TEnum);
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("The type {0} is not an Enum, which it (obviously) has to be.", type));
            }
        }

        public TEnum Convert(object stupidValue)
        {
            try
            {
                return DoConvert((dynamic)stupidValue);
            }
            catch (Exception)
            {
                throw new ArgumentException(string.Format("One cannot cleverly convert value '{0}' ({1}) to type {2}", stupidValue, stupidValue.GetType(), typeof(TEnum)));
            }
        }

        private static TEnum DoConvert(byte value)
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }

        private static TEnum DoConvert(int value)
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }

        private static TEnum DoConvert(string value)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }

        private static TEnum DoConvert(object value)
        {
            return DoConvert((int)value);
        }

        public IEnumerable<TEnum> GetValues()
        {
            return Enum.GetValues(typeof (TEnum)).Cast<TEnum>();
        }
    }
}