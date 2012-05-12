using System.Linq;
using BuildMonitor.Common.ExtensionMethods;

namespace BuildMonitor.Common.Urls
{
    public class VirtualPath
    {
        public static string Combine(params string[] parts)
        {
            if (parts == null)
            {
                return null;
            }
            var strippedParts = parts.Select(p => p.RemoveStarting("/").RemoveTrailing("/").Replace(@"\", "/"));
            return string.Join("/", strippedParts);
        }
    }
}