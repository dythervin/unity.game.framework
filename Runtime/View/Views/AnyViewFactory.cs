using System;
using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    public class AnyViewFactory : IAnyViewFactory
    {
        public event Action<IModel, IModelView> OnConstructed;

        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IViewMap _viewMap;

        public AnyViewFactory(IGameObjectFactory gameObjectFactory, IViewMap viewMap)
        {
            _gameObjectFactory = gameObjectFactory;
            _viewMap = viewMap;
        }

        public virtual bool TryConstruct<TObserver, T>(TObserver observer, out T view, Transform parent = null)
            where TObserver : class, IModel
            where T : class, IModelView<TObserver>
        {
            if (!_gameObjectFactory.TryConstruct(observer.FeatureId, out IModelView viewNonGeneric, parent))
            {
                view = null;
                return false;
            }

            view = (T)viewNonGeneric;

            _viewMap.Add(observer, view);
            Constructed<TObserver, T>(observer, view);
            return true;
        }

        protected virtual void Constructed<TObserver, T>(IModel objectExt, T view)
            where TObserver : class, IModel
            where T : class, IModelView<TObserver>
        {
            OnConstructed?.Invoke(objectExt, view);
        }

        public bool TryConstruct<TObserver>(TObserver observer, Transform parent = null)
            where TObserver : class, IModel
        {
            return TryConstruct<TObserver, IModelView<TObserver>>(observer, out _, parent);
        }
    }
}