using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Dythervin.Game.Framework.View
{
    public class AssetsDatabaseService : IAssetDatabaseService
    {
        private readonly Dictionary<AssetKey, object> _objects = new();

        private static readonly string[] Buffer = new string[1];

        public bool TryGetAsset<T>(AssetKey assetKey, out T asset)
            where T : class
        {
            if (_objects.TryGetValue(assetKey, out object obj))
            {
                asset = obj as T;
                return asset != null;
            }

            //TODO: resource release
            Buffer[0] = assetKey.key;
            var locations = Addressables.LoadResourceLocationsAsync((IEnumerable)Buffer,
                Addressables.MergeMode.Intersection,
                typeof(T)).WaitForCompletion();

            if (locations.Count == 0)
            {
                asset = null;
                _objects[assetKey] = null;
                return false;
            }

            asset = Addressables.LoadAssetAsync<T>(locations[0]).WaitForCompletion();
            _objects.Add(assetKey, asset);
            //TODO: conflict resolve
            return asset != null;
        }
    }
}