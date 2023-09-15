using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    public interface IView : IObject, IViewInputProvider
    {
        GameObject GameObject { get; }

        Transform Transform { get; }
    }
}