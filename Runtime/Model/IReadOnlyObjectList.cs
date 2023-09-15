using System.Collections.Generic;

namespace Dythervin.Game.Framework
{
    public interface IReadOnlyObjectList<TObject> : IReadOnlyObjectListOut<TObject>
        where TObject : IObject
    {
        new List<TObject>.Enumerator GetEnumerator();
    }
}