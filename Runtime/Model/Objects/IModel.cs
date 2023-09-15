using System;
using Dythervin.Common;
using Dythervin.Common.ID;
using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework
{
    /// <summary>
    /// ModelView
    /// </summary>
    public interface IModel : IObject, IIdentified
    {
        event Action OnDataChanged;

        IModelData Data { get; }

        IModelContext Context { get; }

        IFeature Feature { get; }

        FeatureId FeatureId { get; }
    }

    public interface IModelInitializable : IModel, IInitializable
    {
        void Constructed();
    }

    public interface IModel<out TComponent> :
        IComponentOwner<TComponent>,
        IModelComponentOwner
        where TComponent : class, IModelComponent
    {
    }
}