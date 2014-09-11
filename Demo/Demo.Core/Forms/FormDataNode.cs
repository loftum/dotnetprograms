using System.Collections.Generic;

namespace Demo.Core.Forms
{
    public class FormDataNode
    {
        public FormDataNode Parent { get; private set; }
        public string Name { get; private set; }
        public string FullName { get { return Parent == null ? Name : string.Format("{0}.{1}", Parent.FullName, Name); } }

        public string Value { get; set; }
        public IDictionary<string, FormDataNode> Values { get; private set; }

        public FormDataNode(FormDataNode parent, string name)
        {
            Parent = parent;
            Name = name;
            Values = new Dictionary<string, FormDataNode>();
        }

        public void Set(FormDataName name, string value)
        {
            var child = GetChild(name.Name);
            if (name.IsLeaf)
            {
                child.Value = value;
                return;
            }
            child.Set(name.Child, value);
        }

        private FormDataNode GetChild(string name)
        {
            if (!Values.ContainsKey(name))
            {
                Values[name] = new FormDataNode(this, name);
            }
            return Values[name];
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}