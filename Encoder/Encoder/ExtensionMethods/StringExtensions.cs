using System;
using System.Text;
using System.Web;

namespace Encoder.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string ToBase64(this string text)
        {
            if (text == null)
            {
                return null;
            }
            var byteArray = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(byteArray);
        }

        public static string UrlEncoded(this string text)
        {
            return HttpUtility.UrlEncode(text);
        }

        public static string UrlDecoded(this string text)
        {
            return HttpUtility.UrlDecode(text);
        }

        public static string FromBase64(this string base64)
        {
            if (base64 == null)
            {
                return null;
            }
            var bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}