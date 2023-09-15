using System;
using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework
{
    public interface IFeature : IFeatureProvider
    {
        bool TryGetParameter<T>(out T parameter, Predicate<T> predicate = null)
            where T : class;

        bool TryGetParameter(Type type, out IFeatureParameter parameter, Predicate<IFeatureParameter> predicate = null);
    }

    public static class FeatureExtensions
    {
        public static T GetParameter<T>(this IFeature feature, Predicate<T> predicate = null)
            where T : class
        {
            feature.TryGetParameter(out T parameter, predicate);
            return parameter;
        }

        public static IFeatureParameter GetParameter(this IFeature feature, Type type, Predicate<IFeatureParameter> predicate = null)
        {
            feature.TryGetParameter(type, out IFeatureParameter parameter, predicate);
            return parameter;
        }

        public static void GetParameter<T>(this IFeature feature, out T parameter, Predicate<T> predicate = null)
            where T : class
        {
            if (!feature.TryGetParameter(out parameter, predicate))
                throw new NullReferenceException($"No feature parameter of type {typeof(T)}");
        }
    }
}