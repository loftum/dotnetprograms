using System.Collections;
using System.Collections.Generic;

namespace Demo.Core.Forms
{
    public class FormDataCollection : IEnumerable<FormDataNode>
    {
        private readonly IDictionary<string, FormDataNode> _nodes = new Dictionary<string, FormDataNode>();

        public void Add(KeyValuePair<string, string> keyValue)
        {
            var key = keyValue.Key;
            var name = FormDataName.Parse(key);
            var node = GetOrCreate(name.Name);
            if (name.IsLeaf)
            {
                node.Value = keyValue.Value;
                return;
            }
            node.Set(name.Child, keyValue.Value);
        }

        private FormDataNode GetOrCreate(string name)
        {
            if (!_nodes.ContainsKey(name))
            {
                _nodes[name] = new FormDataNode(null, name);
            }
            return _nodes[name];
        }

        public FormDataCollection(IDictionary<string, string> form)
        {
            foreach (var keyValue in form)
            {
                Add(keyValue);
            }
        }

        public FormDataNode this[string key]
        {
            get
            {
                if (_nodes.ContainsKey(key))
                {
                    return _nodes[key];
                }
                return null;
            }
        }

        public IEnumerator<FormDataNode> GetEnumerator()
        {
            return _nodes.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}