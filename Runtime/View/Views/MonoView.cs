using System;
using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    public abstract class MonoView : MonoBehaviour, IView, IViewInitializable
    {
        public event Action OnDestroyed;

        public event Action<IObject> OnObjectDestroyed;


        public GameObject GameObject { get; private set; }

        public bool IsConstructed { get; protected set; }


        public Transform Transform { get; private set; }
        public bool IsDisposed { get; private set; }

        protected virtual void Destroyed()
        {
            OnDestroyed?.Invoke();
            OnObjectDestroyed?.Invoke(this);
        }

        protected virtual void OnDestroy()
        {
            if (IsDisposed)
            {
                throw new InvalidOperationException();
            }

            IsDisposed = true;
            Destroyed();
        }

        protected virtual void Constructed() { }


        void IViewInitializable.Constructed()
        {
            if (IsConstructed)
            {
                throw new Exception("Already initialized");
            }

            IsConstructed = true;
            Transform = transform;
            GameObject = gameObject;
            Constructed();
        }

        public virtual void Dispose()
        {
            Destroy(gameObject);
        }
    }
}