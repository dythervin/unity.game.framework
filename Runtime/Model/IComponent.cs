using System;
using System.Collections.Generic;

namespace Dythervin.Game.Framework
{
    public interface IComponent : IObject
    {
        bool AllowMultiple { get; }

        IObject Owner { get; }

        public IReadOnlyList<Type> FastTypeAccess { get; }

        void SetOwner(IObject owner);
    }
}