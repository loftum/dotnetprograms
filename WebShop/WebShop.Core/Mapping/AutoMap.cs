using System.Linq;
using AutoMapper;

namespace WebShop.Core.Mapping
{
    public static class AutoMap
    {
        public static void Initialize(Profile profile, params Profile[] additional)
        {
            Mapper.Initialize(a =>
                {
                    var profiles = new[] { profile }.Concat(additional);
                    foreach (var p in profiles)
                    {
                        a.AddProfile(p);
                    }
                });
        }

        public static void Reset()
        {
            Mapper.Reset();
        }
    }
}