using Dythervin.Common;
using Dythervin.Game.Framework.View;

namespace Dythervin.Game.Framework.Controller
{
    public abstract class Controller<TModel> : IControllerInitializable, IController<TModel>
        where TModel : class, IModel
    {
        public TModel Model { get; }

        IModel IController.Model => Model;

        private InitState _initState;

        InitState IInitializable.InitState => _initState;

        protected Controller(TModel model)
        {
            Model = model;
        }

        void IInitializable.Init()
        {
            _initState.AssertNotInitialized();
            _initState = InitState.Initializing;
            Init();
            _initState = InitState.Initialized;
        }

        protected abstract void Init();
    }

    public abstract class Controller<TViewInputProvider, TModel> : Controller<TModel>, IControllerViewDependant
        where TViewInputProvider : class, IViewInputProvider
        where TModel : class, IModel
    {
        public TViewInputProvider ViewInputProvider { get; }

        IViewInputProvider IControllerViewDependant.ViewInputProvider => ViewInputProvider;

        protected Controller(TModel model, TViewInputProvider inputProvider) : base(model)
        {
            ViewInputProvider = inputProvider;
        }
    }
}