using System.Collections.Generic;
using Dythervin.Collections;
using Dythervin.Common;

namespace Dythervin.Game.Framework
{
    public interface IFeatureFactory : IFeature, IInitializable<IAnyFactoryControl>, IModelFactory
    {
        IModelInitIListener ModelInitIListener { get; }

        IModelFactory ModelFactory { get; }
    }

    public interface IFeatureFactory<out TModel> : IFeatureFactory
        where TModel : class, IModel
    {
        new IModelFactory<TModel> ModelFactory { get; }
    }

    public interface IFeatureComponentFactory : IFeatureFactory, IModelComponentFeature
    {
        IFeatureComponentFactory AddFastTypeAccess<T>()
            where T : class, IObject;
    }

    public static class ModelComponentFactoryHandlerExtensions
    {
        public static IFeatureComponentFactory AddFastTypeAccess<T, T1>(this IFeatureComponentFactory handler)
            where T : class, IObject
            where T1 : class, IObject
        {
            handler.AddFastTypeAccess<T>().AddFastTypeAccess<T1>();
            return handler;
        }

        public static IFeatureComponentFactory AddFastTypeAccess<T, T1, T2>(this IFeatureComponentFactory handler)
            where T : class, IObject
            where T1 : class, IObject
            where T2 : class, IObject
        {
            handler.AddFastTypeAccess<T>().AddFastTypeAccess<T1>().AddFastTypeAccess<T2>();
            return handler;
        }

        public static IFeatureComponentFactory AddFastTypeAccess<T, T1, T2, T3>(this IFeatureComponentFactory handler)
            where T : class, IObject
            where T1 : class, IObject
            where T2 : class, IObject
            where T3 : class, IObject
        {
            handler.AddFastTypeAccess<T>().AddFastTypeAccess<T1>().AddFastTypeAccess<T2>().AddFastTypeAccess<T3>();

            return handler;
        }

        public static IReadOnlyList<IFeatureComponentFactory> AddFastTypeAccess<T>(
            this IReadOnlyList<IFeatureComponentFactory> list)
            where T : class, IObject
        {
            foreach (IFeatureComponentFactory c in list.ToEnumerable())
            {
                c.AddFastTypeAccess<T>();
            }

            return list;
        }

        public static IReadOnlyList<IFeatureComponentFactory> AddFastTypeAccess<T, T1>(
            this IReadOnlyList<IFeatureComponentFactory> list)
            where T : class, IObject
            where T1 : class, IObject
        {
            foreach (IFeatureComponentFactory c in list.ToEnumerable())
            {
                c.AddFastTypeAccess<T>().AddFastTypeAccess<T1>();
            }

            return list;
        }

        public static IReadOnlyList<IFeatureComponentFactory> AddFastTypeAccess<T, T1, T2>(
            this IReadOnlyList<IFeatureComponentFactory> list)
            where T : class, IObject
            where T1 : class, IObject
            where T2 : class, IObject
        {
            foreach (IFeatureComponentFactory c in list.ToEnumerable())
            {
                c.AddFastTypeAccess<T>().AddFastTypeAccess<T1>().AddFastTypeAccess<T2>();
            }

            return list;
        }

        public static IReadOnlyList<IFeatureComponentFactory> AddFastTypeAccess<T, T1, T2, T3>(
            this IReadOnlyList<IFeatureComponentFactory> list)
            where T : class, IObject
            where T1 : class, IObject
            where T2 : class, IObject
            where T3 : class, IObject
        {
            foreach (IFeatureComponentFactory c in list.ToEnumerable())
            {
                c.AddFastTypeAccess<T>().AddFastTypeAccess<T1>().AddFastTypeAccess<T2>().AddFastTypeAccess<T3>();
            }

            return list;
        }
    }
}