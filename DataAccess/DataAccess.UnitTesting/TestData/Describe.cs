using System.Linq;

namespace DataAccess.UnitTesting.TestData
{
    public static class Describe
    {
        public static string Object(object o)
        {
            if (o == null)
            {
                return "null";
            }
            var properties = string.Join(", ",
                o.GetType().GetProperties().Select(p => string.Format("{0}={1}", p.Name, p.GetValue(o))));
            return string.Format("{0} ({1})", o.GetType().Name, properties);
        }
    }
}