namespace Dythervin.Game.Framework.View
{
    public interface IModelComponentView : IModelView
    {
         IModelView Owner { get; }
    }
}