using DotNetPrograms.Common.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StuffLibrary.Common.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static T FromJsonTo<T>(this string value, bool indented = false, bool suppressErrors = false)
        {
            Guard.NotNull(() => value);
            var settings = new JsonSerializerSettings();
            if (suppressErrors)
            {
                settings.Error = SuppressAllErrors;
            }
            settings.Formatting = indented ? Formatting.Indented : Formatting.None;
            
            var deserialized = JsonConvert.DeserializeObject<T>(value, settings);
            return deserialized;
        }

        private static void SuppressAllErrors(object sender, ErrorEventArgs e)
        {
        }
    } 
}