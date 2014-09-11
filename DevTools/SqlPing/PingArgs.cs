using System;

namespace SqlPing
{
    public class PingArgs
    {
        public string What { get; private set; }
        public bool Verbose { get; private set; }

        public PingArgs(string[] args)
        {
            What = args.GetOrDefault<string>(0);
            Verbose = args.GetOrDefault<string>(1) == "-v";
        }
    }

    static class ArgsExtensions
    {
        public static T GetOrDefault<T>(this string[] args, int index, T defaultValue = default(T))
        {
            var value = args.Length > index ? args[index] : null;
            return value == null ? defaultValue : (T) Convert.ChangeType(value, typeof (T));
        }
    }
}