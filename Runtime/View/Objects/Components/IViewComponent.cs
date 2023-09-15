namespace Dythervin.Game.Framework.View
{
    public interface IViewComponent : IComponent
    {
        new IModelView Owner { get; }
    }
}