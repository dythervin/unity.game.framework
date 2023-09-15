using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework
{
    public interface IGameController
    {
        void Start(IGameData gameDataWrapperAsset);

        void Exit();
    }
}