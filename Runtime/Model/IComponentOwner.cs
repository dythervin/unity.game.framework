namespace Dythervin.Game.Framework
{
    public interface IComponentOwner : IObject
    {
        IReadOnlyObjectListOut<IComponent> Components { get; }
    }


    public interface IComponentOwner<out TComponent> : IComponentOwner
        where TComponent : class, IComponent
    {
        new IReadOnlyObjectListOut<TComponent> Components { get; }
    }
}