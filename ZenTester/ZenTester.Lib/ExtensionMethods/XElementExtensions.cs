using System;
using System.Xml.Linq;

namespace ZenTester.Lib.ExtensionMethods
{
    public static class XElementExtensions
    {
        public static string GetStringValue(this XElement element, XName xname)
        {
            var child = element.Element(xname);
            return child == null ? string.Empty : child.Value;
        }

        public static long GetLongValue(this XElement element, XName xname)
        {
            var child = element.Element(xname);
            return child == null ? 0 : long.Parse(child.Value);
        }

        public static int GetIntValue(this XElement element, XName xname)
        {
            var child = element.Element(xname);
            return child == null ? 0 : int.Parse(child.Value);
        }

        public static DateTime GetDateValue(this XElement element, XName xname)
        {
            var child = element.Element(xname);
            return child == null ? new DateTime() : DateTime.Parse(child.Value);
        }

        public static XElement GetChild(this XElement element, XName xname)
        {
            return element.Element(xname);
        }
    }
}