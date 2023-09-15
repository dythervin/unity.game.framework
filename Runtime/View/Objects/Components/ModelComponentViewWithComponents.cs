using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    public abstract class ModelComponentViewWithComponents<TOwner, TObserver, TContext, TViewComponent>
        : ModelViewWithComponents<TObserver, TContext, TViewComponent>, IModelComponentView
        where TObserver : class, IModelComponent, IModelComponentOwner
        where TContext : class, IViewContext
        where TViewComponent : class, IViewComponent
        where TOwner : class, IModelComponentViewOwner, IModelView
    {
        public TOwner Owner { get; private set; }

        IModelView IModelComponentView.Owner => Owner;

        protected override void Init()
        {
            base.Init();
            Owner = Context.ViewMap.Get<TOwner>(Model.Owner);
            Transform.SetParent(Transform is RectTransform ? Owner.UiComponentsParent : Owner.Transform, false);
            Transform.localScale = Vector3.one;
        }
    }
}