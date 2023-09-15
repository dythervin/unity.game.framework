using System;
using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework
{
    public interface IAnyFactory
    {
        event Action<IModel> OnConstruct;

        IModel Construct(IModelData data);

        TObserver Construct<TObserver>(IModelData data)
            where TObserver : class, IModel;

        IRuleFactoryMap FactoryMap { get; }
    }

    public interface IAnyFactoryControl : IAnyFactory
    {
        void Constructing(IModelFactory factory);

        void DoneConstructing(IModelFactory factory);

        void ConstructComponents(IModelComponentOwnerInitializer model);
    }
}