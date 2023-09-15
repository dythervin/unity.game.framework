using System;
using Dythervin.Common;
using UnityEngine;

namespace Dythervin.Game.Framework
{
    public interface IEntity : IModelComponentOwner, IRadiused, IPositioned
    {
        event Action OnActiveChanged;
        event Action<IEntity> OnObjectActiveChanged;
        new IReadOnlyObjectListOut<IEntityComponent> Components { get; }

        Tag Tags { get; set; }

        bool IsActive { get; set; }

        Transform Transform { get; }
    }

    public interface IEntity<out TComponent> : IEntity
        where TComponent : class, IEntityComponent
    {
        new IReadOnlyObjectListOut<TComponent> Components { get; }
    }
}