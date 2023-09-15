using System;
using Dythervin.AutoAttach;
using Dythervin.Common;
using Dythervin.SerializedReference.Refs;
using UnityEditor;
using UnityEngine;

namespace Dythervin.Game.Framework.View
{
    public class ModelView<TObserver, TContext, TViewComponent> : MonoView,
        IModelView<TObserver, TContext, TViewComponent>,
        IModelViewInitializable
        where TObserver : class, IModel
        where TContext : class, IViewContext
        where TViewComponent : class, IViewComponent
    {
        public InitState InitState { get; private set; }

        [Attach(Attach.Child, true)]
        [SerializeField] private RefList<TViewComponent> attachedViewComponents;

        public bool IsInitialized => Model != null;

        public TContext Context { get; private set; }

        public IObjectList<TViewComponent> ViewComponents { get; }

        IObjectListOut<TViewComponent> IModelView<TObserver, TContext, TViewComponent>.ViewComponents => ViewComponents;

        public TObserver Model { get; private set; }

        IObjectListOut<IViewComponent> IModelView.ViewComponents => ViewComponents;

        IModel IModelView.Model => Model;

        IViewContext IModelView.Context => Context;

        TObserver IProvider<TObserver>.Data => Model;

        public ModelView()
        {
            ViewComponents = new ComponentHashList<TViewComponent>(this);
        }

        protected override void Constructed()
        {
            base.Constructed();
            AddAttachedComponents();
        }

        protected virtual void Awake()
        {
        }

        protected virtual void Init()
        {
        }

        protected virtual void Reset()
        {
            string newName = GetType().Name;
            if (name == newName)
                return;

#if UNITY_EDITOR
            var path = AssetDatabase.GetAssetPath(gameObject);
            AssetDatabase.RenameAsset(path, newName);
#endif
        }

        void IModelViewInitializable.Init(IModel objectExt, IViewContext viewContext)
        {
            InitState.AssertNotInitialized();
            InitState = InitState.Initializing;

            Model = (TObserver)objectExt;
            Context = (TContext)viewContext;
            objectExt.OnDestroyed += ((IObject)this).Dispose;
            Init();
            InitState = InitState.Initialized;
            IComponentListInitializer componentsInitializer = (IComponentListInitializer)ViewComponents;

            componentsInitializer.InitComponents();
        }

        private void AddAttachedComponents()
        {
            if (attachedViewComponents == null)
                return;

            foreach (TViewComponent viewComponent in attachedViewComponents)
            {
                viewComponent.SetOwner(this);
            }
        }
    }

    public abstract class
        ModelViewWithComponents<TObserver, TContext, TViewComponent> : ModelView<TObserver, TContext, TViewComponent>,
            IModelComponentViewOwner
        where TObserver : class, IModelComponentOwner
        where TContext : class, IViewContext
        where TViewComponent : class, IViewComponent
    {
        [SerializeField] private Transform components;

        [SerializeField] private RectTransform uiComponents;

        public RectTransform UiComponentsParent => uiComponents;

        public Transform ComponentsParent => components;

        protected override void Init()
        {
            base.Init();
            Model.Components.OnAdded += ComponentsOnAdded;
            Model.Components.OnRemoved += ComponentsOnRemoved;
        }

        protected virtual void ComponentViewAdded(IModelComponentView view)
        {
        }

        protected virtual void ComponentViewRemoved(IModelComponentView view)
        {
        }

        private void ComponentsOnAdded(IObject obj)
        {
            if (Context.ViewMap.TryGet(obj, out IModelComponentView view))
            {
                ComponentViewAdded(view);
            }
        }

        private void ComponentsOnRemoved(IObject obj)
        {
            if (Context.ViewMap.TryGet(obj, out IModelComponentView view))
            {
                ComponentViewRemoved(view);
            }
        }
    }
}