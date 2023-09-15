using System.Collections.Generic;

namespace Dythervin.Game.Framework.Data
{
    public interface IGameData : IModelData, IModelComponentOwnerData
    {
        new IReadOnlyList<IGameComponentData> Components { get; }
    }
}