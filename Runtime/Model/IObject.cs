using System;
using Dythervin.Core;

namespace Dythervin.Game.Framework
{
    public interface IObject : IDisposableExt
    {
        event Action OnDestroyed;

        event Action<IObject> OnObjectDestroyed;
    }
}