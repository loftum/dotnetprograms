using System;

namespace DotNetPrograms.Common.Mapping.Internal
{
    public class TypePair
    {
        public Type SourceType { get; private set; }
        public Type TargetType { get; private set; }

        public TypePair(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TypePair);
        }

        public bool Equals(TypePair other)
        {
            return other != null &&
                   other.SourceType != null && other.SourceType == SourceType &&
                   other.TargetType != null && other.TargetType == TargetType;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((SourceType != null ? SourceType.GetHashCode() : 0) * 397) ^
                    (TargetType != null ? TargetType.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", SourceType.Name, TargetType.Name);
        }
    }
}