using System;
using System.Collections.Generic;
using Dythervin.Collections;
using Dythervin.Common;

namespace Dythervin.Game.Framework
{
    public class ObjectHashList<TObject> : HashList<TObject>, IObjectList<TObject>
        where TObject : class, IObject
    {
        private new event Action<IObject> OnAdded;

        event Action<IObject> IReadOnlyObjectContainer.OnAdded
        {
            add => OnAdded += value;
            remove => OnAdded -= value;
        }

        private new event Action<IObject> OnRemoved;

        event Action<IObject> IReadOnlyObjectContainer.OnRemoved
        {
            add => OnRemoved += value;
            remove => OnRemoved -= value;
        }

        int IReadOnlyObjectContainer.ObjCount => Count;

        IObject IReadOnlyObjectContainer.GetObjectAt(int index) => this[index];

        public ObjectHashList(IEnumerable<TObject> enumerable) : base(enumerable)
        {
            Subscribe();
        }

        public ObjectHashList(int capacity = 16) : base(capacity)
        {
            Subscribe();
        }

        public ObjectHashList()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            base.OnAdded += obj => OnAdded?.Invoke(obj);
            base.OnRemoved += obj => OnRemoved?.Invoke(obj);
        }

        public virtual bool TryGet<T>(out T obj, Predicate<T> predicate = null)
            where T : class, IObject
        {
            predicate ??= Predicates<object>.True;
            foreach (TObject value in this)
            {
                if (value is T target && predicate(target))
                {
                    obj = target;
                    return true;
                }
            }

            obj = default;
            return false;
        }

        public virtual void GetCollection<T>(ICollection<T> output, Predicate<T> predicate = null)
            where T : class, IObject
        {
            predicate ??= Predicates<object>.True;
            foreach (TObject value in this)
            {
                if (value is T target && predicate(target))
                    output.Add(target);
            }
        }

        bool IObjectList.Add(IObject obj)
        {
            return AddInner((TObject)obj);
        }

        bool IObjectList.Remove(IObject obj)
        {
            return Remove((TObject)obj);
        }
    }
}