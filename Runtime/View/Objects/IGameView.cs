namespace Dythervin.Game.Framework.View
{
    public interface IGameView : IModelView
    {
        new IGame Model { get; }
    }

    public interface IGameView<out TObserver> : IGameView, IModelView<TObserver> where TObserver : class, IGame
    {
        new TObserver Model { get; }
    }
}