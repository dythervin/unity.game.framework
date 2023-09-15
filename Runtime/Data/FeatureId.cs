using System;
using Dythervin.Common;

namespace Dythervin.Game.Framework.Data
{
    public readonly struct FeatureId : IEquatable<FeatureId>
    {
        public readonly TypeID groupId;
        public readonly TypeID featureId;

        public string Name { get; }

        public FeatureId(TypeID groupId, TypeID featureId)
        {
            if (groupId == featureId)
                throw new Exception("Same id");
            this.groupId = groupId;
            this.featureId = featureId;
            Name = $"{groupId.Type.Name}.{featureId.Type.Name}";
        }

        public bool Equals(FeatureId other)
        {
            return groupId.Equals(other.groupId) && featureId.Equals(other.featureId);
        }

        public override bool Equals(object obj)
        {
            return obj is FeatureId other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (groupId.GetHashCode() * 397) ^ featureId.GetHashCode();
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}