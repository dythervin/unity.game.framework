using Dythervin.Common;
using Dythervin.Core;
using Dythervin.Game.Framework.Data;
using UnityEngine;

namespace Dythervin.Game.Framework
{
    public class GameController : IGameController, IModelContext
    {
        public IAnyFactory AnyFactory { get; }

        public IServiceContainer ServiceContainer { get; }

        public IDataAssetRepository DataAssetRepository { get; }

        public IGame Game { get; private set; }

        IGameController IModelContext.GameController => this;

        public GameController(IAnyFactory anyFactory, IDataAssetRepository dataAssetRepository, IServiceContainer serviceContainer)
        {
            AnyFactory = anyFactory;
            DataAssetRepository = dataAssetRepository;
            ServiceContainer = serviceContainer;
        }

#if UNITY_EDITOR
        private void OnApplicationQuitting()
        {
            Application.quitting -= OnApplicationQuitting;
            Game?.TryDispose();
        }
#endif

        public void Start(IGameData gameDataWrapperAsset)
        {
            Game = AnyFactory.Construct<IGame>(gameDataWrapperAsset);
#if UNITY_EDITOR
            Application.quitting += OnApplicationQuitting;
#endif
        }

        public void Exit()
        {
            Game?.TryDispose();
        }
    }
}