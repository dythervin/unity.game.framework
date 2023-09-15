using System;
using Dythervin.Common;
using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework.View
{
    public interface IAssetKeyResolverService : IService
    {
        public AssetKey Get(FeatureId id);

        void Register<TObserverData>(string key)
            where TObserverData : IModelData;

        void Register(Type type, string key);
    }
}