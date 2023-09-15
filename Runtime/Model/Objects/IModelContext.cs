using Dythervin.Common;

namespace Dythervin.Game.Framework
{
    public interface IModelContext
    {
        IAnyFactory AnyFactory { get; }

        IServiceContainer ServiceContainer { get; }

        IGameController GameController { get; }

        IGame Game { get; }

        IDataAssetRepository DataAssetRepository { get; }
    }
}