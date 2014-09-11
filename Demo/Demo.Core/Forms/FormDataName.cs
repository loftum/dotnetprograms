using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Demo.Core.Forms
{
    public class FormDataName
    {
        public FormDataName Parent { get; private set; }
        public FormDataName Child { get; private set; }
        public string Name { get; private set; }
        public string FullPath { get { return Parent == null ? Name : string.Format("{0}.{1}", Parent.FullPath, Name); } }
        public bool IsLeaf { get { return Child == null; } }
        public bool IsIndexed { get; private set; }
        public string Index { get; private set; }

        private FormDataName(FormDataName parent, IList<string> parts)
        {
            Parent = parent;
            Name = parts[0];
            IsIndexed = Regex.IsMatch(Name, @"^[\S]+\[\S+\]$");
            Index = Regex.Match(Name, @"\[(\S+)\]").Groups[1].Value;
            parts.RemoveAt(0);
            if (parts.Any())
            {
                Child = new FormDataName(this, parts);
            }
        }

        public static FormDataName Parse(string path)
        {
            var parts = path.Split('.').ToList();
            return new FormDataName(null, parts);
        }

        public override string ToString()
        {
            return FullPath;
        }
    }
}