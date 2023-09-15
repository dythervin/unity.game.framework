using System;
using System.Collections;
using System.Collections.Generic;
using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework.View
{
    public class RuleRepository : IRuleRepository
    {
        public event Action<IFeatureFactory> OnAdded;

        private readonly Dictionary<FeatureId, IFeatureFactory> _map = new();

        public IFeatureFactory this[FeatureId key] => _map[key];

        public int Count => _map.Count;

        public RuleRepository(Action<IFeatureFactory> onAdded = null)
        {
            OnAdded = onAdded;
        }

        public void Register(IFeatureFactory featureFactory, bool @override = false)
        {
            if (@override)
                _map[featureFactory.FeatureId] = featureFactory;
            else
                _map.Add(featureFactory.FeatureId, featureFactory);

            OnAdded?.Invoke(featureFactory);
        }

        public TObserverFactoryController Register<TObserverFactoryController>(
            TObserverFactoryController featureFactory, bool @override = false)
            where TObserverFactoryController : class, IFeatureFactory
        {
            Register((IFeatureFactory)featureFactory, @override);
            return featureFactory;
        }

        public void Clear()
        {
            _map.Clear();
        }

        IEnumerator<IFeatureFactory> IEnumerable<IFeatureFactory>.GetEnumerator()
        {
            return _map.Values.GetEnumerator();
        }

        public Dictionary<FeatureId, IFeatureFactory>.Enumerator GetEnumerator()
        {
            return _map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _map.Values.GetEnumerator();
        }

        public bool ContainsKey(FeatureId key)
        {
            return _map.ContainsKey(key);
        }

        public bool TryGetValue(FeatureId key, out IFeatureFactory value)
        {
            return _map.TryGetValue(key, out value);
        }
    }
}