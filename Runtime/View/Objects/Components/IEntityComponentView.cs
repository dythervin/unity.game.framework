namespace Dythervin.Game.Framework.View
{
    public interface IEntityComponentView : IModelComponentView, IModelView
    {
        new IEntityComponent Model { get; }
        new IEntityView Owner { get; }
    }

    public interface IEntityComponentView<out TModel> : IEntityComponentView, IModelView<TModel>
        where TModel : class, IEntityComponent
    {
        new TModel Model { get; }
    }
}