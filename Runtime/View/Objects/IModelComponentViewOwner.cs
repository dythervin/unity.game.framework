using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    public interface IModelComponentViewOwner : IView
    {
        Transform ComponentsParent { get; }

        RectTransform UiComponentsParent { get; }
    }
}