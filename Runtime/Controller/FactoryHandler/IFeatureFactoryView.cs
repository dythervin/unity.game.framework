using Dythervin.Game.Framework.View;

namespace Dythervin.Game.Framework.Controller
{
    public interface IFeatureFactoryView<TModel> : IFeatureFactory<TModel>,
        IFeatureFactoryView
        where TModel : class, IModelInitializable, IModel
    {
    }

    public interface IFeatureFactoryView : IFeatureFactory
    {
        IFeatureFactoryView SetViewFactory(IViewFactory viewFactory);
    }
}