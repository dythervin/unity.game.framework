using System;
using System.Collections.Generic;
using System.Text;
using Dythervin.Core.Extensions;
using Dythervin.Game.Framework.Data;
using Dythervin.ObjectPool;

namespace Dythervin.Game.Framework.View
{
    public class AssetKeyResolverService : IAssetKeyResolverService
    {
        private readonly Dictionary<Type, string> _modelKeys = new Dictionary<Type, string>();

        private readonly Dictionary<FeatureId, AssetKey> _assetKeys = new Dictionary<FeatureId, AssetKey>();

        public void Register<TObserver>(string key)
            where TObserver : IModelData
        {
            Register(typeof(TObserver), key);
        }

        public void Register(Type type, string key)
        {
            if (!type.Implements(typeof(IModelData)))
                throw new Exception($"{type} must implement {typeof(IModelData)}");

            _modelKeys.Add(type, key);
        }

        public AssetKey Get(FeatureId id)
        {
            if (!_assetKeys.TryGetValue(id, out AssetKey key))
            {
                _assetKeys[id] = key = new AssetKey(ConstructKey(id), id.groupId.GetHashCode());
            }

            return key;
        }

        private string ConstructKey(FeatureId id)
        {
            using (SharedPool<StringBuilder>.Instance.Get(out StringBuilder str))
            {
                str.Clear();
                Type type = id.groupId.Type;
                if (!_modelKeys.TryGetValue(type, out string key))
                {
                    key = type.Name;
                    // Done explicitly via register
                    // if (type.IsInterface && key.Length > 2 && char.IsUpper(key[1]))
                    // {
                    //     str.Append(key, 1, key.Length - 1);
                    //     const string dataStr = "Data";
                    //     if (key.EndsWith(dataStr))
                    //         str.Remove(str.Length - dataStr.Length, dataStr.Length);
                    //
                    //     key = str.ToString();
                    //     str.Clear();
                    // }

                    _modelKeys.Add(type, key);
                }

                str.Append(key);
                str.Append('/');
                str.Append(id.featureId.Type.Name);

                return str.ToString();
            }
        }
    }
}