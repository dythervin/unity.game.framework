namespace Dythervin.Game.Framework
{
    public interface IGameComponent : IModelComponent, IModel
    {
        new IGame Owner { get; }
    }
}