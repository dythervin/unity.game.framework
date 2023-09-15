namespace Dythervin.Game.Framework.View
{
    public interface IEntityView : IModelView, IModelComponentViewOwner
    {
        new IEntity Model { get; }
    }


    public interface IEntityView<out TObserver> : IEntityView, IModelView<TObserver>
        where TObserver : class, IEntity
    {
        new TObserver Model { get; }
    }
}