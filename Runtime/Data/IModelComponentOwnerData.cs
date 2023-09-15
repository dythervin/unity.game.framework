using System.Collections.Generic;

namespace Dythervin.Game.Framework.Data
{
    public interface IModelComponentOwnerData : IModelData, IModelComponentOwnerROData
    {
    }
    public interface IModelComponentOwnerROData : IModelData
    {
        IReadOnlyList<IModelComponentData> Components { get; }
    }
}