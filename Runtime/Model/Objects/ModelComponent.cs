using System;
using System.Collections.Generic;
using System.Diagnostics;
using Dythervin.Common;
using Dythervin.Game.Framework.Data;
using UnityEngine.Assertions;

namespace Dythervin.Game.Framework
{
    [DebuggerDisplay("OwnerId: {Owner.Id}")]
    public abstract class ModelComponent<TOwner, TContext, TData> : Model<TData, TContext>,
        IModelComponent,
        IModelComponentInitializable
        where TData : class, IModelComponentData
        where TOwner : class, IModelComponentOwner
        where TContext : class, IModelContext
    {
        public virtual bool AllowMultiple => false;

        public TOwner Owner { get; private set; }

        public new IModelComponentFeature Feature => (IModelComponentFeature)base.Feature;

        public IReadOnlyList<Type> FastTypeAccess => Feature.FastTypeAccess;

        protected InitState IsLateInitialized { get; private set; }

        InitState ILateInitializable.LateInitState => IsLateInitialized;

        IObject IComponent.Owner => Owner;

        IModel IModelComponent.Owner => Owner;

        protected ModelComponent(TData componentDataInventoryComponentData, TContext context,
            IModelConstructorContext constructorContext) : base(componentDataInventoryComponentData,
            context,
            constructorContext)
        {
        }

        public virtual void SetOwner(TOwner owner)
        {
            Assert.IsTrue(Owner == null && owner != null);
            Owner = owner;
            ((IObjectList)Owner?.Components)?.Add(this);
        }

        protected override void Init()
        {
            Assert.IsNotNull(Owner);
            base.Init();
        }

        protected virtual void LateInit()
        {
        }

        void ILateInitializable.LateInit()
        {
            IsLateInitialized.AssertNotInitialized();

            IsLateInitialized = InitState.Initializing;
            LateInit();
            IsLateInitialized = InitState.Initialized;
        }

        void IComponent.SetOwner(IObject owner)
        {
            SetOwner((TOwner)owner);
        }
    }

    [DebuggerDisplay("OwnerId: {Owner.Id}")]
    public abstract class ModelComponent<TOwner, TContext, TData, TComponent> : Model<TData, TContext, TComponent>,
            IModelComponent,
            IComponentInitializable
        where TData : class, IModelComponentData, IModelComponentOwnerData
        where TOwner : class, IModelComponentOwner
        where TComponent : class, IModelComponent, IModel
        where TContext : class, IModelContext
    {
        private InitState _lateInitState;

        public virtual bool AllowMultiple => false;

        public TOwner Owner { get; private set; }

        public new IModelComponentFeature Feature => (IModelComponentFeature)base.Feature;

        public IReadOnlyList<Type> FastTypeAccess => Feature.FastTypeAccess;

        InitState ILateInitializable.LateInitState => _lateInitState;

        IObject IComponent.Owner => Owner;

        IModel IModelComponent.Owner => Owner;

        protected ModelComponent(TData data, TContext context, IModelConstructorContext constructorContext) :
            base(data, context, constructorContext)
        {
        }

        public virtual void SetOwner(TOwner owner)
        {
            Assert.IsTrue(Owner == null && owner != null);
            Owner = owner;
            ((IObjectList)Owner?.Components)?.Add(this);
        }

        protected override void Init()
        {
            Assert.IsNotNull(Owner);
            base.Init();
        }

        protected virtual void LateInit()
        {
        }

        void ILateInitializable.LateInit()
        {
            _lateInitState.AssertNotInitialized();

            _lateInitState = InitState.Initializing;
            LateInit();
            _lateInitState = InitState.Initialized;
        }

        void IComponent.SetOwner(IObject owner)
        {
            SetOwner((TOwner)owner);
        }
    }
}