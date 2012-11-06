using System;
using System.Collections.Generic;
using System.Linq;
using DotNetPrograms.Common.ExtensionMethods;
using DotNetPrograms.Common.Validation;

namespace DotNetPrograms.Common.Web.Paths
{
    public static class VirtualPath
    {
        public static string Combine(params string[] paths)
        {
            return Combine(false, paths);
        }

        public static string CombineRooted(params string[] paths)
        {
            return Combine(true, paths);
        }

        private static string Combine(bool rooted, params string[] paths)
        {
            Guard.NotNull(() => paths);
            var strippedParts = paths
                .Where(p => !p.IsNullOrWhiteSpace())
                .Select(p => p.Replace("\\", "/").Trim(new[] { '/' }));
            return string.Format("{0}{1}", rooted ? "/" : string.Empty, string.Join("/", strippedParts));
        }

        public static IList<string> Split(string value)
        {
            return value.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}