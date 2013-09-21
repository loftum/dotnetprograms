using System;
using System.Collections.Generic;
using System.Linq;

namespace WebShop.Core.Domain.Inheritance
{
    public class Inheritable<TValue>
    {
        private readonly Func<Inheritable<TValue>> _getInherited;
        private readonly TValue _defaultValue;
        private readonly Func<string> _source;
        public string Source { get { return _source == null ? "Unknown" : _source(); } }
        public TValue Value { get; set; }
        public TValue Calculated { get { return WillInheritValue ? CalculateInheritedValue() : Value; } }

        public bool HasAncestor { get { return Ancestor != null; } }
        public Inheritable<TValue> Ancestor { get { return _getInherited == null ? null : _getInherited(); } }
        public IEnumerable<Inheritable<TValue>> Heritage
        {
            get
            {
                var ancestorsHeritage = HasAncestor ? Ancestor.Heritage : Enumerable.Empty<Inheritable<TValue>>();
                return new[] { this }.Concat(ancestorsHeritage);
            }
        } 

        public bool WillInheritValue { get { return Equals(Value, _defaultValue) && HasAncestor; } }

        private Inheritable()
        {
        }

        public Inheritable(string source, Func<Inheritable<TValue>> getInherited, TValue defaultValue = default(TValue))
        {
            _source = () => source;
            _getInherited = getInherited;
            _defaultValue = defaultValue;
        }

        public Inheritable(Func<string> source, Func<Inheritable<TValue>> getInherited, TValue defaultValue = default(TValue))
        {
            _source = source;
            _getInherited = getInherited;
            _defaultValue = defaultValue;
        }

        public void Reset()
        {
            Value = _defaultValue;
        }

        private TValue CalculateInheritedValue()
        {
            var inherited = Ancestor;
            return inherited == null ? Value : inherited.Calculated;
        }
    }
}