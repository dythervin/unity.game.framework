using System.Collections.Generic;

namespace Dythervin.Game.Framework
{
    public interface IReadOnlyObjectListOut<out TObject> : IReadOnlyList<TObject>, IReadOnlyObjectContainer
        where TObject : IObject
    {
    }
}