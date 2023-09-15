using System;

namespace Dythervin.Game.Framework
{
    public abstract class DObject : IObject
    {
        public event Action OnDestroyed;

        public event Action<IObject> OnObjectDestroyed;

        private bool _isDisposed;

        public bool IsDisposed => _isDisposed;

        public virtual void Dispose()
        {
            if (_isDisposed)
                throw new InvalidOperationException();

            _isDisposed = true;
            Destroyed();
        }

        protected virtual void Destroyed()
        {
            OnObjectDestroyed?.Invoke(this);
            OnDestroyed?.Invoke();
        }
    }
}