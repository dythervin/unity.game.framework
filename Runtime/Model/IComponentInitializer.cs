using Dythervin.Common;

namespace Dythervin.Game.Framework
{
    public interface IComponentInitializable : IComponent, IInitializable, ILateInitializable
    {
    }

    public interface IModelComponentInitializable : IComponentInitializable, IModelInitializable
    {
    }
}