using System.Collections.Generic;
using System.Text;

namespace StuffLibrary.HtmlTools.Builders
{
    public class HtmlElement
    {
        private readonly IDictionary<string, object> _attributes = new Dictionary<string, object>();
        public string Tag { get; private set; }
        public string Id
        {
            get { return GetAttributeOrDefault<string>("id", null); }
            set { SetAttribute("id", value); }
        }

        public string Type
        {
            get { return GetAttributeOrDefault<string>("type", null); }
            set { SetAttribute("type", value); }
        }

        public string Name
        {
            get { return GetAttributeOrDefault<string>("name", null); }
            set { SetAttribute("name", value); }
        }

        public string Value
        {
            get { return GetAttributeOrDefault<string>("value", null); }
            set { SetAttribute("value", value); }
        }

        public string Title
        {
            get { return GetAttributeOrDefault<string>("title", null); }
            set { SetAttribute("title", value); }
        }

        public string OnClick
        {
            get { return GetAttributeOrDefault<string>("onclick", null); }
            set { SetAttribute("onclick", value); }
        }

        public bool Hidden { get; set; }
        public bool Disabled { get; set; }

        public object Content { get; set; }
        public ISet<string> Classes = new HashSet<string>();

        public HtmlElement(string tag)
        {
            Tag = tag;
            Value = string.Empty;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("<{0} ", Tag);
            foreach (var attribute in _attributes)
            {
                builder.AppendFormat("{0}=\"{1}\" ", attribute.Key, attribute.Value);
            }
            if (Hidden)
            {
                builder.Append("style=\"display:none\" ");
            }
            if (Disabled)
            {
                builder.AppendFormat("disabled=\"disabled\" ");
            }

            builder.AppendFormat("class=\"{0}\" ", ClassesAsString());

            if (Content == null)
            {
                builder.Append("/>");
            }
            else
            {
                builder.Append(">")
                    .Append(Content.ToString())
                    .AppendFormat("</{0}>", Tag);
            }
            return builder.ToString();
        }

        private string ClassesAsString()
        {
            return string.Join(" ", Classes);
        }

        protected void RemoveAttribute(string key)
        {
            _attributes.Remove(key);
        }

        protected void SetAttribute(string key, object value)
        {
            if (value != null)
            {
                _attributes[key] = value;
            }
        }

        protected T GetAttributeOrDefault<T>(string key, T defaultValue)
        {
            var value = GetAttribute(key);
            if (value != null)
            {
                return (T)value;
            }
            return defaultValue;
        }

        protected object GetAttribute(string key)
        {
            try
            {
                return _attributes[key];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }
 }