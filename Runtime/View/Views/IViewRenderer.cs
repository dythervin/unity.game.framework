using System.Collections.Generic;
using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    public interface IViewRenderer
    {
        IReadOnlyList<Renderer> Renderers { get; }
    }
}