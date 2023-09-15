using System;
using System.Diagnostics.CodeAnalysis;
using Dythervin.Common;
using JetBrains.Annotations;

namespace Dythervin.Game.Framework.View
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IModelView : IView
    {
        IViewContext Context { get; }

        IModel Model { get; }

        IObjectListOut<IViewComponent> ViewComponents { get; }
    }

    public static class ModelViewExtensions
    {
        public static bool TryGetViewComponent<T>(this IModelView entityExt, [CanBeNull] out T component,
            Predicate<T> predicate = null)
            where T : class, IObject
        {
            return entityExt.ViewComponents.TryGet(out component, predicate);
        }

        public static void GetViewComponent<T>(this IModelView entityExt, out T component, Predicate<T> predicate = null)
            where T : class, IObject
        {
            entityExt.ViewComponents.Get(out component, predicate);
        }

        [CanBeNull]
        public static T GetViewComponent<T>(this IModelView entityExt, Predicate<T> predicate = null)
            where T : class, IObject
        {
            return entityExt.ViewComponents.Get(predicate);
        }
    }

    public interface IModelView<out TObserver> : IModelView, IProvider<TObserver>
        where TObserver : class, IObject
    {
        new TObserver Model { get; }
    }

    public interface IModelView<out TObserver, out TContext, out TComponent> : IModelView<TObserver>
        where TObserver : class, IObject
        where TComponent : class, IViewComponent
        where TContext : class, IViewContext
    {
        new TContext Context { get; }

        new IObjectListOut<TComponent> ViewComponents { get; }
    }
}