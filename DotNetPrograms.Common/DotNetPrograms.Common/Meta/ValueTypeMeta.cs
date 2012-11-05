using System.Reflection;

namespace DotNetPrograms.Common.Meta
{
    public class ValueTypeMeta<T> where T : struct
    {
        public T MinValue { get; private set; }
        public T MaxValue { get; private set; }
        public T Default { get; private set; }

        public ValueTypeMeta()
        {
            Default = default(T);
            MinValue = GetStaticOrDefault("MinValue");
            MaxValue = GetStaticOrDefault("MaxValue");
        }

        private static T GetStaticOrDefault(string fieldName)
        {
            var field = typeof(T).GetField(fieldName, BindingFlags.Static | BindingFlags.Public);
            if (field != null)
            {
                return (T)field.GetValue(null);
            }
            return default(T);
        }
    }
}