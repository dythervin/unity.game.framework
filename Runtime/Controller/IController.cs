using Dythervin.Game.Framework.View;

namespace Dythervin.Game.Framework.Controller
{
    public interface IController
    {
        IModel Model { get; }
    }

    public interface IControllerViewDependant : IController
    {
        IViewInputProvider ViewInputProvider { get; }
    }

    public interface IController<out TModel> : IController
    {
        new TModel Model { get; }
    }
}