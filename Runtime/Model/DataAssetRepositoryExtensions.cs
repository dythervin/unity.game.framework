using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework
{
    public static class DataAssetRepositoryExtensions
    {
        public static TDataAsset LoadAsset<TDataAsset>(this IDataAssetRepository assetRepository)
            where TDataAsset : class, IDataAsset, new()
        {
            assetRepository.TryLoadAsset(out TDataAsset dataAsset);
            return dataAsset;
        }

        public static TDataAsset LoadAsset<TDataAsset>(this IDataAssetRepository assetRepository, string key)
            where TDataAsset : class, IDataAsset, new()
        {
            assetRepository.TryLoadAsset(key, out TDataAsset dataAsset);
            return dataAsset;
        }
    }
}