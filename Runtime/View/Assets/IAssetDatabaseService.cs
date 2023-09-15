using Dythervin.Common;

namespace Dythervin.Game.Framework.View
{
    public interface IAssetDatabaseService : IService
    {
        bool TryGetAsset<T>(AssetKey assetKey, out T asset)
            where T : class;
    }
}