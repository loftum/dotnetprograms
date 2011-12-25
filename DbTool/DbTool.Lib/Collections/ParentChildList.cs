using System.Collections.Generic;

namespace DbTool.Lib.Collections
{
    public class ParentChildList<TParent, TChild> : List<TChild>
        where TChild : IChild<TParent>
    {
        private readonly TParent _parent;
        public ParentChildList(TParent parent)
        {
            _parent = parent;
        }

        public new void Add(TChild child)
        {
            child.Parent = _parent;
            base.Add(child);
        }
    }
}