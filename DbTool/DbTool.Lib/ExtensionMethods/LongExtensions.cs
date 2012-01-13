namespace DbTool.Lib.ExtensionMethods
{
    public static class LongExtensions
    {
        private static readonly string[] Units = new string[]{"B", "KiB", "MiB", "GiB", "TiB"};

        public static string ToMemoryUsage(this long value)
        {
            var number = value;
            var ii = 0;
            while(number / 1024 > 1)
            {
                number /= 1024;
                ii++;
            }
            return string.Format("{0} {1}", number, Units[ii]);
        }
    }
}