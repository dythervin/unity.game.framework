using System;
using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    public class ViewFactory<TModel, TView> : IViewFactory<TModel, TView>
        where TView : class, IModelView
        where TModel : class, IModel
    {
        public Type ModelType { get; }

        public event Action<IModel, IModelView> OnConstruct;

        protected readonly IGameObjectFactory gameObjectFactory;

        public IViewMap ViewMap { get; }

        protected readonly IViewContext viewContext;

        public static ViewFactory<TModel, TView> Construct<TObserver>(IGameObjectFactory gameObjectFactory,
            IViewMap viewMap, IViewContext viewContext)
            where TObserver : class, IModel
        {
            return new ViewFactory<TModel, TView>(gameObjectFactory, viewMap, viewContext);
        }

        public ViewFactory(IGameObjectFactory gameObjectFactory, IViewMap viewMap, IViewContext viewContext)
        {
            ModelType = typeof(TModel);
            this.gameObjectFactory = gameObjectFactory;
            this.ViewMap = viewMap;
            this.viewContext = viewContext;
        }

        protected virtual void Constructed(IModel model, TView view)
        {
            IModelViewInitializable viewInitializable = (IModelViewInitializable)view;
            viewInitializable.Constructed();
            OnConstruct?.Invoke(model, view);
        }

        bool IViewFactory.TryConstruct(IModel model, Transform parent)
        {
            return TryConstruct((TModel)model, out _, parent);
        }

        public bool TryConstruct<T>(IModel model, out T view, Transform parent)
            where T : class, IModelView
        {
            if (TryConstruct((TModel)model, out TView tView, parent))
            {
                view = tView as T;
                if (view == null)
                    throw new InvalidCastException();

                return true;
            }

            view = null;
            return false;
        }

        bool IViewFactory<TView>.TryConstruct(IModel model, out TView view, Transform parent)
        {
            return TryConstruct(model, out view, parent);
        }

        public virtual bool TryConstruct(TModel model, out TView view, Transform parent = null)
        {
            if (!gameObjectFactory.TryConstruct(model.FeatureId, out IModelView viewNonGeneric, parent))
            {
                view = null;
                return false;
            }

            view = (TView)viewNonGeneric;

            ViewMap.Add(model, view);
            Constructed(model, view);

            return true;
        }
    }
}