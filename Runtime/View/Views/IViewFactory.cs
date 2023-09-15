using System;
using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    public interface IViewFactory
    {
        Type ModelType { get; }

        IViewMap ViewMap { get; }

        event Action<IModel, IModelView> OnConstruct;

        bool TryConstruct<T>(IModel model, out T view, Transform parent = null)
            where T : class, IModelView;

        bool TryConstruct(IModel model, Transform parent = null);
    }

    public interface IViewFactory<TView> : IViewFactory
        where TView : class, IModelView
    {
        bool TryConstruct(IModel model, out TView view, Transform parent = null);
    }

    public interface IViewFactory<in TModel, TView> : IViewFactory<TView>
        where TView : class, IModelView
        where TModel : class, IModel
    {
        bool TryConstruct(TModel model, out TView view, Transform parent = null);
    }
}