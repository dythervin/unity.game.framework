using System;
using System.Collections.Generic;

namespace Dythervin.Game.Framework
{
    public interface IReadOnlyObjectContainer
    {
        event Action<IObject> OnAdded;

        event Action<IObject> OnRemoved;

        event Action OnChanged;

        int ObjCount { get; }

        IObject GetObjectAt(int index);

        bool TryGet<T>(out T obj, Predicate<T> predicate = null)
            where T : class, IObject;

        void GetCollection<T>(ICollection<T> output, Predicate<T> predicate = null)
            where T : class, IObject;
    }
}