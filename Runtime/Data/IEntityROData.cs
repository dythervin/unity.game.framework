using System.Collections.Generic;
using Dythervin.Common;

namespace Dythervin.Game.Framework.Data
{
    public interface IEntityROData : IModelComponentOwnerData
    {
        Tag Tags { get; }
        
        new IReadOnlyList<IEntityComponentData> Components { get; }
    }

    public interface IEntityData : IEntityROData
    {
        new Tag Tags { get; set; }
    }
}