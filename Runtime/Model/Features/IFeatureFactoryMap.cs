using System.Collections.Generic;
using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework
{
    public interface IFeatureFactoryMap<TFeature> : IReadOnlyCollection<TFeature> where TFeature : IFeature
    {
        bool TryGetValue(FeatureId key, out TFeature value);

        TFeature this[FeatureId key] { get; }

        bool ContainsKey(FeatureId key);
    }
}