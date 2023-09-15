using System;
using JetBrains.Annotations;

namespace Dythervin.Game.Framework
{
    public static class ModelComponentOwnerExtensions
    {
        public static bool TryGetComponent<T>(this IModelComponentOwner entityExt, [CanBeNull] out T component,
            Predicate<T> predicate = null)
            where T : class, IObject
        {
            return entityExt.Components.TryGet(out component, predicate);
        }

        public static void GetComponent<T>(this IModelComponentOwner entityExt, out T component,
            Predicate<T> predicate = null)
            where T : class, IObject
        {
            entityExt.Components.Get(out component, predicate);
        }

        [CanBeNull]
        public static T GetComponent<T>(this IModelComponentOwner entityExt, Predicate<T> predicate = null)
            where T : class, IObject
        {
            return entityExt.Components.Get(predicate);
        }
    }
}