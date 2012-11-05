using System;
using System.Collections.Generic;
using DotNetPrograms.Common.Exceptions;
using DotNetPrograms.Common.Meta;

namespace DotNetPrograms.Common.Validation
{
    public class ValueValidator<TValue> : IPropertyValidator<TValue> where TValue : struct, IComparable<TValue>
    {
        private TValue _minValue;
        private TValue _maxValue;
        private TValue _moreThanValue;
        private readonly ISet<TValue> _included = new HashSet<TValue>();
        private readonly ISet<TValue> _excluded = new HashSet<TValue>();

        public ValueValidator()
        {
            var meta = new ValueTypeMeta<TValue>();
            _minValue = meta.MinValue;
            _maxValue = meta.MaxValue;
            _moreThanValue = meta.MinValue;
        }

        public ValueValidator<TValue> AtLeast(TValue number)
        {
            _minValue = number;
            return this;
        }

        public ValueValidator<TValue> AtMost(TValue max)
        {
            _maxValue = max;
            return this;
        }

        public ValueValidator<TValue> MoreThan(TValue number)
        {
            _moreThanValue = number;
            return this;
        }

        public ValueValidator<TValue> Or(params TValue[] included)
        {
            foreach (var number in included)
            {
                _included.Add(number);
                _excluded.Remove(number);
            }
            return this;
        }

        public ValueValidator<TValue> Not(params TValue[] excluded)
        {
            foreach (var number in excluded)
            {
                _excluded.Add(number);
                _included.Remove(number);
            }
            return this;
        }

        public void Validate(TValue number)
        {
            if (_included.Contains(number))
            {
                return;
            }
            if (_excluded.Contains(number))
            {
                throw new UserException("Ugyldig verdi: {0}", number);
            }
            if (number.CompareTo(_maxValue) > 0)
            {
                throw new UserException("{0} kan ikke være større enn {1}", number, _maxValue);
            }

            if (number.CompareTo(_minValue) < 0)
            {
                throw new UserException("{0} kan ikke være mindre enn {1}", number, _minValue);
            }

            var moreThanResult = number.CompareTo(_moreThanValue);
            if (moreThanResult < 0 || moreThanResult == 0)
            {
                throw new UserException("{0} må være større enn {1}", number, _moreThanValue);
            }
        }
    }
}