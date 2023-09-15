namespace Dythervin.Game.Framework
{
    public interface IGame : IModelComponentOwner
    {
        new IReadOnlyObjectListOut<IGameComponent> Components { get; }
        IReadOnlyObjectListOut<IEntity> Entities { get; }
    }
}