using System;
using Dythervin.Game.Framework.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Dythervin.Game.Framework.View
{
    public class GameObjectFactory : IGameObjectFactory
    {
        private readonly IAssetDatabaseService _assetDatabaseService;

        private readonly IAssetKeyResolverService _assetKeyResolver;

        public GameObjectFactory(IAssetDatabaseService assetDatabaseService, IAssetKeyResolverService assetKeyResolver)
        {
            _assetDatabaseService = assetDatabaseService;
            _assetKeyResolver = assetKeyResolver;
        }

        public virtual bool TryConstruct<T>(FeatureId featureId, out T view, Transform parent = null)
            where T : class
        {
            if (!TryConstruct(featureId, out GameObject gameObject, parent))
            {
                view = null;
                return false;
            }

            view = gameObject.GetComponent<T>();
            if (view == null)
                throw new Exception($"No component of type {typeof(T)} found in {gameObject.name}");

            return true;
        }

        public virtual bool TryConstruct(FeatureId featureId, out GameObject view, Transform parent = null)
        {
            return TryConstruct(featureId, out view, out _, parent);
        }

        public bool TryConstruct(FeatureId featureId, out GameObject view, out Vector3 prefabScale,
            Transform parent = null)
        {
            if (!_assetDatabaseService.TryGetAsset(_assetKeyResolver.Get(featureId), out GameObject asset))
            {
                view = null;
                prefabScale = default;
                return false;
            }

            view = Object.Instantiate(asset, parent);
            prefabScale = asset.transform.localScale;
            return true;
        }
    }
}