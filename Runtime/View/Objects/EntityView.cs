using Dythervin.Common;
using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    [DisallowMultipleComponent]
    public abstract class
        EntityView<TEntity, TContext, TViewComponent> : ModelViewWithComponents<TEntity, TContext, TViewComponent>,
            IEntityView<TEntity>
        where TEntity : class, IEntity
        where TContext : class, IViewContext
        where TViewComponent : class, IViewComponent
    {
        IEntity IEntityView.Model => Model;

        TEntity IProvider<TEntity>.Data => Model;
    }
}