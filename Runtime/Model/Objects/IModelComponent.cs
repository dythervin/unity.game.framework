using System;
using System.Collections.Generic;

namespace Dythervin.Game.Framework
{
    public interface IModelComponent : IComponent, IModel
    {
        new IModel Owner { get; }

        new IModelComponentFeature Feature { get; }
    }

    public interface IModelComponentFeature :  IFeature
    {
        bool IsMultipleAllowed { get; }

        IReadOnlyList<Type> FastTypeAccess { get; }
    }
}