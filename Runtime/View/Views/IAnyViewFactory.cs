using System;
using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    public interface IAnyViewFactory
    {
        event Action<IModel, IModelView> OnConstructed;

        bool TryConstruct<TObserver, T>(TObserver observer, out T view, Transform parent = null)
            where T : class, IModelView<TObserver>
            where TObserver : class, IModel;

        bool TryConstruct<TObserver>(TObserver observer, Transform parent = null)
            where TObserver : class, IModel;
    }
}