namespace Dythervin.Game.Framework.View
{
    public class ComponentViewFactory<TObserver, TView> : ViewFactory<TObserver, TView>
        where TObserver : class, IModelComponent
        where TView : class, IModelView, IModelComponentView
    {
        public ComponentViewFactory(IGameObjectFactory gameObjectFactory, IViewMap viewMap, IViewContext viewContext) :
            base(gameObjectFactory, viewMap, viewContext)
        {
        }
    }
}