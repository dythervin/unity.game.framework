using System.Collections.Generic;
using Dythervin.AutoAttach;
using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    public class ViewRenderer : MonoBehaviour, IViewRenderer
    {
        [Attach(Attach.Child, false)]
        [SerializeField] private Renderer[] renderers;

        public IReadOnlyList<Renderer> Renderers => renderers;
    }
}