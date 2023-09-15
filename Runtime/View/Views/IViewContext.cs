namespace Dythervin.Game.Framework.View
{
    public interface IViewContext
    {
        IGameObjectFactory GameObjectFactory { get; }

        IViewMap ViewMap { get; }
    }
}