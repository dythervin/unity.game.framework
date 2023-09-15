using System;
using System.Diagnostics;
using Dythervin.Common;
using Dythervin.Common.ID;
using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework
{
    [DebuggerDisplay("id: {_id}")]
    public abstract class Model<TData, TContext> : DObject, IModelInitializable
        where TData : class, IModelData
        where TContext : class, IModelContext
    {
        public event Action OnDataChanged;

        private InitState _initializationState;

        private bool _isConstructed;

        private readonly IModelConstructorContext _constructorContext;

        private uint _id;

        private readonly TData _data;

        public IFeature Feature { get; }

        public FeatureId FeatureId => Data.FeatureId;

        public IGameController GameController => Context.GameController;

        public TContext Context { get; }

        InitState IInitializable.InitState => IsInitialized;

        protected InitState IsInitialized => _initializationState;

        IModelContext IModel.Context => Context;

        uint IIdentified.Id => _id;

        protected Model(TData componentDataInventoryComponentData, TContext context,
            IModelConstructorContext constructorContext)
        {
            _data = componentDataInventoryComponentData ??
                    throw new NullReferenceException(nameof(componentDataInventoryComponentData));

            Context = context ?? throw new NullReferenceException(nameof(context));
            Feature = constructorContext.Feature;
            _constructorContext = constructorContext;
            this.SetId();
        }

        ~Model()
        {
            this.ReleaseId();
        }

        /// <summary>
        ///     Called only on data request & on init
        /// </summary>
        /// <param name="data"></param>
        protected virtual void LazyDataUpdated(TData data)
        {
        }

        protected virtual void Constructed()
        {
        }

        protected virtual void Init()
        {
            //LazyDataUpdated(Data);
        }

        void IInitializable.Init()
        {
            _initializationState.AssertNotInitialized();

            _initializationState = InitState.Initializing;
            Init();

            _initializationState = InitState.Initialized;
            _constructorContext.InitListener.Notify(this);
        }

        void IModelInitializable.Constructed()
        {
            if (_isConstructed)
            {
                throw new Exception("Already constructed");
            }

            _isConstructed = true;
            Constructed();
        }

        protected void MarkDataDirty()
        {
        }

        protected TData Data
        {
            get
            {
                SyncData(_data);
                return _data;
            }
        }

        protected virtual void SyncData(TData data)
        {
        }

        IModelData IModel.Data => _data;

        void IIdentified.SetId(uint value)
        {
            _id = value;
        }
    }

    public abstract class Model<TData, TContext, TComponent> : Model<TData, TContext>,
        IModel<TComponent>,
        IModelComponentOwnerInitializer
        where TComponent : class, IModel, IModelComponent
        where TData : class, IModelComponentOwnerData
        where TContext : class, IModelContext
    {
        protected readonly ComponentHashList<TComponent> components;

        public IReadOnlyObjectListOut<TComponent> Components => components;

        IComponentListInitializer IModelComponentOwnerInitializer.ComponentListInitializer => components;

        IModelComponentOwnerROData IModelComponentOwner.Data => Data;

        IReadOnlyObjectListOut<IComponent> IComponentOwner.Components => Components;

        IReadOnlyObjectListOut<IModelComponent> IModelComponentOwner.Components => Components;

        protected Model(TData data, TContext context, IModelConstructorContext constructorContext) : base(data,
            context,
            constructorContext)
        {
            components = new ComponentHashList<TComponent>(this);
        }

        protected virtual void ComponentsConstructed()
        {
            components.OnAdded += Components_OnAdded;
            components.OnRemoved += Components_OnRemoved;
        }

        protected virtual void ComponentAdded(TComponent component)
        {
        }

        protected virtual void ComponentRemoved(TComponent component)
        {
        }

        protected override void Destroyed()
        {
            for (int i = components.Count - 1; i >= 0; i--)
            {
                TComponent entityComponent = components[i];
                entityComponent.Dispose();
            }

            base.Destroyed();
        }

        protected TComponentData[] GetNewComponentsDataArray<TComponentData>()
            where TComponentData : class, IModelComponentData
        {
            var array = new TComponentData[components.Count];
            int i = 0;
            foreach (TComponent component in components)
            {
                array[i++] = (TComponentData)component.Data;
            }

            return array;
        }

        void IModelComponentOwnerInitializer.ComponentsConstructed()
        {
            ComponentsConstructed();
        }

        private void Components_OnAdded(TComponent obj)
        {
            ComponentAdded(obj);
        }

        private void Components_OnRemoved(TComponent obj)
        {
            ComponentRemoved(obj);
        }
    }
}