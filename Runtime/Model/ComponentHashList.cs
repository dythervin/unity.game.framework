using System;
using System.Collections.Generic;
using Dythervin.Collections;
using Dythervin.Common;

namespace Dythervin.Game.Framework
{
    public interface IComponentListInitializer
    {
        void InitComponents();
    }

    public class ComponentHashList<TComponent> : ObjectHashList<TComponent>, IComponentListInitializer
        where TComponent : class, IComponent
    {
        private const int MapQueryThreshold = 4;

        private readonly MultiDictionary<Type, TComponent, HashSet<TComponent>> _componentsMap = new();

        private bool _areComponentsInitialized;

        private bool _areComponentsStarted;

        private bool UseMap => Count >= MapQueryThreshold;

        public IObject Owner { get; }

        public ComponentHashList(IObject owner)
        {
            Owner = owner;
        }

        public ComponentHashList(IObject owner, IEnumerable<TComponent> enumerable) : base(enumerable)
        {
            Owner = owner;
        }

        public ComponentHashList(IObject owner, int capacity = 16) : base(capacity)
        {
            Owner = owner;
        }

        public override bool Remove(TComponent obj)
        {
            if (!base.Remove(obj))
                return false;

            RemoveComponent(obj);
            return true;
        }

        public override bool TryGet<T>(out T obj, Predicate<T> predicate = null)
            where T : class
        {
            if (!UseMap || !_componentsMap.TryGetValue(typeof(T), out var value))
            {
                return base.TryGet(out obj, predicate);
            }

            predicate ??= Predicates<object>.True;
            foreach (TComponent component in value)
            {
                if (component is T tComponent && predicate(tComponent))
                {
                    obj = tComponent;
                    return true;
                }
            }

            obj = null;
            return false;
        }

        public override void GetCollection<T>(ICollection<T> output, Predicate<T> predicate = null)
            where T : class
        {
            if (!UseMap || !_componentsMap.TryGetValue(typeof(T), out var value))
            {
                base.GetCollection(output, predicate);
                return;
            }

            predicate ??= Predicates<object>.True;
            foreach (IComponent component in value)
            {
                T tComponent = (T)component;
                if (predicate(tComponent))
                {
                    output.Add(tComponent);
                }
            }
        }

        protected override bool AddInner(TComponent obj)
        {
            if (obj.Owner != Owner)
                throw new Exception("Owned must be set before adding to components list");

            if (!base.AddInner(obj))
                return false;

            AddComponent(obj);
            if (_areComponentsInitialized)
            {
                IComponentInitializable componentInitializable = (IComponentInitializable)obj;
                componentInitializable.Init();
                if (_areComponentsStarted)
                {
                    componentInitializable.LateInit();
                }
            }

            return true;
        }

        private void AddComponent(TComponent component)
        {
            Type type = component.GetType();
            _componentsMap.Add(type, component, component.AllowMultiple);

            foreach (Type iInterface in component.FastTypeAccess.ToEnumerable())
            {
                _componentsMap.Add(iInterface, component, true);
            }
        }

        private void RemoveComponent(TComponent component)
        {
            Type type = component.GetType();
            _componentsMap.Remove(type, component, component.AllowMultiple);

            foreach (Type iInterface in component.FastTypeAccess.ToEnumerable())
            {
                _componentsMap.Remove(iInterface, component, true);
            }
        }

        private void LateInitComponents()
        {
            if (_areComponentsStarted)
            {
                throw new Exception("Already started");
            }

            for (int i = 0; i < Count; i++)
            {
                IComponentInitializable componentInitializable = (IComponentInitializable)this[i];
                componentInitializable.LateInit();
            }

            _areComponentsStarted = true;
        }

        public void InitComponents()
        {
            if (_areComponentsStarted)
            {
                throw new Exception("Already initialized");
            }

            for (int i = 0; i < Count; i++)
            {
                IComponentInitializable componentInitializable = (IComponentInitializable)this[i];
                componentInitializable.Init();
            }

            _areComponentsInitialized = true;
            LateInitComponents();
        }
    }
}