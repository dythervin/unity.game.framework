using System;
using System.Diagnostics.CodeAnalysis;
using Dythervin.Common;
using Dythervin.Core.Extensions;
using Dythervin.Game.Framework.Data;
using Dythervin.ObjectPool;
using UnityEngine.Assertions;

namespace Dythervin.Game.Framework
{
    public abstract class ModelFactory<TModel, TData> : IModelFactory<TModel, TData>,
        IInitializable<IFeature, IModelInitIListener>
        where TData : class, IModelData
        where TModel : class, IModel, IModelInitializable
    {
        public event Action<TModel> OnConstruct;

        // event Action<IModel> IModelFactory.OnConstruct
        // {
        //     add => OnConstruct += value;
        //     remove => OnConstruct -= value;
        // }
        //
        // event Action<IModel> IModelFactory.OnInitialize
        // {
        //     add => OnInitialize += value;
        //     remove => OnInitialize += value;
        // }

        private IModelConstructorContext _constructorContext;

        private readonly IModelContext _context;

        //protected IAnyFactoryControl AnyFactory { get; private set; }

        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        protected ModelFactory(IModelContext context)
        {
            Assert.IsTrue(typeof(TModel).IsInstantiatable());
            _context = context;
        }

        protected virtual void Constructed(TModel obj)
        {
            obj.Constructed();
            OnConstruct?.Invoke(obj);
        }

        void IInitializable<IFeature, IModelInitIListener>.Init(IFeature data, IModelInitIListener modelInitIListener)
        {
            _constructorContext = new ModelConstructorContext(data, modelInitIListener);
        }

        public virtual TModel Construct(TData data)
        {
            return (TModel)Construct((IModelData)data);
        }

        TModel IModelFactory<TModel>.Construct(IModelData data)
        {
            return Construct((TData)data);
        }

        public IModel Construct(IModelData data)
        {
            TModel obj;
            using (ArrayPools.Get(3, out object[] parameters))
            {
                parameters[0] = data;
                parameters[1] = _context;
                parameters[2] = _constructorContext;

                try
                {
                    //AnyFactory.Constructing(this);
                    obj = (TModel)Activator.CreateInstance(typeof(TModel), parameters);
                }
                finally
                {
                    //AnyFactory.DoneConstructing(this);
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameters[i] = null;
                    }
                }
            }

            Constructed(obj);
            return obj;
        }
    }
}