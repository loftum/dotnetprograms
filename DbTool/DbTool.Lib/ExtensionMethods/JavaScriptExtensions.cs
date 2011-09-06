using Newtonsoft.Json;

namespace DbTool.Lib.ExtensionMethods
{
    public static class JavaScriptExtensions
    {
        public static string AsJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}