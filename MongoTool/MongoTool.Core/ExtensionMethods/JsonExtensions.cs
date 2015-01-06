using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MongoTool.Core.ExtensionMethods
{
    public static class JsonExtensions
    {
        public static string ToJson(this object item, bool indented = false, bool suppressErrors = false)
        {
            var settings = new JsonSerializerSettings();
            if (suppressErrors)
            {
                settings.Error = SuppressError;
            }
            var formatting = indented ? Formatting.Indented : Formatting.None;
            return JsonConvert.SerializeObject(item, formatting, settings);
        }

        private static void SuppressError(object sender, ErrorEventArgs e)
        {
            e.ErrorContext.Handled = true;
        }
    }
}