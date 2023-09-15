using Dythervin.Common;
using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    [DisallowMultipleComponent]
    public abstract class GameView<TGame, TContext, TComponent> : ModelView<TGame, TContext, TComponent>, IGameView<TGame>
        where TGame : class, IGame
        where TContext : class, IViewContext
        where TComponent : class, IViewComponent
    {
        IGame IGameView.Model => Model;

        TGame IProvider<TGame>.Data => Model;
    }


    public interface IGameComponentView<T> : IGameComponentView
    {
    }
}