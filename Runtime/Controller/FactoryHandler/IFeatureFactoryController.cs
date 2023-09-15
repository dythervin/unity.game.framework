namespace Dythervin.Game.Framework.Controller
{
    public interface IFeatureFactoryController<TModel> : IFeatureFactoryView<TModel>, IFeatureFactoryController
        where TModel : class, IModelInitializable, IModel
    {
        public IFeatureFactoryController SetControllerFactory<TController>(
            ControllerFactory<TModel, TController> controllerFactory)
            where TController : class, IControllerInitializable, IController<TModel>, new();
    }
}