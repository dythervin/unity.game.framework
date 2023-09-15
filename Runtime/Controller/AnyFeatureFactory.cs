using System;
using System.Collections.Generic;
using Dythervin.Game.Framework.Data;
using UnityEngine.Assertions;

namespace Dythervin.Game.Framework
{
    public class AnyFeatureFactory : IAnyFactoryControl
    {
        public event Action<IModel> OnConstruct;

        public IRuleFactoryMap FactoryMap { get; }

        private readonly HashSet<IModelFactory> _constructingFactories = new HashSet<IModelFactory>();

        public AnyFeatureFactory(IRuleFactoryMap featureFactoryMap)
        {
            FactoryMap = featureFactoryMap;
            featureFactoryMap.OnAdded += factory => factory.Init(this);
        }

        public TObserver Construct<TObserver>(IModelData data)
            where TObserver : class, IModel
        {
            return (TObserver)Construct(data);
        }

        public IModel Construct(IModelData data)
        {
            Assert.IsNotNull(data);
            if (_constructingFactories.Count > 0)
                throw new Exception("Construct should not be called in observer constructor");

            FeatureId featureId;
            if (data is IModelDataWrapped)
            {
                data = data.GetOrCopy();
                ResolveData(data, out featureId);
            }
            else
            {
                featureId = data.FeatureId;
            }

            IModel obj = GetFeatureFactory(featureId).Construct(data);
            OnConstruct?.Invoke(obj);
            return obj;
        }

        private static void ResolveData(IModelData data, out FeatureId featureId)
        {
            featureId = data.EnsureNotWrapped().FeatureId;
        }

        private IFeatureFactory GetFeatureFactory(in FeatureId featureId)
        {
            if (!FactoryMap.TryGetValue(featureId, out IFeatureFactory factory))
                throw new NotImplementedException($"No factory found for {featureId.Name}");

            return factory;
        }

        void IAnyFactoryControl.Constructing(IModelFactory factory)
        {
            _constructingFactories.Add(factory);
        }

        void IAnyFactoryControl.DoneConstructing(IModelFactory factory)
        {
            _constructingFactories.Remove(factory);
        }

        public void ConstructComponents(IModelComponentOwnerInitializer modelWithComponents)
        {
            var components = modelWithComponents.Data.Components;
            for (int i = 0; i < components.Count; i++)
            {
                IModelComponent component = (IModelComponent)Construct(components[i]);
                component.SetOwner(modelWithComponents);
            }

            modelWithComponents.ComponentsConstructed();
        }
    }
}