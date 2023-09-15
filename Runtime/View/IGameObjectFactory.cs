using Dythervin.Game.Framework.Data;
using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    public interface IGameObjectFactory
    {
        bool TryConstruct<T>(FeatureId featureId, out T view, Transform parent = null)
            where T : class;

        bool TryConstruct(FeatureId featureId, out GameObject view, Transform parent = null);

        bool TryConstruct(FeatureId featureId, out GameObject view, out Vector3 prefabScale, Transform parent = null);
    }
}