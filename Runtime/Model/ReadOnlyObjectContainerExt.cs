using System;
using System.Collections.Generic;
using Dythervin.Core.Extensions;

namespace Dythervin.Game.Framework
{
    public static class ReadOnlyObjectContainerExt
    {
        public static List<T> GetList<T>(this IReadOnlyObjectContainer container, Predicate<T> predicate = null)
            where T : class, IObject
        {
            var list = new List<T>();
            container.GetCollection(list, predicate);
            return list;
        }

        public static T Get<T>(this IReadOnlyObjectContainer container, Predicate<T> predicate = null)
            where T : class, IObject
        {
            container.TryGet(out T obj, predicate);
            return obj;
        }

        public static void Get<T>(this IReadOnlyObjectContainer container, out T component, Predicate<T> predicate = null)
            where T : class, IObject
        {
            if (!container.TryGet(out component, predicate))
                throw new NullReferenceException($"No components of type {typeof(T)}");
        }

        public static IObject Get(this IReadOnlyObjectContainer container, Type type)
        {
            TryGet(container, type, out IObject obj);
            return obj;
        }

        public static bool TryGet(this IReadOnlyObjectContainer container, Type type, out IObject obj,
            Predicate<IObject> predicate = null)
        {
            predicate ??= _ => true;
            for (int i = 0; i < container.ObjCount; i++)
            {
                IObject value = container.GetObjectAt(i);
                if (value.GetType().Implements(type) && predicate(value))
                {
                    obj = value;
                    return true;
                }
            }

            obj = default;
            return false;
        }

        public static void GetCollection(this IReadOnlyObjectContainer container, Type type, ICollection<IObject> objs,
            Predicate<IObject> predicate = null)
        {
            predicate ??= _ => true;
            for (int i = 0; i < container.ObjCount; i++)
            {
                IObject value = container.GetObjectAt(i);
                if (value.GetType().Implements(type) && predicate(value))
                    objs.Add(value);
            }
        }

        public static List<IObject> GetCollection(this IReadOnlyObjectContainer container, Type type,
            Predicate<IObject> predicate = null)
        {
            var list = new List<IObject>();
            GetCollection(container, type, list, predicate);
            return list;
        }
    }
}