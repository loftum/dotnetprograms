using System;
using MonoMac.Foundation;
using DbTool.Lib.ExtensionMethods;

namespace DbToolMac.ExtensionMethods
{
    public static class StringExtensions
    {
        public static NSString ToNSString(this string val)
        {
            return new NSString(val);
        }
    }
}

