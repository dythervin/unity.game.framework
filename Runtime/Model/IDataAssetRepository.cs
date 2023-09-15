using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework
{
    public interface IDataAssetRepository
    {
        IDataAsset Get(string key);

        bool TryLoadIAsset<TDataAsset>(out TDataAsset dataAsset)
            where TDataAsset : class, IDataAsset;

        bool TryLoadIAsset<TDataAsset>(string key, out TDataAsset dataAsset)
            where TDataAsset : class, IDataAsset;

        bool TryLoadAsset<TDataAsset>(out TDataAsset dataAsset)
            where TDataAsset : class, IDataAsset, new();

        bool TryLoadAsset<TDataAsset>(string key, out TDataAsset dataAsset)
            where TDataAsset : class, IDataAsset, new();
    }
}